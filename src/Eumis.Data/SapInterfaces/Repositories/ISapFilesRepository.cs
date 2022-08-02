using System.Collections.Generic;
using Eumis.Data.SapInterfaces.ViewObjects;
using Eumis.Domain.SapInterfaces;

namespace Eumis.Data.SapInterfaces.Repositories
{
    public interface ISapFilesRepository : IAggregateRepository<SapFile>
    {
        IList<SapFileVO> GetSapFiles(SapFileStatus? status = null, SapFileType? type = null);

        IList<SapPaidAmount> GetCorrectPaidAmounts(int sapFileId);

        IList<SapFilePaidAmountVO> GetSapPaidAmounts(int sapFileId);

        IList<SapFileDistributedLimitVO> GetSapDistributedLimits(int sapFileId);

        IList<SapDistributedLimit> GetCorrectDistributedLimits(int sapFileId);

        SapFileInfoVO GetSapFileInfo(int sapFileId);
    }
}
