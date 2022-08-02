using System;
using System.Xml.Serialization;

namespace Eumis.ApplicationServices.SapInterfaces.SapDocuments
{
    [Serializable]
    public enum PaymentType
    {
        [XmlEnum(Name = "авансово")]
        Advance = 1,

        [XmlEnum(Name = "междинно")]
        Intermediate = 2,

        [XmlEnum(Name = "окончателно")]
        Final = 3,

        [XmlEnum(Name = "глоба")]
        Fine = 4,

        [XmlEnum(Name = "лихва")]
        Interest = 5,

        [XmlEnum(Name = "възстановяване при доброволно прекратяване")]
        VoluntaryReimbursement = 6,

        [XmlEnum(Name = "възстановяване при грешка")]
        MistakeReimbursement = 7,

        [XmlEnum(Name = "възстановяване при нередност")]
        IrregularityReimbursement = 8,

        [XmlEnum(Name = "банкова гаранция")]
        Bank = 9,

        [XmlEnum(Name = "касов трансфер")]
        Transfer = 10,

        [XmlEnum(Name = "")]
        Undefined = 11,
    }
}
