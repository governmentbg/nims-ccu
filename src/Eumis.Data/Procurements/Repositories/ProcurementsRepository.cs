using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Procurements.PortalViewObjects;
using Eumis.Data.Procurements.ViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Procurements.Repositories
{
    internal class ProcurementsRepository : AggregateRepository<Procurement>, IProcurementsRepository
    {
        public ProcurementsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Procurement, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Procurement, object>>[]
                {
                    p => p.DifferentiatedPositions.Select(x => x.Company),
                    p => p.Documents.Select(x => x.File),
                };
            }
        }

        public ProcurementInfoVO GetProcurementInfo(int procurementId)
        {
            return this.Set()
                .Where(x => x.ProcurementId == procurementId)
                .Select(x => new ProcurementInfoVO
                {
                    Name = x.Name,
                    Status = x.Status,
                })
                .FirstOrDefault();
        }

        public IList<ProcurementVO> GetProcurements()
        {
            var procurements = (from p in this.Set()

                                join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on p.ErrandAreaId equals ea.ErrandAreaId into g1
                                from ea in g1.DefaultIfEmpty()

                                join ela in this.unitOfWork.DbContext.Set<ErrandLegalAct>() on p.ErrandLegalActId equals ela.ErrandLegalActId into g2
                                from ela in g2.DefaultIfEmpty()

                                select new ProcurementVO
                                {
                                    ProcurementId = p.ProcurementId,
                                    Status = p.Status,
                                    Name = p.Name,
                                    PPANumber = p.PPANumber,
                                    ErrandArea = ea != null ? ea.Name : string.Empty,
                                    ErrandLegalAct = ela != null ? ela.Name : string.Empty,
                                    PrognosysAmount = p.PrognosysAmount,
                                }).ToList();

            return procurements;
        }

        public IList<ProcurementDocumentVO> GetProcurementDocuments(int procurementId)
        {
            var documents = (from d in this.unitOfWork.DbContext.Set<ProcurementDocument>()
                             join b in this.unitOfWork.DbContext.Set<Blob>() on d.BlobKey equals b.Key into g1
                             from b in g1.DefaultIfEmpty()
                             where d.ProcurementId == procurementId
                             select new ProcurementDocumentVO
                             {
                                 ProcurementDocumentId = d.ProcurementDocumentId,
                                 ProcurementId = d.ProcurementId,
                                 Name = d.Name,
                                 Description = d.Description,
                                 File = (b.Key == null) ? null : new FileVO
                                 {
                                     Key = b.Key,
                                     Name = b.FileName,
                                 },
                             }).ToList();

            return documents;
        }

        public IList<ProcurementDifferentiatedPositionVO> GetProcurementDifferentiatedPositions(int procurementId)
        {
            var positions = (from dp in this.unitOfWork.DbContext.Set<ProcurementDifferentiatedPosition>().Where(x => x.ProcurementId == procurementId)
                             join c in this.unitOfWork.DbContext.Set<Company>() on dp.CompanyId equals c.CompanyId
                             select new ProcurementDifferentiatedPositionVO
                             {
                                 ProcurementDifferentiatedPositionId = dp.ProcurementDifferentiatedPositionId,
                                 ProcurementId = dp.ProcurementId,
                                 Comment = dp.Comment,
                                 Name = dp.Name,
                                 CompanyName = c.Name,
                                 CompanyUinType = c.UinType,
                                 CompanyUin = c.Uin,
                             }).ToList();

            return positions;
        }

        public IList<CentralProcurementPVO> GetProcurementPVOs()
        {
            var procurements = (from p in this.Set().Where(x => x.Status == ProcurementStatus.Active)
                                join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on p.ErrandAreaId equals ea.ErrandAreaId
                                join ela in this.unitOfWork.DbContext.Set<ErrandLegalAct>() on p.ErrandLegalActId equals ela.ErrandLegalActId
                                join et in this.unitOfWork.DbContext.Set<ErrandType>() on p.ErrandTypeId equals et.ErrandTypeId

                                join pdf in this.unitOfWork.DbContext.Set<ProcurementDifferentiatedPosition>() on p.ProcurementId equals pdf.ProcurementId into g0

                                join pd in this.unitOfWork.DbContext.Set<ProcurementDocument>() on p.ProcurementId equals pd.ProcurementId into g1

                                select new CentralProcurementPVO()
                                {
                                    AnnouncedDate = p.AnnouncedDate,
                                    PPANumber = p.PPANumber,
                                    ExpectedAmount = p.ExpectedAmount,
                                    OffersDeadlineDate = p.OffersDeadlineDate,
                                    CentralProcurement = new EntityGidNomVO
                                    {
                                        Gid = p.Gid,
                                        Name = p.ShortName,
                                    },
                                    ProcurementPlan = new CentralProcurementPlan
                                    {
                                        Name = p.Name,
                                        Description = p.Description,
                                        ErrandArea = new EntityCodeNomVO
                                        {
                                            Code = ea.Code,
                                            Name = ea.Name,
                                        },
                                        ErrandLegalAct = new EntityGidNomVO
                                        {
                                            Gid = ela.Gid,
                                            Name = ela.Name,
                                        },
                                        ErrandType = new EntityCodeNomVO
                                        {
                                            Code = et.Code,
                                            Name = et.Name,
                                        },
                                    },
                                    DifferentiatedPositions = g0.Select(x => new CentralDifferentiatedPositionPVO
                                    {
                                        Comment = x.Comment,
                                        Name = x.Name,
                                        DifferentiatedPosition = new EntityGidNomVO
                                        {
                                            Gid = x.Gid,
                                            Name = x.Name,
                                        },
                                    }).ToList(),
                                    ProcurementDocuments = g1.Select(x => new CentralProcurementDocumentPVO
                                    {
                                        Name = x.Name,
                                        Description = x.Description,
                                        BlobKey = x.BlobKey,
                                        ProcurementDocument = new EntityGidNomVO
                                        {
                                            Gid = x.Gid,
                                            Name = x.Name,
                                        },
                                    }).ToList(),
                                }).ToList();

            return procurements;
        }
    }
}
