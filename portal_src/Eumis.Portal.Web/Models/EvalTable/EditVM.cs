using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Models.EvalTable
{
    public class EditVM : BaseVM, IEditVM<R_10023.EvalTable>, IEngineValidatable
    {
        public EvalTableGroupsWrapper EvalTableGroupsWrapper { get; set; }
        public List<R_10018.AttachedDocument> AttachedDocumentCollection { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10023.EvalTable table)
        {
            this.EvalTableGroupsWrapper = new EvalTableGroupsWrapper();

            this.EvalTableGroupsWrapper.EvalTableGroupCollection = table.EvalTableGroupCollection;
            this.EvalTableGroupsWrapper.Type = table.type;
            this.EvalTableGroupsWrapper.Limit = table.Limit;
            this.EvalTableGroupsWrapper.HasGroups = table.EvalTableGroupCollection != null && table.EvalTableGroupCollection.Count > 0;
            this.EvalTableGroupsWrapper.IsLimitValid = table.IsLimitValid;
            this.AttachedDocumentCollection = table.AttachedDocumentCollection;
        }

        public R_10023.EvalTable Set(R_10023.EvalTable table)
        {
            table.EvalTableGroupCollection = new R_10023.EvalTableGroupCollection();

            if(this.EvalTableGroupsWrapper != null && this.EvalTableGroupsWrapper.EvalTableGroupCollection != null)
                table.EvalTableGroupCollection.AddRange(this.EvalTableGroupsWrapper.EvalTableGroupCollection);

            table.EvalTableGroupCollection.Type = table.type;

            table.Limit = this.EvalTableGroupsWrapper != null ? this.EvalTableGroupsWrapper.Limit : 0.00m;
            table.EvalTableGroupCollection.Limit = table.Limit;
            table.AttachedDocumentCollection = new R_10023.AttachedDocumentCollection();
            if(this.AttachedDocumentCollection != null)
                table.AttachedDocumentCollection.AddRange(this.AttachedDocumentCollection);

            return table;
        }

        #endregion
    }

    public class EvalTableGroupsWrapper
    {
        public R_09993.EvalTypeNomenclature Type { get; set; }
        public decimal Limit { get; set; }
        public bool IsLimitValid { get; set; }
        public bool HasGroups { get; set; }
        public List<R_10022.EvalTableGroup> EvalTableGroupCollection { get; set; }
    }
}