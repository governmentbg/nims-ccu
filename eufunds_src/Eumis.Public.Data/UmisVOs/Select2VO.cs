using System.Diagnostics.CodeAnalysis;

namespace Eumis.Public.Data.UmisVOs
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "JSON response")]
    public class Select2VO
    {
        public string id { get; set; }

        public string text { get; set; }
    }
}
