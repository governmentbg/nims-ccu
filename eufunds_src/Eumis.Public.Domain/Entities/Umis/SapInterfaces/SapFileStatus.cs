using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces
{
    public enum SapFileStatus
    {
        [Description("Новосъздаден")]
        New = 1,

        [Description("Импортиран")]
        Imported = 2
    }
}
