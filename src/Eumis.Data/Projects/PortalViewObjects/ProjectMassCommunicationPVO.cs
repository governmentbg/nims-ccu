using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Eumis.Data.Projects.PortalViewObjects
{
    public class ProjectMassCommunicationPVO
    {
        public ProjectMassCommunicationPVO()
        {
            this.Files = new List<FilePVO>();
        }

        public ProjectMassCommunicationPVO(ProjectMassManagingAuthorityCommunication communication)
            : this()
        {
            this.Subject = new EntityGidNomPVO
            {
                Gid = Guid.NewGuid(),
                Name = communication.Subject.GetEnumDescription(new CultureInfo(SystemLocalization.Bg_BG)),
                NameAlt = communication.Subject.GetEnumDescription(new CultureInfo(SystemLocalization.En_GB)),
            };

            this.Message = communication.Message;

            communication.Documents.ForEach(x =>
            {
                this.Files.Add(new FilePVO
                {
                    Description = x.Description,
                    FileKey = x.FileKey,
                    FileName = x.FileName,
                    Name = x.Name,
                });
            });
        }

        public EntityGidNomPVO Subject { get; set; }

        public string Message { get; set; }

        public IList<FilePVO> Files { get; set; }
    }
}
