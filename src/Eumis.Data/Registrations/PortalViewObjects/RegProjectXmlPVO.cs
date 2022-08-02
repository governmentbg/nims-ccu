using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class RegProjectXmlPVO
    {
        public Guid Gid { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Hash { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProcedureCode { get; set; }

        public DateTime ProcedureEndingDate { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        // registered only properties
        public string RegistrationNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public RegProjectXmlRegType? RegistrationType { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public RegProjectXmlRegType? RegistrationTypeText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public RegProjectXmlRegType? RegistrationTypeTextAlt
        {
            get
            {
                return this.RegistrationType;
            }
        }

        public ProjectEvalStatus? ProjectStatus { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProjectEvalStatus? ProjectStatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProjectEvalStatus? ProjectStatusTextAlt
        {
            get
            {
                return this.ProjectStatus;
            }
        }

        public ProjectRegistrationStatus? ProjectRegStatus { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRegistrationStatus? ProjectRegStatusText { get; set; }

        public IList<MessagePVO> Messages { get; set; }

        public IList<ProjectCommunicationQuestionPVO> ProjectCommunications { get; set; }

        public IList<ProjectVersionPVO> ProjectVersions { get; set; }

        // submitted only properties
        public DateTime? SubmitDate { get; set; }

        public bool HasCommunications { get; set; }

        public bool HasNewQuestions { get; set; }

        public bool HasNewAnswers { get; set; }
    }
}
