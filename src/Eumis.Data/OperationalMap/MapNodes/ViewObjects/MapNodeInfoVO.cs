using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects
{
    public class MapNodeInfoVO
    {
        public MapNodeStatus Status { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string ParentShortName { get; set; }
    }
}
