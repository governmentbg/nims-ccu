using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectVersionVO
    {
        public int ProjectVersionId { get; set; }

        public int ProjectId { get; set; }

        public Guid XmlGid { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectVersionXmlStatus Status { get; set; }

        public string CreateNote
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.CreateNoteBg, this.CreateNoteEn);
            }
        }

        [JsonIgnore]
        public string CreateNoteEn { get; set; }

        [JsonIgnore]
        public string CreateNoteBg { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public InternalFileVO ProjectFile { get; set; }

        public IList<InternalFileVO> ProjectFileSignatures { get; set; }
    }
}
