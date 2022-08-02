using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType
    {
        [Description("Микроданни участници (ФЕПНЛ)")]
        Type1 = 1,

        [Description("Микроданни участници (ЕСФ)")]
        Type2 = 2,

        [Description("Микроданни хранителни продукти")]
        Type3 = 3,

        [Description("Микроданни на АСП")]
        Type4 = 4
    }
}