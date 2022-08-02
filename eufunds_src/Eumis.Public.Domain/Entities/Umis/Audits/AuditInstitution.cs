using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public enum AuditInstitution
    {
        [Description("Сметна палата")]
        CourtОfAuditors = 1,

        [Description("Звено за вътрешен одит")]
        Internal = 2,

        [Description("АДФИ")]
        Adfi = 3,

        [Description("ОО")]
        OO = 4,

        [Description("ЕК")]
        EK = 5,

        [Description("Европейска сметна палата")]
        EuropeanCourtОfAuditors = 6,

        [Description("Друга")]
        Other = 7
    }
}
