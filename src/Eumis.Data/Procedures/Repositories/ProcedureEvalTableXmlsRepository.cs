using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureEvalTableXmlsRepository : AggregateRepository<ProcedureEvalTableXml>, IProcedureEvalTableXmlsRepository
    {
        public ProcedureEvalTableXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProcedureEvalTableXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProcedureEvalTableXml, object>>[]
                {
                    p => p.Files,
                };
            }
        }

        public ProcedureEvalTableXml FindByProcedureEvalTableId(int procedureEvalTableId)
        {
            return this.Set()
                .Where(p => p.ProcedureEvalTableId == procedureEvalTableId)
                .Single();
        }

        public ProcedureEvalTableXml FindByGid(Guid procedureEvalTableXmlGid)
        {
            return this.Set()
                .Where(p => p.Gid == procedureEvalTableXmlGid)
                .Single();
        }

        public ProcedureEvalTableXml FindForUpdateByGid(Guid procedureEvalTableXmlGid, byte[] version)
        {
            var evalTableXml = this.FindByGid(procedureEvalTableXmlGid);

            this.CheckVersion(evalTableXml.Version, version);

            return evalTableXml;
        }

        public IList<ProcedureEvalTableXml> FindByProcedureId(int procedureId)
        {
            return this.Set()
                .Where(et => et.ProcedureId == procedureId)
                .ToList();
        }

        public void RemoveByEvalTableId(int evalTableId)
        {
            var evalTableXml = this.Set()
                .Single(pet => pet.ProcedureEvalTableId == evalTableId);

            this.unitOfWork.DbContext.Set<ProcedureEvalTableXml>().Remove(evalTableXml);
        }

        public int GetProcedureId(Guid gid)
        {
            return (from pet in this.unitOfWork.DbContext.Set<ProcedureEvalTableXml>()
                    where pet.Gid == gid
                    select pet.ProcedureId).Single();
        }
    }
}
