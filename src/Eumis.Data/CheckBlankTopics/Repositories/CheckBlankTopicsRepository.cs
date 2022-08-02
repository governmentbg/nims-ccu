using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.CheckBlankTopics.ViewObjects;
using Eumis.Domain.CheckBlankTopics;

namespace Eumis.Data.CheckBlankTopics.Repositories
{
    internal class CheckBlankTopicsRepository : AggregateRepository<CheckBlankTopic>, ICheckBlankTopicsRepository
    {
        public CheckBlankTopicsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<CheckBlankTopicVO> GetTopics()
        {
            return (from topic in this.unitOfWork.DbContext.Set<CheckBlankTopic>()
                    select new CheckBlankTopicVO
                    {
                        CheckBlankTopicId = topic.CheckBlankTopicId,
                        Name = topic.Name,
                    }).ToList();
        }
    }
}
