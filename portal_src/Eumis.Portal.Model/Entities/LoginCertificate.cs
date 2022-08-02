using System;
using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class LoginCertificate
    {
        public int LoginCertificateId { get; set; }
        public System.DateTime LoginDate { get; set; }
        public string IP { get; set; }
        public byte[] CertificateFile { get; set; }
        public string CertificateIssuer { get; set; }
        public string CertificatePolicies { get; set; }
        public string CertificateSubject { get; set; }
        public string AlternativeSubject { get; set; }
        public string CertificateThumbprint { get; set; }
        public string ErrorCode { get; set; }
        public bool IsIisErrorOccurred { get; set; }
        public bool IsLoginSuccessful { get; set; }
    }
}
