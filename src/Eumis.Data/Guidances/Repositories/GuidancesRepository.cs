using Eumis.Common.Db;
using Eumis.Data.Guidances.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.Guidances;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Guidances.Repositories
{
    internal class GuidancesRepository : AggregateRepository<Guidance>, IGuidancesRepository
    {
        public GuidancesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<GuidanceDataVO> GetGuidances()
        {
            return (from g in this.unitOfWork.DbContext.Set<Guidance>()
                    join u in this.unitOfWork.DbContext.Set<User>() on g.CreatedByUserId equals u.UserId
                    orderby g.CreateDate descending
                    select new
                    {
                        g.GuidanceId,
                        g.Description,
                        g.Module,
                        g.BlobKey,
                        g.FileName,
                        u.Username,
                        u.Fullname,
                        g.CreateDate,
                    })
                    .ToList()
                    .Select(o => new GuidanceDataVO
                    {
                        GuidanceId = o.GuidanceId,
                        Description = o.Description,
                        Module = o.Module,
                        CreateDate = o.CreateDate,
                        Creator = o.Fullname + "(" + o.Username + ")",
                        File = new FileVO
                        {
                            Key = o.BlobKey,
                            Name = o.FileName,
                        },
                    })
                    .ToList();
        }

        public IList<GuidanceVO> GetGuidances(GuidanceModule module)
        {
            return (from g in this.unitOfWork.DbContext.Set<Guidance>()
                    where g.Module == module
                    orderby g.CreateDate descending
                    select new GuidanceVO
                    {
                        GuidanceId = g.GuidanceId,
                        Description = g.Description,
                        FileName = g.FileName,
                        FileKey = g.BlobKey,
                    }).ToList();
        }

        public bool ExistsGuidanceWithKey(Guid fileKey)
        {
            return (from g in this.unitOfWork.DbContext.Set<Guidance>()
                    where g.BlobKey == fileKey
                    select g.GuidanceId).Any();
        }
    }
}
