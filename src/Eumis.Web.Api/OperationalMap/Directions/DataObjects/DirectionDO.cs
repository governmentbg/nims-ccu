using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.OperationalMap.Directions.DataObjects
{
    public class DirectionDO
    {
        public DirectionDO()
        {
        }

        public DirectionDO(Direction direction)
            : this()
        {
            this.DirectionId = direction.DirectionId;
            this.Name = direction.Name;
            this.NameAlt = direction.NameAlt;
            this.Status = direction.Status;
            this.Version = direction.Version;
        }

        public int DirectionId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public DirectionStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
