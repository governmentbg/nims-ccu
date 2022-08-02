using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Certificates
{
    public class CertificateSubjectInfo
    {
        [JsonProperty("CN")]
        public string Name { get; set; }

        [JsonProperty("E")]
        public string Email { get; set; }

        [JsonProperty("C")]
        public string Country { get; set; }

        [JsonProperty("S")]
        public string Settlement { get; set; }

        [JsonProperty("L")]
        public string Location { get; set; }

        [JsonProperty("O")]
        public string Organization { get; set; }
    }
}
