using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common
{
    [Serializable]
    public class LocalizedSelectListItem
    {
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string NameEN { get; set; }
        public string Text { get; set; }
    }
}
