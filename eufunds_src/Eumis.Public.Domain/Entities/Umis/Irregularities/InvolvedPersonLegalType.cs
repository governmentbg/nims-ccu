using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum InvolvedPersonLegalType
    {
        [Description("Физическо лице")]
        Person = 1,

        [Description("Юридическо лице")]
        LegalPerson = 2
    }
}
