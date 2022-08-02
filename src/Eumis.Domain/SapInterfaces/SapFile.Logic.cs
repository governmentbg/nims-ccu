using System;

namespace Eumis.Domain.SapInterfaces
{
    public partial class SapFile
    {
        public void MarkAsImported()
        {
            if (this.Status != SapFileStatus.New)
            {
                throw new DomainValidationException("Cannot import twice same sap file.");
            }

            this.Status = SapFileStatus.Imported;
            this.ModifyDate = DateTime.Now;
        }
    }
}
