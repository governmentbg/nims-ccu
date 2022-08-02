namespace Eumis.Log.ActionLogger
{
    public class ActionLogGroupInfo
    {
        internal ActionLogGroupInfo(int id, string key, string displayName)
        {
            this.Id = id;
            this.Key = key;
            this.DisplayName = displayName;
        }

        public int Id { get; private set; }

        public string Key { get; private set; }

        public string DisplayName { get; private set; }
    }
}
