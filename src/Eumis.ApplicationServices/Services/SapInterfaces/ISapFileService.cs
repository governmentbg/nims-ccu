using System;
using Eumis.Domain.SapInterfaces;

namespace Eumis.ApplicationServices.Services.SapInterfaces
{
    public interface ISapFileService
    {
        SapFile CreateSapFile(Guid fileKey, string fileName, SapFileType type);

        void ImportSapFile(SapFile sapFile);

        void DeleteSapFile(SapFile sapFile);
    }
}
