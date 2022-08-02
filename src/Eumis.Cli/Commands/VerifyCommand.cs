using System;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;

namespace Eumis.Cli
{
    public class VerifyCommand : ICommand
    {
        public string Name { get; } = "verify";

        public void Configure(CommandLineApplication app, CancellationToken stopped)
        {
            var fileArg = app.Argument("file", "Path to the isun file whose signature to verify.");

            app.OnExecute(() =>
            {
                string file = fileArg.Value;

                if (string.IsNullOrEmpty(file) || !File.Exists(file))
                {
                    Console.WriteLine("Invalid file!");
                    app.ShowHelp();

                    return 1;
                }

                if (!File.Exists($"{file}.p7s"))
                {
                    Console.WriteLine($"Missing signature file! Looking for {file}.p7s");
                    app.ShowHelp();

                    return 1;
                }

                if (this.Verify(File.ReadAllBytes(file), File.ReadAllBytes($"{file}.p7s")))
                {
                    Console.WriteLine("Signature OK!");
                }
                else
                {
                    Console.WriteLine("Invalid signature!");
                }

                return 0;
            });
        }

        private bool Verify(byte[] data, byte[] signature)
        {
            try
            {
                var contentInfo = new ContentInfo(data);

                // Create a new, detached SignedCms message.
                SignedCms signedCms = new SignedCms(contentInfo, true);

                // encodedMessage is the encoded message received from 
                // the sender.
                signedCms.Decode(signature);

                // Verify the signature without validating the 
                // certificate.
                signedCms.CheckSignature(true);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
