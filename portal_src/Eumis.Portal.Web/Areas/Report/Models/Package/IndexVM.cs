using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eumis.Documents.Enums;
using Eumis.Common;
using Eumis.Documents.Contracts;
using PagedList;

namespace Eumis.Portal.Web.Areas.Report.Models.Package
{
    public class IndexVM
    {
        public StaticPagedList<ContractReportPVO> Packages { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEditSent { get; set; }
        public bool AllowConcurrencyReports { get; } = false;
        public IList<ContractProcedureApplicationSection> ProcedureApplicationSections { get; set; }
    }
}
