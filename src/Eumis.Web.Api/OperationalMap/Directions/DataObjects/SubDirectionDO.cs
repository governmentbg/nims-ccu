using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.OperationalMap.Directions.DataObjects
{
    public class SubDirectionDO
    {
        public SubDirectionDO()
        {
        }

        public SubDirectionDO(SubDirection subDirection, byte[] version)
            : this()
        {
            this.DirectionId = subDirection.DirectionId;
            this.Name = subDirection.Name;
            this.NameAlt = subDirection.NameAlt;
            this.Version = version;
        }

        public int SubDirectionId { get; set; }

        public int DirectionId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public byte[] Version { get; set; }
    }
}
