using System.ComponentModel.DataAnnotations.Schema;

namespace Eumis.Public.Domain.Entities
{
    public partial class BgMapsEntryPoint
    {
        [NotMapped]
        public NomenclatureType Type
        {
            get
            {
                return (NomenclatureType) this.NomenType;
            }
            set
            {
                this.NomenType = (byte) value;
            }
        }
    }
}
