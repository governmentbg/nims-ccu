using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.MapNodes.ViewObjects
{
    public class MapNodeDirectionVO
    {
        public int MapNodeDirectionId { get; set; }

        public int MapNodeId { get; set; }

        public string DirectionName { get; set; }

        public string DirectionNameAlt { get; set; }

        public string SubDirectionName { get; set; }

        public string SubDirectionNameAlt { get; set; }
    }
}
