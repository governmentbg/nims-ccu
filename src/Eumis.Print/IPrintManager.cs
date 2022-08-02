using Newtonsoft.Json.Linq;

namespace Eumis.Print
{
    public interface IPrintManager
    {
        byte[] Print(TemplateType templateType, PrintType printType, JObject context);
    }
}
