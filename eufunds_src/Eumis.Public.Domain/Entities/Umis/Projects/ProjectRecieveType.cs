using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectRecieveType
    {
        [Description("На ръка")]
        InPerson = 1,

        [Description("Поща")]
        Mail = 2,

        [Description("Куриер")]
        Courier = 3,

        [Description("Факс")]
        Fax = 4,

        [Description("Електронен път")]
        Electronic = 5
    }
}
