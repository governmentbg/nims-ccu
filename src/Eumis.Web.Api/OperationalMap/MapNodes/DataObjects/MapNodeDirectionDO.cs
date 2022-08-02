using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.OperationalMap.MapNodes
{
    public class MapNodeDirectionDO
    {
        public MapNodeDirectionDO()
        {
        }

        public MapNodeDirectionDO(int programmeId, byte[] version)
            : this()
        {
            this.MapNodeId = programmeId;
            this.Version = version;
        }

        public MapNodeDirectionDO(MapNodeDirection mapNodeDirection, byte[] version)
            : this(mapNodeDirection.MapNodeId, version)
        {
            this.MapNodeDirectionId = mapNodeDirection.MapNodeDirectionId;
            this.DirectionId = mapNodeDirection.DirectionId;
            this.SubDirectionId = mapNodeDirection.SubDirectionId;
        }

        public int MapNodeDirectionId { get; set; }

        public int MapNodeId { get; set; }

        public int? DirectionId { get; set; }

        public int? SubDirectionId { get; set; }

        public byte[] Version { get; set; }
    }
}
