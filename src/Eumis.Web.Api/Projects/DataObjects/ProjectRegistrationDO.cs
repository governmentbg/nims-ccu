using System;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectRegistrationDO
    {
        public ProjectRegistrationDO()
        {
            this.HasVersions = false;
            this.HasRegistration = false;
        }

        public ProjectRegistrationDO(Project project, bool hasVersions, bool hasRegistration)
        {
            var regDate = project.RegDate;
            this.RegDate = regDate.Date;
            this.RegTime = ((regDate.Hour * 60) + regDate.Minute) * 60000;

            this.ProjectId = project.ProjectId;
            this.ProcedureId = project.ProcedureId;
            this.ProjectTypeId = project.ProjectTypeId;
            this.CompanyId = project.CompanyId;
            this.CompanyName = project.CompanyName;
            this.CompanyNameAlt = project.CompanyNameAlt;
            this.CompanyUin = project.CompanyUin;
            this.CompanyUinType = project.CompanyUinType;
            this.Name = project.Name;
            this.NameAlt = project.NameAlt;
            this.RegistrationStatus = project.RegistrationStatus;
            this.RegNumber = project.RegNumber;
            this.RecieveType = project.RecieveType;
            this.RecieveDate = project.RecieveDate;
            this.SubmitDate = project.SubmitDate;
            this.StoragePlace = project.StoragePlace;
            this.Originals = project.Originals;
            this.Copies = project.Copies;
            this.Notes = project.Notes;
            this.HasVersions = hasVersions;
            this.HasRegistration = hasRegistration;

            this.Version = project.Version;
        }

        public int ProjectId { get; set; }

        public int ProcedureId { get; set; }

        public int? RegProjectXmlId { get; set; }

        public int? ProjectTypeId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public ProjectRegistrationStatus? RegistrationStatus { get; set; }

        public string RegNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public int? RegTime { get; set; }

        public ProjectRecieveType? RecieveType { get; set; }

        public DateTime? RecieveDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public string StoragePlace { get; set; }

        public int? Originals { get; set; }

        public int? Copies { get; set; }

        public string Notes { get; set; }

        public bool HasVersions { get; set; }

        public bool HasRegistration { get; set; }

        public byte[] Version { get; set; }
    }
}
