using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Console = Colorful.Console;

namespace Eumis.Cli
{
    public class ReportCommand : ICommand
    {
        private const string trelloModuleCommentRegEx = @"\$Модул:([А-я\s]+)#";
        private const string trelloAttachmentRegEx = @"https:\/\/github\.com\/.+\/Eumis\/+";

        private string trelloKey;
        private string trelloToken;
        private string trelloBoardId;
        private string error;
        private int currentCard = 0;
        private int countCards = 0;
        private int countCrCards = 0;
        private int countHouseKeepingCards = 0;

        public string Name { get; } = "report";

        public void Configure(CommandLineApplication app, CancellationToken stopped)
        {
            var listNameArg = app.Argument("listName", "Trello list name.");
            var reportFileArg = app.Argument("reportFile", "Path to report file.");

            app.OnExecute(async () =>
            {
                string listName = listNameArg.Value;
                string reportFile = reportFileArg.Value;

                if (string.IsNullOrEmpty(listName))
                {
                    Console.WriteLine("Missing listName!");
                    app.ShowHelp();

                    return 1;
                }

                if (string.IsNullOrEmpty(reportFile))
                {
                    Console.WriteLine("Missing reportFile!");
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

                trelloKey = GetEnvVar("EUMIS_CLI_TRELLO_KEY");
                trelloToken = GetEnvVar("EUMIS_CLI_TRELLO_TOKEN");

                try
                {
                    await this.GetAllCardAsync(listName, reportFile, stopped);
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

        private async Task GetAllCardAsync(string listName, string reportFile, CancellationToken ct)
        {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            using (XmlWriter writer = XmlWriter.Create(reportFile, settings))
            {
                var cards = await GetCardsAsync(listName, ct);

                writer.WriteStartDocument();

                if (string.IsNullOrEmpty(error))
                {
                    writer.WriteComment($"Generated date: {DateTime.Now}");
                    writer.WriteComment($"Report list: {listName}");
                    writer.WriteComment($"Find report cards: {countCards} (including {countCrCards} CR cards)");
                    writer.WriteComment($"Find house keeping cards (with orange label in Trello): {countHouseKeepingCards}");
                    writer.WriteComment($"Total find cards: {cards.Count}");
                    writer.WriteStartElement("Report");

                    if (countCrCards > 0)
                    {
                        var crCards = cards.Where(c => c.IsHouseKeeping == false && !string.IsNullOrEmpty(c.Level));

                        writer.WriteStartElement("NewFeatureDescription");

                        foreach (var (value, index) in crCards.Select((v, i) => (v, i)))
                        {
                            writer.WriteStartElement($"Number_{index + 1}");

                            writer.WriteElementString("Title", $"{value.Title} ({value.Level})");
                            writer.WriteElementString("Description", value.Description);
                            writer.WriteElementString("DaysInDevelopmentList", $"{Math.Round(value.DurationInLists.Where(d => d.ListName == "In Development").Select(d => d.DurationTotalHours).Sum()/24)}");
                            writer.WriteElementString("DaysInTestList", $"{Math.Round(value.DurationInLists.Where(d => d.ListName == "In Test").Select(d => d.DurationTotalHours).Sum()/24)}");

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }

                    var reportCards = cards.Where(c => c.IsHouseKeeping == false);

                    if (reportCards.Any())
                    {
                        writer.WriteStartElement("CardTable");

                        var results = reportCards.GroupBy(
                            c => c.Module,
                            c => c.Title,
                            (key, g) => new { Module = key, Cards = g.ToList() });

                        foreach (var item in results)
                        {
                            writer.WriteStartElement("ReportCard");

                            writer.WriteElementString("Module", item.Module);

                            writer.WriteStartElement("Cards");
                            foreach (var card in item.Cards)
                            {
                                writer.WriteElementString("Card", card);
                            }
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();

                        writer.WriteStartElement("ReportCards");

                        foreach (Card card in reportCards)
                        {
                            writer.WriteStartElement("ReportCard");

                            writer.WriteElementString("ID", card.Id);
                            writer.WriteElementString("Module", card.Module);
                            writer.WriteElementString("Title", card.Title);
                            writer.WriteElementString("Level", card.Level);

                            writer.WriteStartElement("Labels");
                            foreach (var label in card.Labels)
                            {
                                writer.WriteElementString("Label", label);
                            }
                            writer.WriteEndElement();

                            writer.WriteElementString("Description", card.Description);
                            writer.WriteElementString("Due", card.Due);

                            writer.WriteStartElement("DurationInLists");
                            foreach (var durationInList in card.DurationInLists)
                            {
                                writer.WriteStartElement("DurationInList");

                                writer.WriteElementString("List", durationInList.ListName);
                                writer.WriteElementString("Duration", $"{durationInList.DurationDays} дни и {durationInList.DurationHours} часа");

                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();

                            writer.WriteStartElement("Members");
                            foreach (var member in card.Members)
                            {
                                writer.WriteElementString("Member", member);
                            }
                            writer.WriteEndElement();

                            writer.WriteStartElement("Attachments");
                            foreach (var url in card.GitHubUrls)
                            {
                                writer.WriteElementString("GitHubUrls", url);
                            }
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }

                    var houseKeepingCards = cards.Where(c => c.IsHouseKeeping == true);

                    if (houseKeepingCards.Any())
                    {
                        writer.WriteStartElement("HouseKeepingCards");

                        foreach (Card card in houseKeepingCards)
                        {
                            writer.WriteStartElement("HouseKeepingCard");

                            writer.WriteElementString("ID", card.Id);
                            writer.WriteElementString("Title", card.Title);

                            writer.WriteStartElement("Labels");
                            foreach (var label in card.Labels)
                            {
                                writer.WriteElementString("Label", label);
                            }
                            writer.WriteEndElement();

                            writer.WriteElementString("Description", card.Description);
                            writer.WriteElementString("Due", card.Due);

                            writer.WriteStartElement("DurationInLists");
                            foreach (var durationInList in card.DurationInLists)
                            {
                                writer.WriteStartElement("DurationInList");

                                writer.WriteElementString("List", durationInList.ListName);
                                writer.WriteElementString("Duration", $"{durationInList.DurationDays} дни и {durationInList.DurationHours} часа");

                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();

                            writer.WriteStartElement("Members");
                            foreach (var member in card.Members)
                            {
                                writer.WriteElementString("Member", member);
                            }
                            writer.WriteEndElement();

                            writer.WriteStartElement("Attachments");
                            foreach (var url in card.GitHubUrls)
                            {
                                writer.WriteElementString("GitHubUrls", url);
                            }
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteStartElement("Errors");
                    writer.WriteElementString("Error", error);
                    writer.WriteEndElement();
                }

                writer.WriteEndDocument();
            }

            Console.WriteLineFormatted("Result written to {0}", Color.LawnGreen, Color.White, reportFile);
        }

        private async Task<List<Card>> GetCardsAsync(string listName, CancellationToken ct)
        {
            Console.WriteLineFormatted("Search list with name {0}.", Color.LawnGreen, Color.White, listName);

            var trelloBoard = (await GetBoardAsync(ct)).Children<JObject>().FirstOrDefault(l => l["name"].ToString() == "Tasks");

            if (trelloBoard != null)
            {
                trelloBoardId = trelloBoard["id"].ToString();

                var boardLists = (await GetListsInBoardAsync(ct)).Children<JObject>().FirstOrDefault(l => l["name"].ToString() == listName);

                if (boardLists != null)
                {
                    Console.WriteLine(" Done.");
                    Console.WriteLineFormatted("Searching for cards in list {0}.", Color.LawnGreen, Color.White, listName);

                    var cards = new JArray((await GetCardsInListAsync(boardLists["id"].ToString(), ct)).OrderBy(c => (string)c["name"]));

                    Console.WriteLine(" Done.");
                    Console.WriteLineFormatted("Found {0} cards in list {1}.", Color.LawnGreen, Color.White, cards.Count, listName);

                    if (cards.Count != 0)
                    {
                        var boardLabels = (await GetBoardLabelsAsync(ct)).ToObject<List<Label>>();

                        var boardMembers = (await GetBoardMembersAsync(ct)).ToObject<List<Member>>();

                        var result = new List<Card>();

                        using (var pbar = new ProgressBar(cards.Count, "Getting card info."))
                        {
                            Regex regexModule = new Regex(trelloModuleCommentRegEx);
                            Regex regexAttachment = new Regex(trelloAttachmentRegEx);

                            foreach (var c in cards)
                            {
                                Interlocked.Add(ref currentCard, 1);

                                var labels = (from bl in boardLabels
                                              join l in c["idLabels"].ToObject<List<string>>() on bl.Id equals l
                                              select bl.Name).ToList();

                                List<(string ListName, DateTime MoveDate)> cardMoveInList = (await GetUpdateCardActionsAsync(c["id"].ToString(), ct)).Children<JObject>().Select(l => (ListName: l["data"]["listAfter"]["name"].ToString(), MoveDate: (DateTime)l["date"])).OrderBy(l => l.MoveDate).ToList();
                                List<(string ListName, int DurationDays, int DurationHours, double DurationTotalHours)> durationInList = new List<(string ListName, int DurationDays, int DurationHours, double DurationTotalHours)>();

                                for (int i = 0; i < cardMoveInList.Count - 1; i++)
                                {
                                    var timeDifference = cardMoveInList[i + 1].MoveDate - cardMoveInList[i].MoveDate;

                                    durationInList.Add(ValueTuple.Create(cardMoveInList[i].ListName, timeDifference.Days, timeDifference.Hours, timeDifference.TotalHours));
                                }

                                var members = (from bm in boardMembers
                                               join m in c["idMembers"].ToObject<List<string>>() on bm.Id equals m
                                               select bm.FullName).ToList();

                                var attachments = (await GetCardAttachmentsAsync(c["id"].ToString(), ct)).Children<JObject>().Select(l => l["url"].ToString()).ToList();
                                var gitHubUrls = new List<string>();

                                foreach (var attachment in attachments)
                                {
                                    Match match = regexAttachment.Match(attachment);

                                    if (match.Success)
                                    {
                                        gitHubUrls.Add(attachment);
                                    }
                                }

                                if (labels.Any(l => l == "housekeeping"))
                                {
                                    Interlocked.Add(ref countHouseKeepingCards, 1);

                                    result.Add(new Card(c["id"].ToString(),
                                                        string.Empty,
                                                        c["name"].ToString(),
                                                        string.Empty,
                                                        labels,
                                                        c["desc"].ToString(),
                                                        c["due"].ToString(),
                                                        durationInList,
                                                        members,
                                                        gitHubUrls,
                                                        true));
                                }
                                else
                                {
                                    Interlocked.Add(ref countCards, 1);

                                    var level = string.Empty;

                                    if (labels.Any(l => l == "low"))
                                    {
                                        Interlocked.Add(ref countCrCards, 1);
                                        level = "Промяна от ниско ниво";
                                    }
                                    else if (labels.Any(l => l == "medium"))
                                    {
                                        Interlocked.Add(ref countCrCards, 1);
                                        level = "Промяна от средно ниво";
                                    }
                                    else if (labels.Any(l => l == "high"))
                                    {
                                        Interlocked.Add(ref countCrCards, 1);
                                        level = "Промяна от високо ниво";
                                    }

                                    var comments = (await GetCardCommentActionsAsync(c["id"].ToString(), ct)).Children<JObject>().Select(l => l["data"]["text"].ToString()).ToList();
                                    var module = string.Empty;

                                    foreach (var comment in comments)
                                    {
                                        Match match = regexModule.Match(comment);

                                        if (match.Success)
                                        {
                                            module = match.Groups[1].Value.Trim();
                                            break;
                                        }
                                    }

                                    result.Add(new Card(c["id"].ToString(),
                                                        module,
                                                        c["name"].ToString(),
                                                        level,
                                                        labels,
                                                        c["desc"].ToString(),
                                                        c["due"].ToString(),
                                                        durationInList,
                                                        members,
                                                        gitHubUrls,
                                                        false));
                                }

                                pbar.Tick($"Getting card info. {currentCard} of {cards.Count}.");
                            }
                        }

                        Console.WriteLineFormatted($"Report list: {listName}", Color.LawnGreen, Color.White);
                        Console.WriteLineFormatted($"Find report cards: {countCards} (including {countCrCards} CR cards)", Color.LawnGreen, Color.White);
                        Console.WriteLineFormatted($"Find house keeping cards (with orange label in Trello): {countHouseKeepingCards}", Color.LawnGreen, Color.White);
                        Console.WriteLineFormatted($"Total find cards: {cards.Count}", Color.LawnGreen, Color.White);

                        return result;
                    }
                    else
                    {
                        return new List<Card>();
                    }
                }
                else
                {
                    Console.WriteLineFormatted("Not found list with name {0}.", Color.Red, Color.White, listName);
                    error = $"Not found list with name {listName}.";

                    return new List<Card>();
                }
            }
            else
            {
                Console.WriteLineFormatted("Not found board with name {0}.", Color.Red, Color.White, "Tasks");
                error = "Not found board with name Tasks.";

                return new List<Card>();
            }
        }

        #region Trello API requests
        private async Task<JArray> GetBoardAsync(CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/members/me/boards?key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetListsInBoardAsync(CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/boards/{trelloBoardId}/lists?cards=none&filter=open&fields=id%2Cname&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetCardsInListAsync(string trelloListId, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/lists/{trelloListId}/cards?fields=id%2CdateLastActivity%2Cdesc%2CidLabels%2Cname%2Cbadges%2Cdue%2CidChecklists%2CidMembers%2Clabels%2CshortUrl&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetBoardLabelsAsync(CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/boards/{trelloBoardId}/labels?fields=id%2Cname&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetBoardMembersAsync(CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/boards/{trelloBoardId}/members?fields=id%2CfullName&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetCardCommentActionsAsync(string cardId, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/cards/{cardId}/actions?filter=commentCard&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetUpdateCardActionsAsync(string cardId, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/cards/{cardId}/actions?filter=updateCard:idList&fields=data%2Cdate&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
        }

        private async Task<JArray> GetCardAttachmentsAsync(string cardId, CancellationToken ct)
        {
            var resp = await this.GetStringAsync($"https://api.trello.com/1/cards/{cardId}/attachments?fields=name%2Curl&key={trelloKey}&token={trelloToken}", ct);

            return JArray.Parse(resp);
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
        #endregion

        #region Objects
        private class Card
        {
            string _id;
            string _module;
            string _title;
            string _level;
            List<string> _labels;
            string _description;
            string _due;
            List<(string ListName, int DurationDays, int DurationHours, double DurationTotalHours)> _durationInLists;
            List<string> _members;
            List<string> _gitHubUrls;
            bool _isHouseKeeping;

            public Card(
                string id,
                string module,
                string title,
                string level,
                List<string> labels,
                string description,
                string due,
                List<(string ListName, int DurationDays, int DurationHours, double DurationTotalHours)> durationInLists,
                List<string> members,
                List<string> gitHubUrls,
                bool isHouseKeeping)
            {
                this._id = id;
                this._module = module;
                this._title = title;
                this._level = level;
                this._labels = labels;
                this._description = description;
                this._due = due;
                this._durationInLists = durationInLists;
                this._members = members;
                this._gitHubUrls = gitHubUrls;
                this._isHouseKeeping = isHouseKeeping;
            }

            public string Id { get { return _id; } }
            public string Module { get { return _module; } }
            public string Title { get { return _title; } }
            public string Level { get { return _level; } }
            public List<string> Labels { get { return _labels; } }
            public string Description { get { return _description; } }
            public string Due { get { return _due; } }
            public List<(string ListName, int DurationDays, int DurationHours, double DurationTotalHours)> DurationInLists { get { return _durationInLists; } }
            public List<string> Members { get { return _members; } }
            public List<string> GitHubUrls { get { return _gitHubUrls; } }
            public bool IsHouseKeeping { get { return _isHouseKeeping; } }
        }

        private class Label
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        private class Member
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("fullName")]
            public string FullName { get; set; }
        }
        #endregion
    }
}
