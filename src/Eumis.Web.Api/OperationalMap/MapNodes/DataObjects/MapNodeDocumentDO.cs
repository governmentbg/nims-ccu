using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Web.Api.OperationalMap.MapNodes.DataObjects
{
    public class MapNodeDocumentDO
    {
        public MapNodeDocumentDO()
        {
        }

        public MapNodeDocumentDO(int mapNodeId, byte[] version)
        {
            this.MapNodeId = mapNodeId;
            this.Version = version;
        }

        public MapNodeDocumentDO(MapNodeDocument mapNodeDocument, byte[] version)
        {
            this.MapNodeDocumentId = mapNodeDocument.MapNodeDocumentId;
            this.MapNodeId = mapNodeDocument.MapNodeId;
            this.Name = mapNodeDocument.Name;
            this.Description = mapNodeDocument.Description;

            if (mapNodeDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = mapNodeDocument.File.Key,
                    Name = mapNodeDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int MapNodeDocumentId { get; set; }

        public int MapNodeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
