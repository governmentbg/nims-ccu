using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Monitorstat.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Monitorstat.Repositories
{
    internal class MonitorstatSurveysRepository : AggregateRepository<MonitorstatSurvey>, IMonitorstatSurveysRepository
    {
        public MonitorstatSurveysRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<MonitorstatSurvey, object>>[] Includes
        {
            get
            {
                return new Expression<Func<MonitorstatSurvey, object>>[]
                {
                    c => c.Reports,
                };
            }
        }

        public IList<MonitorstatSurveyVO> GetSurveys()
        {
            return (from ms in this.Set()
                    select new MonitorstatSurveyVO
                    {
                        MonitorstatSurveyId = ms.MonitorstatSurveyId,
                        CreateDate = ms.CreateDate,
                        Name = ms.Name,
                        Year = (int)ms.Year,
                    }).ToList();
        }

        public IList<MonitorstatSurvey> GetSurveysByYear(MonitorstatYear year)
        {
            return this.unitOfWork.DbContext.Set<MonitorstatSurvey>().Where(x => x.Year == year).ToList();
        }

        public MonitorstatSurveyVO GetSurvey(int id)
        {
            var survey = this.Find(id);

            return new MonitorstatSurveyVO(survey);
        }
    }
}
