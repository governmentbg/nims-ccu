using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.CertReports;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class CertReportsNomsRepository : EntityNomsRepository<CertReport, EntityNomVO>
    {
        public CertReportsNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.CertReportId,
                t => t.OrderNum.ToString(),
                t => new EntityNomVO
                {
                    NomValueId = t.CertReportId,
                    Name = t.OrderNum.ToString(),
                })
        {
        }

        public override EntityNomVO GetNom(int nomValueId)
        {
            var predicate =
                PredicateBuilder.True<CertReport>()
                .AndPropertyEquals(t => t.CertReportId, nomValueId);

            return (from cr in this.unitOfWork.DbContext.Set<CertReport>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId
                    select new { cr, p })
                 .ToList()
                 .Select(t => new EntityNomVO
                 {
                     NomValueId = t.cr.CertReportId,
                     Name = t.cr.OrderNum + " - " + t.cr.ApprovalDate.Value.ToString("dd.MM.yyyy") + " (" + t.p.Name + ")",
                 })
                 .SingleOrDefault();
        }

        public override IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId
                    where cr.Status == CertReportStatus.Approved || cr.Status == CertReportStatus.PartialyApproved
                    orderby cr.CreateDate
                    select new
                    {
                        CertReportId = cr.CertReportId,
                        OrderNum = cr.OrderNum,
                        ApprovalDate = cr.ApprovalDate,
                        ProgrammeName = p.Name,
                    })
                .ToList()
                .Select(t => new EntityNomVO
                {
                    NomValueId = t.CertReportId,
                    Name = t.OrderNum + " - " + t.ApprovalDate.Value.ToString("dd.MM.yyyy") + " (" + t.ProgrammeName + ")",
                })
                .Where(t => string.IsNullOrEmpty(term) ? true : t.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
    }
}
