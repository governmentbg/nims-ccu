using Eumis.Domain.Core;
using Eumis.Domain.Irregularities;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityVersionDocDO
    {
        public IrregularityVersionDocDO()
        {
        }

        public IrregularityVersionDocDO(int versionId, byte[] version)
        {
            this.VersionId = versionId;
            this.Version = version;
        }

        public IrregularityVersionDocDO(IrregularityVersionDoc signalDoc, IrregularityVersionStatus? versionStatus, byte[] version)
        {
            this.DocumentId = signalDoc.IrregularityVersionDocId;
            this.VersionId = signalDoc.IrregularityVersionId;
            this.Description = signalDoc.Description;

            this.File = new FileDO
            {
                Key = signalDoc.FileKey,
                Name = signalDoc.FileName,
            };

            this.VersionStatus = versionStatus;
            this.Version = version;
        }

        public int VersionId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public IrregularityVersionStatus? VersionStatus { get; set; }

        public byte[] Version { get; set; }
    }
}
