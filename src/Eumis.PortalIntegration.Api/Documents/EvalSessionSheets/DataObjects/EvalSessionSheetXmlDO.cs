using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Data.EvalSessions.PortalViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.PortalIntegration.Api.Core;
using Newtonsoft.Json;

namespace Eumis.PortalIntegration.Api.Documents.EvalSessionSheets.DataObjects
{
    public class EvalSessionSheetXmlDO : XmlDO
    {
        public EvalSessionSheetXmlDO()
        {
        }

        public EvalSessionSheetXmlDO(EvalSessionSheetXmlData evalSessionSheetXmlData)
        {
            this.ModifyDate = evalSessionSheetXmlData.ModifyDate;
            this.Version = evalSessionSheetXmlData.Version;
            this.Xml = evalSessionSheetXmlData.Xml;

            this.ProjectName = evalSessionSheetXmlData.SheetData.ProjectName;
            this.ProjectNameAlt = evalSessionSheetXmlData.SheetData.ProjectNameAlt;
            this.ProjectRegNumber = evalSessionSheetXmlData.SheetData.ProjectRegNumber;
            this.AssessorName = evalSessionSheetXmlData.SheetData.AssessorName;
            this.ProcedureName = evalSessionSheetXmlData.SheetData.ProcedureName;
            this.ProcedureNameAlt = evalSessionSheetXmlData.SheetData.ProcedureNameAlt;
            this.CompanyName = evalSessionSheetXmlData.SheetData.CompanyName;
            this.CompanyNameAlt = evalSessionSheetXmlData.SheetData.CompanyNameAlt;
            this.EvalTableTypeText = evalSessionSheetXmlData.SheetData.EvalTableType;
        }

        public EvalSessionSheetXmlDO(EvalSessionSheetXml evalSessionSheetXml, EvalSessionSheetData evalSessionSheetData)
        {
            this.ModifyDate = evalSessionSheetXml.ModifyDate;
            this.Version = evalSessionSheetXml.Version;
            this.Xml = evalSessionSheetXml.Xml;

            this.ProjectName = evalSessionSheetData.ProjectName;
            this.ProcedureNameAlt = evalSessionSheetData.ProcedureNameAlt;
            this.ProjectRegNumber = evalSessionSheetData.ProjectRegNumber;
            this.AssessorName = evalSessionSheetData.AssessorName;
            this.ProcedureName = evalSessionSheetData.ProcedureName;
            this.ProcedureNameAlt = evalSessionSheetData.ProcedureNameAlt;
            this.CompanyName = evalSessionSheetData.CompanyName;
            this.CompanyNameAlt = evalSessionSheetData.CompanyNameAlt;
            this.EvalTableTypeText = evalSessionSheetData.EvalTableType;
        }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public string ProjectRegNumber { get; set; }

        public string AssessorName { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProcedureEvalTableType? EvalTableTypeText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProcedureEvalTableType? EvalTableTypeTextAlt
        {
            get
            {
                return this.EvalTableTypeText;
            }
        }
    }
}
