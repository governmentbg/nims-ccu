using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Areas.Report.Models.MicroData
{
    public class MicroStaticPagedList<T> : StaticPagedList<T>
    {
        public string DocNumber { get; set; }
        public string ContractNumber { get; set; }
        
        public MicroStaticPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount, string docNumber, string contractNumber) 
            : base(subset, pageNumber, pageSize, totalItemCount)
        {
            this.DocNumber = docNumber;
            this.ContractNumber = contractNumber;
        }
    }
}