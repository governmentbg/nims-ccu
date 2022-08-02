//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Eumis.Common.Helpers;
using Eumis.Common.Linq;
using Eumis.Common.Validation;
using Eumis.Documents;
using Eumis.Documents.Interfaces;
using Eumis.Documents.Validation;
using R_10018;

namespace R_10026
{
    public partial class EvalSheet : IEumisDocument, IEumisDocumentWithFiles, ILocalValidatable
    {
        string IEumisDocument.Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        DateTime IEumisDocument.CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        DateTime IEumisDocument.ModificationDate
        {
            get
            {
                return this.modificationDate;
            }
            set
            {
                this.modificationDate = value;
            }
        }

        public IEnumerable<AttachedDocument> Files
        {
            get
            {
                return EnumerableExtensions.Concat(
                    this.GetFiles(d => d.AttachedDocumentCollection),
                    this.GetFiles(d => d.EvalTableAttachedDocumentCollection));
            }
        }

        public static EvalSheet Init(R_10023.EvalTable table)
        {
            if (table == null)
                table = new R_10023.EvalTable();

            EvalSheet sheet = new EvalSheet()
            {
                type = table.type,
                Limit = table.Limit,
                EvalSheetGroupCollection = new EvalSheetGroupCollection(),
                EvalTableAttachedDocumentCollection = new R_10026.AttachedDocumentCollection(),
                AttachedDocumentCollection = new R_10026.AttachedDocumentCollection()
            };

            sheet.EvalTableAttachedDocumentCollection.AddRange(table.AttachedDocumentCollection);

            foreach (var tableGroup in table.EvalTableGroupCollection)
            {
                var sheetGroup = new R_10025.EvalSheetGroup();

                sheetGroup.Name = tableGroup.Name;
                sheetGroup.Limit = tableGroup.Limit;
                sheetGroup.EvalSheetCriteriaCollection = new R_10025.EvalSheetCriteriaCollection();

                foreach (var tableCriteria in tableGroup.EvalTableCriteriaCollection)
                {
                    var sheetCriteria = new R_10024.EvalSheetCriteria();
                    sheetCriteria.EvalTableCriteria = tableCriteria;

                    sheetGroup.EvalSheetCriteriaCollection.Add(sheetCriteria);
                }

                sheet.EvalSheetGroupCollection.Add(sheetGroup);
            }

            sheet.EvalSheetGroupCollection.Init(sheet);

            #region Metadata

            sheet.createDate = DateTime.Now;

            sheet.id = Guid.NewGuid().ToString();

            #endregion

            return sheet;
        }

        public static EvalSheet Load(EvalSheet sheet)
        {
            if (sheet == null)
                throw new ArgumentNullException("Missing EvalSheet.");

            if (sheet.EvalSheetGroupCollection == null)
                throw new ArgumentNullException("Missing EvalSheetGroupCollection.");

            sheet.EvalSheetGroupCollection.Init(sheet);

            if (sheet.EvalTableAttachedDocumentCollection == null)
                sheet.EvalTableAttachedDocumentCollection = new AttachedDocumentCollection();

            if (sheet.AttachedDocumentCollection == null)
                sheet.AttachedDocumentCollection = new AttachedDocumentCollection();

            #region Metadata

            sheet.createDate = DateTime.Now;

            sheet.id = Guid.NewGuid().ToString();

            #endregion

            return sheet;
        }

        [XmlIgnore]
        public List<ModelValidationResultExtended> LocalValidationErrors { get; set; }

        [XmlIgnore]
        public decimal WeightDecimal
        {
            get
            {
                decimal result = 0.00m;

                if(this.EvalSheetGroupCollection != null)
                { 
                    foreach (var criteria in this.EvalSheetGroupCollection)
                    {
                        result += criteria.WeightDecimal;
                    }
                }

                return result;
            }
        }

        [XmlIgnore]
        public string WeightTotal
        {
            get
            {
                return DataUtils.DecimalToStringDecimalPointSpace(this.WeightDecimal);
            }
        }

        [XmlIgnore]
        public string ProjectName { get; set; }

        [XmlIgnore]
        public string ProjectRegNumber { get; set; }

        [XmlIgnore]
        public string AssessorName { get; set; }

        [XmlIgnore]
        public string ProcedureName { get; set; }

        [XmlIgnore]
        public string CompanyName { get; set; }

        [XmlIgnore]
        public string EvalTableTypeText { get; set; }
    }
}