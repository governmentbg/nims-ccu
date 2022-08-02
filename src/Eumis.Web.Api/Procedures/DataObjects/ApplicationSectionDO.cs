using Eumis.Data.Procedures.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ApplicationSectionDO
    {
        public ApplicationSectionDO(IList<ApplicationSectionVO> sections, byte[] version)
        {
            this.Sections = sections;
            this.Version = version;
        }

        public IList<ApplicationSectionVO> Sections { get; set; }

        public byte[] Version { get; set; }
    }
}
