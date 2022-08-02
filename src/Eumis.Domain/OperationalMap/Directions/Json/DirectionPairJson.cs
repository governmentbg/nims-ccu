using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.OperationalMap.Directions
{
    public class DirectionPairJson
    {
        public DirectionPairJson()
        {
        }

        public DirectionPairJson(ProcedureDirection direction)
        {
            this.Direction = new DirectionJson()
            {
                Gid = direction.Direction.Gid,
                Name = direction.Direction.Name,
                NameAlt = direction.Direction.NameAlt,
            };

            if (direction.SubDirection != null)
            {
                this.SubDirection = new DirectionJson()
                {
                    Gid = direction.SubDirection.Gid,
                    Name = direction.SubDirection.Name,
                    NameAlt = direction.SubDirection.NameAlt,
                };
            }
        }

        public DirectionJson Direction { get; set; }

        public DirectionJson SubDirection { get; set; }
    }
}
