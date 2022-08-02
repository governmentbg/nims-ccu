using System.Collections.Generic;
using Eumis.Data.CheckBlankTopics.ViewObjects;
using Eumis.Domain.CheckBlankTopics;

namespace Eumis.Data.CheckBlankTopics.Repositories
{
    public interface ICheckBlankTopicsRepository : IAggregateRepository<CheckBlankTopic>
    {
        IList<CheckBlankTopicVO> GetTopics();
    }
}
