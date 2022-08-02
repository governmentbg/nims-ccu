using Eumis.Domain.Core;

namespace Eumis.Data.OperationalMap.MapNodes.ViewObjects
{
    public class MapNodeDocumentVO
    {
        public int MapNodeDocumentId { get; set; }

        public int MapNodeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
