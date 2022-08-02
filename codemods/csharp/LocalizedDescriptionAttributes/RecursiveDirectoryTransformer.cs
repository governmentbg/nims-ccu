using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizedDescriptionAttributes
{
    class RecursiveDirectoryTransformer
    {
        bool used;
        int succeeded;
        int skipped;
        int unmodified;
        int errors;
        string targetDirectory;
        Func<string, string, string> transform;

        public RecursiveDirectoryTransformer(string targetDirectory, Func<string, string, string> transform)
        {
            this.used = false;
            this.succeeded = 0;
            this.skipped = 0;
            this.unmodified = 0;
            this.errors = 0;
            this.targetDirectory = targetDirectory;
            this.transform = transform;
        }

        public void Process()
        {
            if (this.used)
            {
                throw new Exception("This transformer has already been used! Create a new one.");
            }

            this.used = true;
            this.ProcessDirectory(this.targetDirectory, this.transform);

            Console.WriteLine($"succeeded: {this.succeeded}");
            Console.WriteLine($"skipped: {this.skipped}");
            Console.WriteLine($"unmodified: {this.unmodified}");
            Console.WriteLine($"errors: {this.errors}");
        }

        private void ProcessDirectory(string targetDirectory, Func<string, string, string> transform)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                var oldContents = File.ReadAllText(fileName);
                try
                {
                    var newContents = transform(fileName, oldContents);
                    if (string.IsNullOrEmpty(newContents))
                    {
                        this.skipped++;
                    }
                    else if (newContents == oldContents)
                    {
                        this.unmodified++;
                    }
                    else
                    {
                        File.WriteAllText(fileName, newContents);
                        this.succeeded++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error while transforming file {fileName}.\n{e.Message}");
                    this.errors++;
                }
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory, transform);
            }
        }
    }
}
