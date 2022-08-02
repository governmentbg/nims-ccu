using System.ComponentModel.DataAnnotations.Schema;

namespace Eumis.Public.Domain.Entities
{
    public partial class Map
    {
        [NotMapped]
        public MapTypeEnum TypeEnum
        {
            get
            {
                return (MapTypeEnum) this.Type;
            }
            set
            {
                this.Type = (byte) value;
            }
        }

    }
}
