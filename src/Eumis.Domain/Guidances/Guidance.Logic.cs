using System;

namespace Eumis.Domain.Guidances
{
    public partial class Guidance
    {
        public void UpdateAttributes(
            GuidanceModule module,
            string description,
            Guid fileKey,
            string fileName)
        {
            this.Module = module;
            this.Description = description;
            this.BlobKey = fileKey;
            this.FileName = fileName;

            this.ModifyDate = DateTime.Now;
        }
    }
}
