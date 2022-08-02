using System;
using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class ProjectCommunicationAnswerPVO
    {
        public ProjectCommunicationAnswerPVO(ProjectCommunicationAnswer answer)
        {
            this.AnswerGid = answer.Gid;
            this.AnswerDate = answer.Answer.MessageDate;
            this.ReadDate = answer.ReadDate;
            this.OrderNum = answer.OrderNum;
            this.Hash = answer.Answer.Hash;

            this.Status = answer.Status;
            this.StatusText = answer.Status;
        }

        public Guid AnswerGid { get; set; }

        public int OrderNum { get; set; }

        public DateTime? AnswerDate { get; set; }

        public DateTime? ReadDate { get; set; }

        public ProjectCommunicationAnswerStatus Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProjectCommunicationAnswerStatus StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProjectCommunicationAnswerStatus StatusTextAlt
        {
            get
            {
                return this.Status;
            }
        }

        public string Hash { get; set; }
    }
}
