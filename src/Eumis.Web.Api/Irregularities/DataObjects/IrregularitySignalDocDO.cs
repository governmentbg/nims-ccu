using Eumis.Domain.Core;
using Eumis.Domain.Irregularities;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularitySignalDocDO
    {
        public IrregularitySignalDocDO()
        {
        }

        public IrregularitySignalDocDO(int signalId, byte[] version)
        {
            this.SignalId = signalId;
            this.Version = version;
        }

        public IrregularitySignalDocDO(IrregularitySignalDoc signalDoc, byte[] version)
        {
            this.DocumentId = signalDoc.IrregularitySignalDocId;
            this.SignalId = signalDoc.IrregularitySignalId;
            this.Description = signalDoc.Description;

            this.File = new FileDO
            {
                Key = signalDoc.FileKey,
                Name = signalDoc.FileName,
            };

            this.Version = version;
        }

        public int SignalId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
