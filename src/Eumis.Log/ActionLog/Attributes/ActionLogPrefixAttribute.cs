using System;

namespace Eumis.Log.ActionLogger.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ActionLogPrefixAttribute : Attribute
    {
        public ActionLogPrefixAttribute(Type prefixAction)
        {
            this.PrefixAction = prefixAction;
        }

        public Type PrefixAction { get; set; }
    }
}
