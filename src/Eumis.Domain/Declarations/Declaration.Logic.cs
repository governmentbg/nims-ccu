using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain
{
    public partial class Declaration
    {
        public void UpdateAttributes(
            string name,
            string nameAlt,
            string content,
            string contentAlt)
        {
            this.AssertIsDraft();

            this.Name = name;
            this.NameAlt = nameAlt;
            this.Content = content;
            this.ContentAlt = contentAlt;

            this.ModifyDate = DateTime.Now;
        }

        public void AddFiles(IList<DeclarationFile> files)
        {
            this.AssertIsDraft();

            this.DeclarationFiles = this.DeclarationFiles.Concat(files).ToList();
        }

        public void UpdateFiles(IList<Tuple<int, Guid, string, string>> files)
        {
            this.AssertIsDraft();

            foreach (var file in files)
            {
                var oldFile = this.DeclarationFiles.Single(f => f.DeclarationFileId == file.Item1);
                oldFile.BlobKey = file.Item2;
                oldFile.Name = file.Item3;
                oldFile.Description = file.Item4;
            }
        }

        public void RemoveFiles(IList<int> fileIds)
        {
            this.AssertIsDraft();

            var files = this.DeclarationFiles
                .Where(f => fileIds.Contains(f.DeclarationFileId))
                .ToList();

            foreach (var file in files)
            {
                this.DeclarationFiles.Remove(file);
            }
        }

        public void Publish(DateTime activationDate)
        {
            this.AssertIsDraft();

            this.Status = DeclarationStatus.Published;
            this.ActivationDate = activationDate;

            this.ModifyDate = DateTime.Now;
        }

        public void Archive()
        {
            if (this.Status != DeclarationStatus.Published)
            {
                throw new DomainValidationException("Status transition not allowed");
            }

            this.Status = DeclarationStatus.Archived;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != DeclarationStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit declaration that is not in Draft status");
            }
        }
    }
}
