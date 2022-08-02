using System.ComponentModel;

namespace Eumis.Documents.Contracts
{
    public enum ContractReportMicroType
    {
        [Description("Микроданни участници (ФЕПНЛ)")]
        Type1 = 1,

        [Description("Микроданни участници (ЕСФ)")]
        Type2 = 2,

        [Description("Микроданни хранителни продукти")]
        Type3 = 3,

        [Description("Микроданни АСП")]
        Type4 = 4
    }
}