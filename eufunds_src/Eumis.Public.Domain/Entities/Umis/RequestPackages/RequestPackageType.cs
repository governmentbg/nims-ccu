using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.RequestPackages
{
    public enum RequestPackageType
    {
        [Description("Заявка")]
        Request = 1,

        [Description("Директна промяна")]
        DirectChange = 2
    }
}
