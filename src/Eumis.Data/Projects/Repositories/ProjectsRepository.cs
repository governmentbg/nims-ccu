using Eumis.Common;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectsRepository : AggregateRepository<Project>, IProjectsRepository
    {
        public ProjectsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Project, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Project, object>>[]
                {
                    c => c.MonitorstatRequests,
                    c => c.MonitorstatRequests.Select(r => r.DeclarationFile),
                };
            }
        }

        public Project FindByRegNumber(string regNumber)
        {
            return this.Set()
                .Where(p => p.RegNumber == regNumber)
                .SingleOrDefault();
        }

        public List<Project> GetProjectsByUin(int procedureId, string uin, UinType uinType)
        {
            var projects = new List<Project>();

            var predicate = PredicateBuilder.True<Project>()
                .And(x => x.ProcedureId == procedureId)
                .And(x => x.CompanyUin == uin);

            var projectIds = (from p in this.Set().Where(predicate)
                              select p.ProjectId)
                              .Distinct()
                              .ToList();

            // Aggregate root collections must be loaded
            projectIds.ForEach(x => projects.Add(this.Find(x)));

            return projects;
        }

        public IList<Project> FindAll(int[] projectIds)
        {
            return this.Set()
                .Where(t => projectIds.Contains(t.ProjectId))
                .ToList();
        }

        public IList<ProjectRegistrationsVO> GetProjectRegistrations(
            int[] programmeIds,
            int? programmePriorityId,
            int? procedureId,
            DateTime? fromDate,
            DateTime? toDate,
            string projectNumber)
        {
            var predicate = PredicateBuilder.True<Project>();

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            predicate = predicate
                .And(p => p.RegistrationStatus == ProjectRegistrationStatus.Registered || p.RegistrationStatus == ProjectRegistrationStatus.RegisteredLate)
                .AndEquals(p => p.ProcedureId, procedureId)
                .AndDateTimeGreaterThanOrEqual(t => t.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.RegDate, toDate)
                .AndStringContains(t => t.RegNumber, projectNumber);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();
            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                           where programmeIds.Contains(ps.ProgrammeId)
                           select ps.ProcedureId;

            return (from p in this.unitOfWork.DbContext.Set<Project>().Where(predicate)
                    join pt in this.unitOfWork.DbContext.Set<ProjectType>() on p.ProjectTypeId equals pt.ProjectTypeId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    where subquery.Contains(p.ProcedureId)
                    select new ProjectRegistrationsVO
                    {
                        ProjectId = p.ProjectId,
                        ProcedureId = p.ProcedureId,
                        ProcedureNameBg = proc.Name,
                        ProcedureNameEn = proc.NameAlt,
                        RegNumber = p.RegNumber,
                        NameBg = p.Name,
                        NameEn = p.NameAlt,
                        CompanyNameBg = p.CompanyName,
                        CompanyNameEn = p.CompanyNameAlt,
                        CompanyUinType = p.CompanyUinType,
                        CompanyUin = p.CompanyUin,
                        RegistrationStatus = p.RegistrationStatus,
                        ProjectTypeBg = pt.Name,
                        ProjectTypeEn = pt.NameAlt,
                        RegDate = p.RegDate,
                    })
                    .ToList();
        }

        public int GetProcedureId(int projectId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Project>()
                    where p.ProjectId == projectId
                    select p.ProcedureId)
                .SingleOrDefault();
        }

        public int GetPrimaryProgrammeId(int projectId)
        {
            return (from p in this.Set()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where p.ProjectId == projectId && ps.IsPrimary
                    select ps.ProgrammeId)
                .Single();
        }

        public ProjectRegistrationDataVO GetProjectRegistrationData(int projectId)
        {
            var directions = this.GetProjectDirections(new int[] { projectId });
            var groupedDirectionsInMemory = directions.GroupBy(d => d.ProjectId);

            var resultInMemory = this.GetProjectData(projectId);

            return (from r in resultInMemory
                    join g in groupedDirectionsInMemory on r.ProjectId equals g.Key into g1
                    from g in g1.DefaultIfEmpty()
                    select new ProjectRegistrationDataVO
                    {
                        ProjectId = r.ProjectId,
                        ProgrammeName = r.ProgrammeName,
                        ProgrammePriorityName = r.ProgrammePriorityName,
                        ProcedureName = r.ProcedureName,
                        ProjectName = r.ProjectName,
                        RegNumber = r.RegNumber,
                        RegDate = r.RegDate,
                        ProjectXmlHash = r.ProjectXmlHash,
                        CompanyName = r.CompanyName,
                        Uin = r.Uin,
                        UinType = r.UinType,
                        Directions = g != null ? g.Select(s => new DirectionItemVO { Direction = s.Direction, SubDirection = s.SubDirection }).ToList() : new List<DirectionItemVO>(),
                    }).Single();
        }

        public bool HasAssociatedRegistration(int projectId)
        {
            return (from rp in this.unitOfWork.DbContext.Set<RegProjectXml>()
                    where rp.ProjectId == projectId
                    select rp.RegProjectXmlId).Any();
        }

        public bool IsProjectNumExisting(string projectNum, int? procedureId)
        {
            var predicate = PredicateBuilder.True<Project>()
                .AndStringMatches(p => p.RegNumber, projectNum, true)
                .AndEquals(p => p.ProcedureId, procedureId);

            return (from p in this.unitOfWork.DbContext.Set<Project>().Where(predicate)
                    select p.ProjectId).Any();
        }

        public IList<string> CanWithdrawProject(int projectId)
        {
            var errors = new List<string>();

            var evalSession = (from p in this.unitOfWork.DbContext.Set<Project>()
                               join es in this.unitOfWork.DbContext.Set<EvalSession>().Where(t => t.EvalSessionStatus != EvalSessionStatus.Canceled) on p.ProcedureId equals es.ProcedureId
                               join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on new { es.EvalSessionId, p.ProjectId } equals new { esp.EvalSessionId, esp.ProjectId }
                               where p.ProjectId == projectId
                               select es)
                        .SingleOrDefault();

            if (evalSession != null)
            {
                bool hasAssociatedSheets = this.unitOfWork.DbContext.Set<EvalSessionSheet>().Where(p => p.EvalSessionId == evalSession.EvalSessionId && p.ProjectId == projectId && p.Status != EvalSessionSheetStatus.Canceled).Any();

                bool hasAssociatedEvaluations = this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalSessionId == evalSession.EvalSessionId && p.ProjectId == projectId && p.IsDeleted == false).Any();

                if (hasAssociatedSheets || hasAssociatedEvaluations)
                {
                    errors.Add("Не можете да оттеглите това проектно предложение, защото то участва в неанулирана оценителна сесия и в нея има неанулирани оценителни листа или обобщени оценки. Трябва да бъдат анулирани в оценителната сесия.");
                }
            }

            return errors;
        }

        public ProjectRegistrationStatus GetProjectRegistrationStatus(int projectId)
        {
            return this.unitOfWork.DbContext.Set<Project>().Where(p => p.ProjectId == projectId).Select(p => p.RegistrationStatus).SingleOrDefault();
        }

        public ApplicationFormType GetProcedureApplicationFormType(int projectId)
        {
            return (from pr in this.unitOfWork.DbContext.Set<Project>()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on pr.ProcedureId equals p.ProcedureId
                    where pr.ProjectId == projectId
                    select p.ApplicationFormType).Single();
        }

        public IList<Project> GetProjectsForSession(int evalSessionId)
        {
            return (from p in this.Set()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on p.ProjectId equals esp.ProjectId
                    where esp.EvalSessionId == evalSessionId
                    select p).ToList();
        }

        public IList<Project> GetActiveProjectsForSession(int evalSessionId)
        {
            return (from p in this.Set()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on p.ProjectId equals esp.ProjectId
                    where esp.EvalSessionId == evalSessionId && !esp.IsDeleted && p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn
                    select p).ToList();
        }

        public IList<ProjectDossierDocumentVO> GetProjectDossierDocuments(
            int projectId,
            int? contractId,
            ProjectDossierDocumentType[] docTypes,
            string objDescription,
            string fileDescription)
        {
            var results = new List<ProjectDossierDocumentVO>();

            if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ProjectVersion))
            {
                var projectVersionsFiles = (from pvf in this.unitOfWork.DbContext.Set<ProjectVersionXmlFile>()
                                            join pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on pvf.ProjectVersionXmlId equals pv.ProjectVersionXmlId
                                            join p in this.unitOfWork.DbContext.Set<Project>() on pv.ProjectId equals p.ProjectId
                                            where pv.ProjectId == projectId && pv.Status != ProjectVersionXmlStatus.Draft
                                            select new ProjectDossierDocumentVO()
                                            {
                                                ObjectType = ProjectDossierDocumentType.ProjectVersion,
                                                ObjectTypeDesc = ProjectDossierDocumentType.ProjectVersion,
                                                ObjectDescription = pv.OrderNum.ToString(),
                                                File = new Web.Api.Core.FilePVO()
                                                {
                                                    Key = pvf.BlobKey,
                                                    Name = pvf.Name,
                                                    Description = pvf.Description,
                                                },
                                            })
                                            .ToList();

                results.AddRange(projectVersionsFiles);
            }

            if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.EvalSessionSheet))
            {
                var evalSessionSheetFiles = (from essxf in this.unitOfWork.DbContext.Set<EvalSessionSheetXmlFile>()
                                             join essx in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>() on essxf.EvalSessionSheetXmlId equals essx.EvalSessionSheetXmlId
                                             join ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>() on essx.EvalSessionSheetId equals ess.EvalSessionSheetId
                                             join es in this.unitOfWork.DbContext.Set<EvalSession>() on ess.EvalSessionId equals es.EvalSessionId
                                             where ess.ProjectId == projectId && ess.Status != EvalSessionSheetStatus.Draft && essxf.Type == EvalSessionSheetXmlFileType.AttachedDoc
                                             select new
                                             {
                                                 es.SessionNum,
                                                 ess.EvalTableType,
                                                 essxf.BlobKey,
                                                 essxf.Name,
                                                 essxf.Description,
                                             })
                                            .ToList()
                                            .Select(t => new ProjectDossierDocumentVO()
                                            {
                                                ObjectType = ProjectDossierDocumentType.EvalSessionSheet,
                                                ObjectTypeDesc = ProjectDossierDocumentType.EvalSessionSheet,
                                                ObjectDescription = t.SessionNum + "-" + t.EvalTableType.GetEnumDescription(),
                                                File = new Web.Api.Core.FilePVO()
                                                {
                                                    Key = t.BlobKey,
                                                    Name = t.Name,
                                                    Description = t.Description,
                                                },
                                            })
                                            .ToList();

                results.AddRange(evalSessionSheetFiles);
            }

            if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ProjectCommunication))
            {
                var projectCommunicationFiles = (from pcf in this.unitOfWork.DbContext.Set<ProjectCommunicationMessageFile>()
                                                 join pc in this.unitOfWork.DbContext.Set<ProjectCommunication>() on pcf.ProjectCommunicationId equals pc.ProjectCommunicationId
                                                 join p in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals p.ProjectId
                                                 where pc.ProjectId == projectId && pc.Status != ProjectCommunicationStatus.DraftQuestion &&
                                                    ((pcf.MessageType == ProjectCommunicationMessageType.Question && pcf.Type == ProjectCommunicationMessageFileType.ContentAttachedDoc) ||
                                                     (pcf.MessageType == ProjectCommunicationMessageType.Answer && pcf.Type == ProjectCommunicationMessageFileType.ReplyAttachedDoc))
                                                 select new
                                                 {
                                                     pcf.MessageType,
                                                     pcf.BlobKey,
                                                     pcf.Name,
                                                     pcf.Description,
                                                 })
                                            .ToList()
                                            .Select(t => new ProjectDossierDocumentVO()
                                            {
                                                ObjectType = ProjectDossierDocumentType.ProjectCommunication,
                                                ObjectTypeDesc = ProjectDossierDocumentType.ProjectCommunication,
                                                ObjectDescription = t.MessageType.GetEnumDescription(),
                                                File = new Web.Api.Core.FilePVO()
                                                {
                                                    Key = t.BlobKey,
                                                    Name = t.Name,
                                                    Description = t.Description,
                                                },
                                            })
                                            .ToList();

                results.AddRange(projectCommunicationFiles);
            }

            if (contractId.HasValue)
            {
                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractVersion))
                {
                    var contractVersionFiles = (from cvxf in this.unitOfWork.DbContext.Set<ContractVersionXmlFile>()
                                                join cvx in this.unitOfWork.DbContext.Set<ContractVersionXml>() on cvxf.ContractVersionXmlId equals cvx.ContractVersionXmlId
                                                join c in this.unitOfWork.DbContext.Set<Contract>() on cvx.ContractId equals c.ContractId
                                                where cvx.ContractId == contractId && cvx.Status != ContractVersionStatus.Draft
                                                select new
                                                {
                                                    cvx.OrderNum,
                                                    cvxf.BlobKey,
                                                    cvxf.Name,
                                                    cvxf.Description,
                                                })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractVersion,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractVersion,
                                                    ObjectDescription = t.OrderNum.ToString(),
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                        Description = t.Description,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractVersionFiles);
                }

                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractsContractRegistration))
                {
                    var contractsContractRegistrationFiles = (from ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>()
                                                              join b in this.unitOfWork.DbContext.Set<Blob>() on ccr.BlobKey equals b.Key
                                                              join cr in this.unitOfWork.DbContext.Set<ContractRegistration>() on ccr.ContractRegistrationId equals cr.ContractRegistrationId
                                                              where ccr.ContractId == contractId
                                                              select new
                                                              {
                                                                  cr.Email,
                                                                  BlobKey = b.Key,
                                                                  Name = b.FileName,
                                                              })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractsContractRegistration,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractsContractRegistration,
                                                    ObjectDescription = t.Email,
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractsContractRegistrationFiles);
                }

                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractProcurement))
                {
                    var contractProcurementFiles = (from cpxf in this.unitOfWork.DbContext.Set<ContractProcurementXmlFile>()
                                                    join cpx in this.unitOfWork.DbContext.Set<ContractProcurementXml>() on cpxf.ContractProcurementXmlId equals cpx.ContractProcurementXmlId
                                                    join c in this.unitOfWork.DbContext.Set<Contract>() on cpx.ContractId equals c.ContractId
                                                    where cpx.ContractId == contractId && cpx.Status != ContractProcurementStatus.Draft
                                                    select new
                                                    {
                                                        cpx.OrderNum,
                                                        cpxf.BlobKey,
                                                        cpxf.Name,
                                                        cpxf.Description,
                                                    })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractProcurement,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractProcurement,
                                                    ObjectDescription = t.OrderNum.ToString(),
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                        Description = t.Description,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractProcurementFiles);
                }

                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractReportPayment))
                {
                    var contractReportPaymentFiles = (from crpxf in this.unitOfWork.DbContext.Set<ContractReportPaymentXmlFile>()
                                                      join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crpxf.ContractReportPaymentId equals crp.ContractReportPaymentId
                                                      join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                                                      where crp.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status)
                                                      select new
                                                      {
                                                          crp.VersionNum,
                                                          crpxf.BlobKey,
                                                          crpxf.Name,
                                                          crpxf.Description,
                                                      })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractReportPayment,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractReportPayment,
                                                    ObjectDescription = t.VersionNum.ToString(),
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                        Description = t.Description,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractReportPaymentFiles);
                }

                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractReportFinancial))
                {
                    var contractReportFinancialFiles = (from crfxf in this.unitOfWork.DbContext.Set<ContractReportFinancialFile>()
                                                        join crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>() on crfxf.ContractReportFinancialId equals crf.ContractReportFinancialId
                                                        join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crf.ContractReportId equals cr.ContractReportId
                                                        where crf.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status)
                                                        select new
                                                        {
                                                            crf.VersionNum,
                                                            crfxf.BlobKey,
                                                            crfxf.Name,
                                                            crfxf.Description,
                                                        })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractReportFinancial,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractReportFinancial,
                                                    ObjectDescription = t.VersionNum.ToString(),
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                        Description = t.Description,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractReportFinancialFiles);
                }

                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractReportTechnical))
                {
                    var contractReportTechnicalFiles = (from crtxf in this.unitOfWork.DbContext.Set<ContractReportTechnicalFile>()
                                                        join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on crtxf.ContractReportTechnicalId equals crt.ContractReportTechnicalId
                                                        join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crt.ContractReportId equals cr.ContractReportId
                                                        where crt.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status) && crtxf.Type == ContractReportTechnicalFileType.AttachedDoc
                                                        select new
                                                        {
                                                            crt.VersionNum,
                                                            crtxf.BlobKey,
                                                            crtxf.Name,
                                                            crtxf.Description,
                                                        })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractReportTechnical,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractReportTechnical,
                                                    ObjectDescription = t.VersionNum.ToString(),
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                        Description = t.Description,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractReportTechnicalFiles);
                }

                if (docTypes.Length == 0 || docTypes.Contains(ProjectDossierDocumentType.ContractAdminAuthorityCommunication))
                {
                    var contractAdminAuthorityCommunicationFiles = (from ccxf in this.unitOfWork.DbContext.Set<ContractCommunicationXmlFile>()
                                                                    join ccx in this.unitOfWork.DbContext.Set<ContractCommunicationXml>() on ccxf.ContractCommunicationXmlId equals ccx.ContractCommunicationXmlId
                                                                    join c in this.unitOfWork.DbContext.Set<Contract>() on ccx.ContractId equals c.ContractId
                                                                    where ccx.ContractId == contractId && ccx.Type == ContractCommunicationType.Administrative && ccx.Status != ContractCommunicationStatus.Draft
                                                                    select new
                                                                    {
                                                                        ccx.Source,
                                                                        ccxf.BlobKey,
                                                                        ccxf.Name,
                                                                        ccxf.Description,
                                                                    })
                                                .ToList()
                                                .Select(t => new ProjectDossierDocumentVO()
                                                {
                                                    ObjectType = ProjectDossierDocumentType.ContractAdminAuthorityCommunication,
                                                    ObjectTypeDesc = ProjectDossierDocumentType.ContractAdminAuthorityCommunication,
                                                    ObjectDescription = t.Source.GetEnumDescription(),
                                                    File = new Web.Api.Core.FilePVO()
                                                    {
                                                        Key = t.BlobKey,
                                                        Name = t.Name,
                                                        Description = t.Description,
                                                    },
                                                })
                                                .ToList();

                    results.AddRange(contractAdminAuthorityCommunicationFiles);
                }

                IList<ContractReportMicroType> microTypes = new List<ContractReportMicroType>();
                if (docTypes.Length == 0)
                {
                    microTypes = Enum.GetValues(typeof(ContractReportMicroType)).Cast<ContractReportMicroType>().ToList();
                }
                else
                {
                    if (docTypes.Contains(ProjectDossierDocumentType.ContractReportMicroType1))
                    {
                        microTypes.Add(ContractReportMicroType.Type1);
                    }

                    if (docTypes.Contains(ProjectDossierDocumentType.ContractReportMicroType2))
                    {
                        microTypes.Add(ContractReportMicroType.Type2);
                    }

                    if (docTypes.Contains(ProjectDossierDocumentType.ContractReportMicroType3))
                    {
                        microTypes.Add(ContractReportMicroType.Type3);
                    }

                    if (docTypes.Contains(ProjectDossierDocumentType.ContractReportMicroType4))
                    {
                        microTypes.Add(ContractReportMicroType.Type4);
                    }
                }

                if (microTypes.Count != 0)
                {
                    var contractsReportMicroFiles = (from crm in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                                                     join b in this.unitOfWork.DbContext.Set<Blob>() on crm.ExcelBlobKey equals b.Key
                                                     join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crm.ContractReportId equals cr.ContractReportId
                                                     where crm.ContractId == contractId &&
                                                        ContractReport.FinalStatuses.Contains(cr.Status) &&
                                                        microTypes.Contains(crm.Type)
                                                     select new
                                                     {
                                                         crm.Type,
                                                         crm.VersionNum,
                                                         BlobKey = b.Key,
                                                         Name = b.FileName,
                                                     })
                                                .ToList()
                                                .Select(t =>
                                                    {
                                                        ProjectDossierDocumentType? objectType = null;
                                                        switch (t.Type)
                                                        {
                                                            case ContractReportMicroType.Type1:
                                                                objectType = ProjectDossierDocumentType.ContractReportMicroType1;
                                                                break;
                                                            case ContractReportMicroType.Type2:
                                                                objectType = ProjectDossierDocumentType.ContractReportMicroType2;
                                                                break;
                                                            case ContractReportMicroType.Type3:
                                                                objectType = ProjectDossierDocumentType.ContractReportMicroType3;
                                                                break;
                                                            case ContractReportMicroType.Type4:
                                                                objectType = ProjectDossierDocumentType.ContractReportMicroType4;
                                                                break;
                                                        }

                                                        return new ProjectDossierDocumentVO()
                                                        {
                                                            ObjectType = objectType.Value,
                                                            ObjectTypeDesc = objectType.Value,
                                                            ObjectDescription = t.VersionNum.ToString(),
                                                            File = new Web.Api.Core.FilePVO()
                                                            {
                                                                Key = t.BlobKey,
                                                                Name = t.Name,
                                                            },
                                                        };
                                                    })
                                                .ToList();

                    results.AddRange(contractsReportMicroFiles);
                }
            }

            IEnumerable<ProjectDossierDocumentVO> filteredResult = results;

            if (!string.IsNullOrEmpty(objDescription))
            {
                filteredResult = filteredResult.Where(t => t.ObjectDescription.IndexOf(objDescription, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(fileDescription))
            {
                filteredResult = filteredResult.Where(t => t.File.Description.IndexOf(fileDescription, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return filteredResult.ToList();
        }

        public IList<ProjectRegistrationsVO> GetProjectRegistrationsForProjectDossier(
            int[] programmeIds,
            int? procedureId,
            string projectNumber)
        {
            var predicate = PredicateBuilder.True<Project>();

            predicate = predicate
                .AndEquals(p => p.ProcedureId, procedureId)
                .AndStringContains(t => t.RegNumber, projectNumber);

            return (from p in this.unitOfWork.DbContext.Set<Project>().Where(predicate)
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on p.ProjectId equals esp.ProjectId
                    join es in this.unitOfWork.DbContext.Set<EvalSession>() on esp.EvalSessionId equals es.EvalSessionId
                    join pt in this.unitOfWork.DbContext.Set<ProjectType>() on p.ProjectTypeId equals pt.ProjectTypeId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    where es.EvalSessionStatus == EvalSessionStatus.Ended
                    select new ProjectRegistrationsVO
                    {
                        ProjectId = p.ProjectId,
                        ProcedureId = p.ProcedureId,
                        ProcedureNameBg = proc.Name,
                        ProcedureNameEn = proc.NameAlt,
                        RegNumber = p.RegNumber,
                        NameBg = p.Name,
                        NameEn = p.NameAlt,
                        CompanyNameBg = p.CompanyName,
                        CompanyNameEn = p.CompanyNameAlt,
                        CompanyUinType = p.CompanyUinType,
                        CompanyUin = p.CompanyUin,
                        RegistrationStatus = p.RegistrationStatus,
                        ProjectTypeBg = pt.Name,
                        ProjectTypeEn = pt.NameAlt,
                        RegDate = p.RegDate,
                    })
                    .Distinct()
                    .ToList();
        }

        public IList<string> IsProjectValidForProjectDossier(int projectId)
        {
            var errors = new List<string>();

            if (!this.IsProjectInFinishedEvalSession(projectId))
            {
                errors.Add("Проектното предложение трябва да бъде в оценителна сесия със статус 'Приключена'");
            }

            return errors;
        }

        public bool IsProjectInFinishedEvalSession(int projectId)
        {
            var isProjectInEndedSession = (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                                           join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on es.EvalSessionId equals esp.EvalSessionId
                                           where (es.EvalSessionStatus == EvalSessionStatus.Ended || es.EvalSessionStatus == EvalSessionStatus.EndedByLAG) && esp.ProjectId == projectId
                                           select es.EvalSessionId).Any();

            return isProjectInEndedSession;
        }

        public ProjectVersionXmlFile GetProjectVersionXmlFile(int projectId, int projectVersionXmlFileId)
        {
            return (from pvf in this.unitOfWork.DbContext.Set<ProjectVersionXmlFile>()
                    join pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on pvf.ProjectVersionXmlId equals pv.ProjectVersionXmlId
                    join p in this.unitOfWork.DbContext.Set<Project>() on pv.ProjectId equals p.ProjectId
                    where pvf.FileId == projectVersionXmlFileId && p.ProjectId == projectId
                    select pvf)
                    .Single();
        }

        public IList<ProjectMonitorstatRequestsVO> GetMonitorstatRequests(int projectId)
        {
            return (from mr in this.unitOfWork.DbContext.Set<ProjectMonitorstatRequest>().Where(x => x.ProjectId == projectId)
                    join p in this.Set() on mr.ProjectId equals p.ProjectId
                    join f in this.unitOfWork.DbContext.Set<ProjectVersionXmlFile>() on mr.ProjectVersionXmlFileId equals f.FileId into gf
                    from f in gf.DefaultIfEmpty()
                    join pmr in this.unitOfWork.DbContext.Set<ProcedureMonitorstatRequest>() on mr.ProcedureMonitorstatRequestId equals pmr.ProcedureMonitorstatRequestId

                    join u in this.unitOfWork.DbContext.Set<Domain.Users.User>() on mr.UserId equals u.UserId into g1
                    from u in g1.DefaultIfEmpty()

                    join r in this.unitOfWork.DbContext.Set<ProjectMonitorstatResponse>() on mr.ProjectMonitorstatRequestId equals r.ProjectMonitorstatRequestId into g2

                    select new ProjectMonitorstatRequestsVO
                    {
                        ProjectMonitorstatRequestId = mr.ProjectMonitorstatRequestId,
                        RequestName = pmr.Name,
                        CompanyUin = mr.CompanyUin,
                        Status = mr.Status,
                        ModifyDate = mr.ModifyDate,
                        Declaration = f != null ? f.Name : mr.DeclarationFile.FileName,
                        FileKey = f != null ? f.BlobKey : mr.DeclarationFile.Key,
                        User = u.Username,
                        Version = p.Version,
                        Responses = g2.Select(x => new ProjectMonitorstatResponseVO()
                        {
                            FileKey = x.FileKey,
                            FileName = x.FileName,
                            ModifyDate = x.ModifyDate,
                        })
                        .OrderByDescending(resp => resp.ModifyDate)
                        .ToList(),
                    })
                    .OrderByDescending(req => req.ModifyDate)
                    .ToList();
        }

        public IList<ProjectMonitorstatRequest> GetMonitorstatRequests(int procedureId, string uin)
        {
            var predicate = PredicateBuilder.True<ProjectMonitorstatRequest>()
                .And(r => r.Status == ProjectMonitorstatRequestStatus.Sent || r.Status == ProjectMonitorstatRequestStatus.Finished)
                .AndEquals(p => p.CompanyUin, uin);

            var requests =
                (from pmr in this.unitOfWork.DbContext.Set<ProjectMonitorstatRequest>().Where(predicate)
                 join p in this.unitOfWork.DbContext.Set<Project>() on pmr.ProjectId equals p.ProjectId
                 where p.ProcedureId == procedureId
                 select pmr)
                 .ToList();

            return requests;
        }

        public IList<ProjectMonitorstatResponse> GetMonitorstatResponses(int projectMonitorstatRequestId)
        {
            var responses =
                (from resp in this.unitOfWork.DbContext.Set<ProjectMonitorstatResponse>()
                 where resp.ProjectMonitorstatRequestId == projectMonitorstatRequestId
                 select resp)
                 .OrderByDescending(resp => resp.ModifyDate)
                 .ToList();

            return responses;
        }

        public ProjectMonitorstatRequest GetMonitorstatRequest(Guid subjectRequest)
        {
            return this.unitOfWork.DbContext.Set<ProjectMonitorstatRequest>().FirstOrDefault(s => s.ForeignGid.Value == subjectRequest);
        }

        public int? GetProjectId(string projectRegNumber)
        {
            return (from c in this.unitOfWork.DbContext.Set<Project>()
                    where c.RegNumber == projectRegNumber
                    select (int?)c.ProjectId)
                    .SingleOrDefault();
        }

        public IList<EvalSessionProjectMonitorstatRequestsVO> GetMonitorstatRequestsForEvalSession(
            int evalSessionId,
            int? projectId,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            var predicate = PredicateBuilder.True<ProjectMonitorstatRequest>()
                .AndEquals(p => p.ProjectId, projectId)
                .AndDateTimeGreaterThanOrEqual(p => p.ModifyDate, dateFrom?.Date)
                .AndDateTimeLessThanOrEqual(p => p.ModifyDate, dateTo?.EndOfDay());

            return (from mr in this.unitOfWork.DbContext.Set<ProjectMonitorstatRequest>().Where(predicate)
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>().Where(esp => !esp.IsDeleted && esp.EvalSessionId == evalSessionId) on mr.ProjectId equals esp.ProjectId
                    join p in this.unitOfWork.DbContext.Set<Project>() on mr.ProjectId equals p.ProjectId
                    join pmr in this.unitOfWork.DbContext.Set<ProcedureMonitorstatRequest>() on mr.ProcedureMonitorstatRequestId equals pmr.ProcedureMonitorstatRequestId
                    join r in this.unitOfWork.DbContext.Set<ProjectMonitorstatResponse>() on mr.ProjectMonitorstatRequestId equals r.ProjectMonitorstatRequestId into g2

                    join f in this.unitOfWork.DbContext.Set<ProjectVersionXmlFile>() on mr.ProjectVersionXmlFileId equals f.FileId into gf
                    from f in gf.DefaultIfEmpty()

                    join u in this.unitOfWork.DbContext.Set<Domain.Users.User>() on mr.UserId equals u.UserId into g1
                    from u in g1.DefaultIfEmpty()

                    select new EvalSessionProjectMonitorstatRequestsVO
                    {
                        ProjectMonitorstatRequestId = mr.ProjectMonitorstatRequestId,
                        ProjectId = mr.ProjectId,
                        CompanyUin = mr.CompanyUin,
                        Status = mr.Status,
                        ModifyDate = mr.ModifyDate,
                        ProjectNameBg = p.Name,
                        ProjectNameEn = p.NameAlt,
                        ProjectRegNumber = p.RegNumber,
                        Declaration = f != null ? f.Name : mr.DeclarationFile.FileName,
                        FileKey = f != null ? f.BlobKey : mr.DeclarationFile.Key,
                        User = u.Username,
                        Responses = g2.Select(x => new ProjectMonitorstatResponseVO()
                        {
                            FileKey = x.FileKey,
                            FileName = x.FileName,
                            ModifyDate = x.ModifyDate,
                        })
                        .OrderByDescending(resp => resp.ModifyDate)
                        .ToList(),
                    })
                    .OrderByDescending(req => req.ModifyDate)
                    .ToList();
        }

        public IList<ProjectDirectionVO> GetProjectDirections(int[] projectIds)
        {
            var whereIn = string.Join(",", projectIds);

            string query = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10019' as R10019,
                    N'http://ereg.egov.bg/segment/R-09998' as R09998,
                    N'http://ereg.egov.bg/segment/R-10093' as R10093,
                    N'http://ereg.egov.bg/segment/R-10000' as R10000
                )
                
                SELECT 
                px.ProjectId,
                T2.j.value('.', 'nvarchar(1000)' ) as 'Direction', 
                T3.k.value('.', 'nvarchar(1000)') as 'SubDirection'
                FROM ProjectVersionXmls px
                CROSS APPLY Xml.nodes('/Project/R10019:DirectionsBudgetContract/R09998:Directions/R10093:Direction') AS T1(i)
                OUTER APPLY T1.i.nodes('R10093:DirectionItem/R10000:Name') as T2(j)
                OUTER APPLY T1.i.nodes('R10093:SubDirection/R10000:Name') as T3(k)
                
                where px.ProjectId in ({whereIn})";

            return this.SqlQuery<ProjectDirectionVO>(query, new List<SqlParameter>()).ToList();
        }

        private IList<ProjectRegistrationDataVO> GetProjectData(int projectId)
        {
            return (from proj in this.unitOfWork.DbContext.Set<Project>()
             join pr in this.unitOfWork.DbContext.Set<Procedure>() on proj.ProcedureId equals pr.ProcedureId
             join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
             join p in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals p.MapNodeId
             join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

             join projx in this.unitOfWork.DbContext.Set<RegProjectXml>() on proj.ProjectId equals projx.ProjectId into g1
             from projx in g1.DefaultIfEmpty()

             where proj.ProjectId == projectId && ps.IsPrimary
             select new ProjectRegistrationDataVO
             {
                 ProjectId = proj.ProjectId,
                 ProgrammeName = p.Name,
                 ProgrammePriorityName = pp.Name,
                 ProcedureName = pr.Name,
                 ProjectName = proj.Name,
                 RegNumber = proj.RegNumber,
                 RegDate = proj.RegDate,
                 ProjectXmlHash = projx.Hash,
                 CompanyName = proj.CompanyName,
                 Uin = proj.CompanyUin,
                 UinType = proj.CompanyUinType,
             }).ToList();
        }
    }
}
