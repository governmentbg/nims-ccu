using Eumis.Common.Json;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectMonitorstatRequestDO
    {
        public ProjectMonitorstatRequestDO()
        {
            this.Responses = new List<ProjectMonitorstatResponseVO>();
        }

        public ProjectMonitorstatRequestDO(
            int projectId,
            int projectVersionXmlId,
            byte[] projectVersion,
            string companyUin,
            UinType companyUinType)
            : this()
        {
            this.Version = projectVersion;
            this.Status = ProjectMonitorstatRequestStatus.Draft;
            this.ProjectId = projectId;
            this.ProjectVersionXmlId = projectVersionXmlId;
            this.CompanyUin = companyUin;
            this.CompanyUinType = companyUinType;
        }

        public ProjectMonitorstatRequestDO(
            ProjectMonitorstatRequest request,
            byte[] projectVersion,
            IList<ProjectMonitorstatResponse> responses,
            Guid? projectVersionXmlFileBlobKey)
            : this(request.ProjectId, request.ProjectVersionXmlId, projectVersion, request.CompanyUin, request.CompanyUinType.Value)
        {
            this.ProjectMonitorstatRequestId = request.ProjectMonitorstatRequestId;
            this.ProcedureMonitorstatRequestId = request.ProcedureMonitorstatRequestId;
            this.ProjectVersionXmlId = request.ProjectVersionXmlId;
            this.ProjectVersionXmlFileId = request.ProjectVersionXmlFileId;
            this.ProjectVersionXmlFileBlobKey = projectVersionXmlFileBlobKey;
            this.ProgrammeDeclarationId = request.ProgrammeDeclarationId;
            this.DeclarationBlobKey = request.DeclarationBlobKey;
            this.Status = request.Status;
            this.ForeignGid = request.ForeignGid?.ToString();

            this.Responses = responses.Select(r => new ProjectMonitorstatResponseVO(r)).ToList();
        }

        public int ProjectMonitorstatRequestId { get; set; }

        public int ProjectId { get; set; }

        public int ProcedureMonitorstatRequestId { get; set; }

        public int ProjectVersionXmlId { get; set; }

        public int? ProjectVersionXmlFileId { get; set; }

        public Guid? ProjectVersionXmlFileBlobKey { get; set; }

        public int? ProgrammeDeclarationId { get; set; }

        public Guid? DeclarationBlobKey { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public ProjectMonitorstatRequestStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectMonitorstatRequestStatus StatusDescr
        {
            get
            {
                return this.Status;
            }
        }

        public string ForeignGid { get; }

        public byte[] Version { get; set; }

        public IList<ProjectMonitorstatResponseVO> Responses { get; set; }
    }
}
