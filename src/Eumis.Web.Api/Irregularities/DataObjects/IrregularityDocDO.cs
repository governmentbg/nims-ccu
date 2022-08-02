using Eumis.Domain.Core;
using Eumis.Domain.Irregularities;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityDocDO
    {
        public IrregularityDocDO()
        {
        }

        public IrregularityDocDO(int irregularityId, byte[] version)
        {
            this.IrregularityId = irregularityId;
            this.Version = version;
        }

        public IrregularityDocDO(IrregularityDoc doc, byte[] version)
        {
            this.DocumentId = doc.IrregularityDocId;
            this.IrregularityId = doc.IrregularityId;
            this.Description = doc.Description;

            this.File = new FileDO
            {
                Key = doc.FileKey,
                Name = doc.FileName,
            };

            this.Version = version;
        }

        public int IrregularityId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
