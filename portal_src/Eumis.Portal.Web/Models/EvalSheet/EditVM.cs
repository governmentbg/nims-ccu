using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Models.EvalSheet
{
    public class EditVM : BaseVM, IEditVM<R_10026.EvalSheet>, IEngineValidatable
    {
        public EvalSheetGroupsWrapper EvalSheetGroupsWrapper { get; set; }
        public List<R_10018.AttachedDocument> EvalTableAttachedDocumentCollection { get; set; }
        public List<R_10018.AttachedDocument> AttachedDocumentCollection { get; set; }
        public string Note { get; set; }

        public string ProjectName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string AssessorName { get; set; }

        public string ProcedureName { get; set; }

        public string CompanyName { get; set; }

        public string EvalTableTypeText { get; set; }


        #region Get Set

        public EditVM() { }

        public EditVM(R_10026.EvalSheet sheet)
        {
            this.ProjectName = sheet.ProjectName;
            this.ProjectRegNumber = sheet.ProjectRegNumber;
            this.AssessorName = sheet.AssessorName;

            this.ProcedureName = sheet.ProcedureName;
            this.CompanyName = sheet.CompanyName;
            this.EvalTableTypeText = sheet.EvalTableTypeText;

            this.EvalSheetGroupsWrapper = new EvalSheetGroupsWrapper();
            this.EvalSheetGroupsWrapper.EvalSheetGroupCollection = sheet.EvalSheetGroupCollection;
            this.EvalSheetGroupsWrapper.Type = sheet.type;
            this.EvalSheetGroupsWrapper.Limit = sheet.Limit;
            this.EvalSheetGroupsWrapper.Total = sheet.Total;
            this.EvalSheetGroupsWrapper.IsTotalValid = sheet.IsTotalValid;
            this.EvalSheetGroupsWrapper.IsManual = sheet.IsManual;
            this.EvalSheetGroupsWrapper.ReasonManual = sheet.ReasonManual;
            this.EvalSheetGroupsWrapper.IsSuccess = sheet.IsSuccess;

            this.EvalSheetGroupsWrapper.WeightTotal = sheet.WeightTotal;

            this.AttachedDocumentCollection = sheet.AttachedDocumentCollection;
            this.EvalTableAttachedDocumentCollection = sheet.EvalTableAttachedDocumentCollection;

            this.Note = sheet.Note;
        }

        public R_10026.EvalSheet Set(R_10026.EvalSheet sheet)
        {
            for (int i = 0; i < sheet.EvalSheetGroupCollection.Count; i++)
            {
                for (int j = 0; j < sheet.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection.Count; j++)
                {
                    sheet.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].Accept = this.EvalSheetGroupsWrapper.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].Accept;
                    sheet.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].Evaluation = this.EvalSheetGroupsWrapper.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].Evaluation;

                    sheet.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].Note = this.EvalSheetGroupsWrapper.EvalSheetGroupCollection[i].EvalSheetCriteriaCollection[j].Note;
                }

                if(sheet.type.Equals(R_09993.EvalTypeNomenclature.Weight))
                    sheet.EvalSheetGroupCollection[i].Total = this.EvalSheetGroupsWrapper.EvalSheetGroupCollection[i].Total;
            }

            sheet.Total = this.EvalSheetGroupsWrapper.Total;
            sheet.IsManual = this.EvalSheetGroupsWrapper.IsManual;
            sheet.ReasonManual = this.EvalSheetGroupsWrapper.ReasonManual;
            sheet.IsSuccess = this.EvalSheetGroupsWrapper.IsSuccess;

            sheet.EvalSheetGroupCollection.Init(sheet);

            sheet.AttachedDocumentCollection = new R_10026.AttachedDocumentCollection();
            if (this.AttachedDocumentCollection != null)
                sheet.AttachedDocumentCollection.AddRange(this.AttachedDocumentCollection);

            sheet.Note = this.Note;

            return sheet;
        }

        #endregion
    }

    public class EvalSheetGroupsWrapper
    {
        public R_09993.EvalTypeNomenclature Type { get; set; }
        public decimal Limit { get; set; }
        public decimal Total { get; set; }
        public bool IsTotalValid { get; set; }
        public List<R_10025.EvalSheetGroup> EvalSheetGroupCollection { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsManual { get; set; }
        public string ReasonManual { get; set; }

        public string WeightTotal { get; set; }
    }
}