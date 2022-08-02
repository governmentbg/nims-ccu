using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Eumis.Cli
{
    public class ChangeLogCommand : ICommand
    {
        private const string trelloCommentRegEx = @"!\[\]\(https:\/\/github\.trello\.services\/images\/mini-trello-icon\.png\) \[(.+)\]\((https:\/\/trello\.com\/c\/(.+)\/.+)\)";

        private string repo;
        private string repoPath;
        private string access_token;
        private string trelloKey;
        private string trelloToken;
        private int countCommits;
        private int currentCommit = 0;
        private int countCards = 0;
        private int countSkipedCards = 0;

        public string Name { get; } = "changelog";

        public void Configure(CommandLineApplication app, CancellationToken stopped)
        {
            var fromHashArg = app.Argument("fromHash", "Hash of start commit.");
            var toHashArg = app.Argument("toHash", "Hash of end commit.");
            var outputFileArg = app.Argument("outputFile", "Optional path to output file.");

            app.OnExecute(async () =>
            {
                string fromHash = fromHashArg.Value;
                string toHash = toHashArg.Value;
                string outputFile = outputFileArg.Value;

                if (string.IsNullOrEmpty(fromHash))
                {
                    Console.WriteLine("Missing fromHash!");
                    app.ShowHelp();

                    return 1;
                }

                if (string.IsNullOrEmpty(toHash))
                {
                    Console.WriteLine("Missing toHash!");
                    app.ShowHelp();

                    return 1;
                }

                IConfiguration config = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                string GetEnvVar(string varName, string defaultValue = null)
                {
                    var value = config[varName] ?? defaultValue;
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new Exception($"Missing environment variables - {varName}");
                    }

                    return value;
                }

                repo = GetEnvVar("EUMIS_CLI_GIT_REPO", "hristo-ciela/eumis");
                repoPath = GetEnvVar("EUMIS_CLI_GIT_REPOPATH");
                access_token = GetEnvVar("EUMIS_CLI_GIT_ACCESSTOKEN");
                trelloKey = GetEnvVar("EUMIS_CLI_TRELLO_KEY");
                trelloToken = GetEnvVar("EUMIS_CLI_TRELLO_TOKEN");

                try
                {
                    await this.GetAllCardAsync(fromHash, toHash, outputFile, stopped);
                }
                catch
                {
                    if (!stopped.IsCancellationRequested)
                    {
                        throw;
                    }
                }

                return 0;
            });
        }

        private List<string> GetCommits(string fromHash, string toHash)
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = repoPath,
                FileName = "git",
                Arguments = $"log {fromHash}..{toHash}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            string result;
            using (var process = Process.Start(startInfo))
            {
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            };

            return Regex.Matches(result, @"\bcommit\b (\w{40})")
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Reverse()
                .ToList();
        }

        private async Task<JArray> GetPullsAsync(CancellationToken ct)
        {
            var resp = await this.GetStringAsync(
                $"https://api.github.com/repos/{repo}/pulls" +
                    "?state=all" +
                    "&sort=updated" +
                    "&direction=direction" +
                    "$per_page=100" +
                    $"&access_token={access_token}",
                ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetIssueCommentsAsync(string number, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.github.com/repos/{repo}/issues/{number}/comments" +
                $"?access_token={access_token}",
                ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetCommitCommentsAsync(string hash, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.github.com/repos/{repo}/commits/{hash}/comments" +
                $"?access_token={access_token}",
                ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetTrelloCardListAsync(string shortLink, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/Cards/{shortLink}/labels?key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<string> GetCommitCardTitlesAsync(string hash, JArray pulls, CancellationToken ct)
        {
            JArray comments, labels;
            JObject pull = pulls.Children<JObject>().FirstOrDefault(o => o["head"] != null && o["head"]["sha"].ToString() == hash);
            string result = string.Empty;

            if (pull != null)
            {
                comments = await GetIssueCommentsAsync(pull["number"].ToString(), ct);
            }
            else
            {
                comments = await GetCommitCommentsAsync(hash, ct);
            }

            if (comments.Count > 0)
            {
                Regex regex = new Regex(trelloCommentRegEx);
                Match match = regex.Match(comments.Children<JObject>().FirstOrDefault(o => o["body"] != null)["body"].ToString());

                if (match.Success)
                {
                    labels = await GetTrelloCardListAsync(match.Groups[3].Value, ct);
                    JObject label = labels.Children<JObject>().FirstOrDefault(o => o["color"].ToString() == "orange");

                    if (label == null)
                    {
                        Interlocked.Add(ref countCards, 1);
                        result = match.Groups[1].Value;
                    }
                    else
                    {
                        Interlocked.Add(ref countSkipedCards, 1);
                    }
                }
            }

            Interlocked.Add(ref currentCommit, 1);
            return result;
        }

        private async Task<string> GetCardTitlesAsync(string fromHash, string toHash, CancellationToken ct)
        {
            var commits = GetCommits(fromHash, toHash);
            countCommits = commits.Count;

            Console.WriteLineFormatted("Found {0} commits between {1} and {2}.", Color.LawnGreen, Color.White, commits.Count, fromHash, toHash);

            if (countCommits != 0)
            {
                Console.Write("Getting pull requests info.");

                var pulls = await GetPullsAsync(ct);

                Console.WriteLine(" Done.");

                var cards = new List<string>();
                using (var pbar = new ProgressBar(commits.Count, "Getting card info."))
                {
                    foreach (var c in commits)
                    {
                        cards.Add(await GetCommitCardTitlesAsync(c, pulls, ct));
                        pbar.Tick($"Getting card info. {cards.Count} of {commits.Count}.");
                    }
                }

                Console.WriteFormatted("Found {0} eligible cards", Color.LawnGreen, Color.White, countCards - countSkipedCards);
                if (countSkipedCards > 0)
                {
                    Console.WriteLineFormatted(", skipped {0} cards (with orange label in Trello).", Color.LawnGreen, Color.White, countSkipedCards);
                }
                else
                {
                    Console.WriteLine(".");
                }

                return string.Join(
                    "\r\n",
                    cards
                        .Where(c => !c.Equals(string.Empty))
                        .ToList());
            }
            else
            {
                return string.Empty;
            }
        }

        private async Task GetAllCardAsync(string fromHash, string toHash, string outputFile, CancellationToken ct)
        {
            string result = await GetCardTitlesAsync(fromHash, toHash, ct);

            if (string.IsNullOrEmpty(result))
            {
                return;
            }

            if (!string.IsNullOrEmpty(outputFile))
            {
                File.WriteAllText(outputFile, result, Encoding.UTF8);
                Console.WriteLineFormatted("Result written to {0}", Color.LawnGreen, Color.White, outputFile);
            }
            else
            {
                var currEnc = Console.OutputEncoding;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(result);
                Console.OutputEncoding = currEnc;
            }
        }

        private async Task<string> GetStringAsync(string uri, CancellationToken ct)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));
                request.Headers.Add("User-Agent", "Request-Promiser");

                HttpResponseMessage response = await client.SendAsync(request, ct);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Request failed - {uri}");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
