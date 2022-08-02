using Eumis.Common.Json;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectDossierDocumentVO
    {
        public ProjectDossierDocumentType ObjectType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectDossierDocumentType ObjectTypeDesc { get; set; }

        public string ObjectDescription { get; set; }

        public FilePVO File { get; set; }
    }
}
