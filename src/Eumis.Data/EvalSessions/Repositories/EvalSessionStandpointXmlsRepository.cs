using System;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionStandpointXmlsRepository : AggregateRepository<EvalSessionStandpointXml>, IEvalSessionStandpointXmlsRepository
    {
        public EvalSessionStandpointXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<EvalSessionStandpointXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<EvalSessionStandpointXml, object>>[]
                {
                    p => p.Files,
                };
            }
        }

        public int GetEvalSessionId(Guid gid)
        {
            return (from s in this.unitOfWork.DbContext.Set<EvalSessionStandpointXml>()
                    where s.Gid == gid
                    select s.EvalSessionId).Single();
        }

        public int GetEvalSessionStandpointId(Guid gid)
        {
            return (from s in this.unitOfWork.DbContext.Set<EvalSessionStandpointXml>()
                    where s.Gid == gid
                    select s.EvalSessionStandpointId).Single();
        }

        public EvalSessionStandpointXml FindByEvalSessionStandpointId(int standpointId)
        {
            return this.Set()
                .Where(s => s.EvalSessionStandpointId == standpointId)
                .Single();
        }

        public EvalSessionStandpointXml FindByGid(Guid gid)
        {
            return this.Set()
                .Where(s => s.Gid == gid)
                .Single();
        }

        public EvalSessionStandpointXml FindForUpdateByGid(Guid gid, byte[] version)
        {
            var standpoint = this.FindByGid(gid);
            this.CheckVersion(standpoint.Version, version);

            return standpoint;
        }
    }
}
