using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Models.Submit
{
    public class SignatureVM
    {
        public string fileKey { get; set; }
        public string fileName { get; set; }
        public string serialNumber { get; set; }
        public string effectiveDate { get; set; }
        public string expirationDate { get; set; }
        public string issuer { get; set; }
        public string subject { get; set; }
    }
}