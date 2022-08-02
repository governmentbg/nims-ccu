using System;

namespace Eumis.Public.Web.InfrastructureClasses.PublicEnvironment
{
    public class EnvironmentTuple
    {
        public EnvironmentTuple(string key, EnvironmentType type, Func<string> text)
        {
            this.Key = key;
            this.Text = text;
            this.Type = type;
        }

        public string Key { get; private set; }

        public EnvironmentType Type { get; private set; }

        public Func<string> Text { get; private set; }
    }
}