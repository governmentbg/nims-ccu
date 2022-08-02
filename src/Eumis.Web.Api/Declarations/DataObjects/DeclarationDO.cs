using Eumis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.Declarations.DataObjects
{
    public class DeclarationDO
    {
        public DeclarationDO()
        {
            this.Files = new List<DeclarationFileDO>();
        }

        public DeclarationDO(string createdByUser)
        {
            var currentDate = DateTime.Now;

            this.Status = DeclarationStatus.Draft;
            this.CreatedByUser = createdByUser;
            this.CreateDate = currentDate;

            this.Files = new List<DeclarationFileDO>();
        }

        public DeclarationDO(Declaration declaration, string createdByUser)
        {
            this.DeclarationId = declaration.DeclarationId;
            this.Status = declaration.Status;
            this.Name = declaration.Name;
            this.NameAlt = declaration.NameAlt;
            this.Content = declaration.Content;
            this.ContentAlt = declaration.ContentAlt;
            this.ActivationDate = declaration.ActivationDate;
            this.CreatedByUser = createdByUser;
            this.CreateDate = declaration.CreateDate;
            this.Version = declaration.Version;
            this.Files = declaration.DeclarationFiles.Select(f => new DeclarationFileDO(f)).ToList();
        }

        public int? DeclarationId { get; set; }

        public DeclarationStatus? Status { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public DateTime? ActivationDate { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public byte[] Version { get; set; }

        public IList<DeclarationFileDO> Files { get; set; }
    }
}
