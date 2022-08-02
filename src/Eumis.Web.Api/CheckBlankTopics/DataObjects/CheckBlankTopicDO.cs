using Eumis.Domain.CheckBlankTopics;

namespace Eumis.Web.Api.CheckBlankTopics.DataObjects
{
    public class CheckBlankTopicDO
    {
        public CheckBlankTopicDO()
        {
        }

        public CheckBlankTopicDO(CheckBlankTopic topic)
        {
            this.CheckBlankTopicId = topic.CheckBlankTopicId;
            this.Name = topic.Name;

            this.Version = topic.Version;
        }

        public int? CheckBlankTopicId { get; set; }

        public string Name { get; set; }

        public byte[] Version { get; set; }
    }
}
