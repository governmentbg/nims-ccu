using Eumis.Domain.OperationalMap.Directions;
using Eumis.Rio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class DirectionPairPVO
    {
        public DirectionPairPVO()
        {
        }

        public DirectionPairPVO(DirectionPairJson pair)
        {
            this.Direction = new PrivateNomenclature()
            {
                Id = pair.Direction.Gid.ToString(),
                Name = pair.Direction.Name,
                NameEN = pair.Direction.NameAlt,
            };

            if (pair.SubDirection != null)
            {
                this.SubDirection = new PrivateNomenclature()
                {
                    Id = pair.SubDirection.Gid.ToString(),
                    Name = pair.SubDirection.Name,
                    NameEN = pair.SubDirection.NameAlt,
                };
            }
        }

        public PrivateNomenclature Direction { get; set; }

        public PrivateNomenclature SubDirection { get; set; }
    }
}
