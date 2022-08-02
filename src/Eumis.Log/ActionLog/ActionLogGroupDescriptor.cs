using Eumis.Domain.ActionLogs;

namespace Eumis.Log.ActionLogger
{
    public class ActionLogGroupDescriptor
    {
        internal ActionLogGroupDescriptor(ActionLogType actionLogType, string key)
        {
            this.ActionLogType = actionLogType;
            this.Key = key;
        }

        public ActionLogType ActionLogType { get; private set; }

        public string Key { get; private set; }
    }
}
