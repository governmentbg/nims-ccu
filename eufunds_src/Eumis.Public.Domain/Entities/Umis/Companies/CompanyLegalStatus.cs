using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Companies
{
    public enum CompanyLegalStatus
    {
        [Description("Публично правна")]
        Public = 1,

        [Description("Частно правна")]
        Private = 2
    }
}
