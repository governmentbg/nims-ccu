using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CheckBlankTopics.Repositories;
using Eumis.Data.CheckBlankTopics.ViewObjects;
using Eumis.Domain.CheckBlankTopics;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.CheckBlankTopics.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CheckBlankTopics.Controllers
{
    [RoutePrefix("api/checkBlankTopics")]
    public class CheckBlankTopicsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICheckBlankTopicsRepository checkBlankTopicsRepository;
        private IAuthorizer authorizer;

        public CheckBlankTopicsController(
            IUnitOfWork unitOfWork,
            ICheckBlankTopicsRepository checkBlankTopicsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.checkBlankTopicsRepository = checkBlankTopicsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<CheckBlankTopicVO> GetTopics()
        {
            this.authorizer.AssertCanDo(CheckBlankTopicListActions.Search);

            return this.checkBlankTopicsRepository.GetTopics();
        }

        [Route("{topicId:int}")]
        public CheckBlankTopicDO GetTopic(int topicId)
        {
            this.authorizer.AssertCanDo(CheckBlankTopicActions.View, topicId);

            var topic = this.checkBlankTopicsRepository.Find(topicId);

            return new CheckBlankTopicDO(topic);
        }

        [HttpPut]
        [Route("{topicId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CheckBlankTopics.Edit), IdParam = "topicId")]
        public void UpdateTopic(int topicId, CheckBlankTopicDO topic)
        {
            this.authorizer.AssertCanDo(CheckBlankTopicActions.Edit, topicId);

            CheckBlankTopic oldTopic = this.checkBlankTopicsRepository.FindForUpdate(topicId, topic.Version);
            oldTopic.UpdateTopic(topic.Name);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public CheckBlankTopicDO NewTopic()
        {
            this.authorizer.AssertCanDo(CheckBlankTopicListActions.Create);

            return new CheckBlankTopicDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CheckBlankTopics.Create))]
        public CheckBlankTopicDO CreateTopic(CheckBlankTopicDO topic)
        {
            this.authorizer.AssertCanDo(CheckBlankTopicListActions.Create);

            CheckBlankTopic newTopic = new CheckBlankTopic(topic.Name);

            this.checkBlankTopicsRepository.Add(newTopic);

            this.unitOfWork.Save();

            return new CheckBlankTopicDO(newTopic);
        }

        [HttpDelete]
        [Route("{topicId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CheckBlankTopics.Delete), IdParam = "topicId")]
        public void DeleteTopic(int topicId, string version)
        {
            this.authorizer.AssertCanDo(CheckBlankTopicActions.Delete, topicId);

            byte[] vers = System.Convert.FromBase64String(version);
            CheckBlankTopic oldTopic = this.checkBlankTopicsRepository.FindForUpdate(topicId, vers);

            this.checkBlankTopicsRepository.Remove(oldTopic);

            this.unitOfWork.Save();
        }
    }
}
