using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class ProjectVersionPVO
    {
        public ProjectVersionPVO(
            Guid gid,
            string note,
            string noteAlt,
            DateTime createDate,
            DateTime modifyDate,
            ProjectVersionXmlStatus status)
        {
            this.Gid = gid;
            this.Note = note;
            this.NoteAlt = noteAlt;
            this.CreateDate = createDate;
            this.ModifyDate = modifyDate;
            this.Status = status;
            this.StatusText = status;
        }

        public Guid Gid { get; set; }

        public string Note { get; set; }

        public string NoteAlt { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public ProjectVersionXmlStatus Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProjectVersionXmlStatus StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProjectVersionXmlStatus StatusTextAlt
        {
            get
            {
                return this.Status;
            }
        }
    }
}
