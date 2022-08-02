using Newtonsoft.Json.Linq;

namespace Eumis.Print
{
    internal interface ITemplate
    {
        byte[] Print(PrintType printType, JObject context);
    }
}
