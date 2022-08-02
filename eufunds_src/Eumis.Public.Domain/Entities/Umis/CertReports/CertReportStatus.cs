using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public enum CertReportStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Приключен")]
        Ended = 2,

        [Description("В проверка")]
        Unchecked = 3,

        [Description("Одобрен")]
        Approved = 4,

        [Description("Частично одобрен")]
        PartialyApproved = 5,

        [Description("Неодобрен")]
        Unapproved = 6,

        [Description("Върнат")]
        Returned = 7
    }
}