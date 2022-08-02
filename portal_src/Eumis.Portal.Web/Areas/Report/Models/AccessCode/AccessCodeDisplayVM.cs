using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Areas.Report.Models.AccessCode
{
    public class AccessCodeDisplayVM
    {
        public ContractRegistrationAccessCodePVO User { get; set; }
        public bool IsSuccess { get; set; }
    }
}