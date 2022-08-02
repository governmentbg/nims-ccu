using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Core
{
    public enum Sign
    {
        [Description("-")]
        Negative = -1,

        [Description("+")]
        Positive = 1,
    }
}
