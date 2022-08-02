using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain
{
    public partial class News
    {
        public void UpdateAttributes(string title, string content, bool emailNotification)
        {
            this.AssertIsDraft();

            this.Title = title;
            this.Content = content;
            this.EmailNotification = emailNotification;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateAttributes(string title, string titleAlt, string content, string contentAlt, string author, string authorAlt, DateTime dateFrom, DateTime dateTo)
        {
            this.AssertIsDraft();

            this.Title = title;
            this.TitleAlt = titleAlt;
            this.Content = content;
            this.ContentAlt = contentAlt;
            this.Author = author;
            this.AuthorAlt = authorAlt;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;

            this.ModifyDate = DateTime.Now;
        }

        public void AddFiles(IList<NewsFile> files)
        {
            this.AssertIsDraft();

            this.NewsFiles = this.NewsFiles.Concat(files).ToList();
        }

        public void UpdateFiles(IList<Tuple<int, Guid, string, string>> files)
        {
            this.AssertIsDraft();

            foreach (var file in files)
            {
                var oldFile = this.NewsFiles.Single(f => f.NewsFileId == file.Item1);
                oldFile.BlobKey = file.Item2;
                oldFile.Name = file.Item3;
                oldFile.Description = file.Item4;
            }
        }

        public void RemoveFiles(IList<int> fileIds)
        {
            this.AssertIsDraft();

            var files = this.NewsFiles
                .Where(f => fileIds.Contains(f.NewsFileId))
                .ToList();

            foreach (var file in files)
            {
                this.NewsFiles.Remove(file);
            }
        }

        public void Publish(DateTime dateFrom, DateTime dateTo)
        {
            this.AssertIsDraft();

            if (dateFrom > dateTo)
            {
                throw new InvalidOperationException("Invalid period");
            }

            this.Status = NewsStatus.Published;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            this.ModifyDate = DateTime.Now;
        }

        public void Archive()
        {
            if (this.Status != NewsStatus.Published)
            {
                throw new DomainValidationException("Status transition not allowed");
            }

            this.Status = NewsStatus.Archived;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            if (this.Status == NewsStatus.Draft)
            {
                throw new DomainValidationException("Status transition not allowed");
            }

            this.Status = NewsStatus.Draft;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToPublished()
        {
            if (this.Status == NewsStatus.Published)
            {
                throw new DomainValidationException("Status transition not allowed");
            }

            this.Status = NewsStatus.Published;

            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != NewsStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit news that is not in Draft status");
            }
        }
    }
}
