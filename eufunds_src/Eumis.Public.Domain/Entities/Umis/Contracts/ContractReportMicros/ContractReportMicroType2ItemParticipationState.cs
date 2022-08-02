using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemParticipationState
    {
        [Description("лицето е напуснало операцията преди планирания край на дейностите")]
        LeftBeforeTheEnd = 1,

        [Description("лицето е участвало до планирания край на дейностите")]
        StayedUntilTheEnd = 2,

        [Description("лицето е участвало до планирания край на дейностите (обучение по ОПДУ) с получаване на сертификат")]
        HasCertificate = 3,

        [Description("лицето е участвало до планирания край на дейностите (обучение по ОПДУ) без получаване на сертификат")]
        NoCertificate = 4,

        [Description("лицето е участвало до планирания край на дейностите (обучение по ОПДУ), за които не е предвидено издаването на сертификат")]
        NoCertificateAtAll = 5
    }
}
