using System;

namespace Eumis.Domain.CheckBlankTopics
{
    public partial class CheckBlankTopic
    {
        public void UpdateTopic(string name)
        {
            this.Name = name;
            this.ModifyDate = DateTime.Now;
        }
    }
}
