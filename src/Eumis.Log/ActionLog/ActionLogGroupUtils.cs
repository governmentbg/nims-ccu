using Eumis.Domain.ActionLogs;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Eumis.Log.ActionLogger
{
    public static class ActionLogGroupUtils
    {
        private static List<ActionLogGroupInfo> actionLogInfoItems = null;
        private static List<ActionLogGroupInfo> portalActionLogInfoItems = null;
        private static ConcurrentDictionary<Type, ActionLogGroupDescriptor> classDescriptionKeys = new ConcurrentDictionary<Type, ActionLogGroupDescriptor>();

        private static List<ActionLogGroupInfo> ActionLogInfoItems
        {
            get
            {
                if (actionLogInfoItems == null)
                {
                    actionLogInfoItems = ExtractLevel2GroupInfoItems(typeof(ActionLogGroups));
                }

                return actionLogInfoItems;
            }
        }

        private static List<ActionLogGroupInfo> PortalActionLogInfoItems
        {
            get
            {
                if (portalActionLogInfoItems == null)
                {
                    portalActionLogInfoItems = ExtractLevel2GroupInfoItems(typeof(ActionLogPortalGroups));
                }

                return portalActionLogInfoItems;
            }
        }

        public static List<ActionLogGroupInfo> GetActionLogInfoItems()
        {
            return ActionLogInfoItems;
        }

        public static List<ActionLogGroupInfo> GetPortalActionLogInfoItems()
        {
            return PortalActionLogInfoItems;
        }

        public static ActionLogGroupInfo GetActionLogInfoByKey(string key)
        {
            ActionLogGroupInfo infoItem = null;

            var splitActions = key.Split(new char[] { '.' });
            if (splitActions.Count() >= 2)
            {
                var actionLevel2 = string.Format("{0}.{1}", splitActions[0], splitActions[1]);

                infoItem = ActionLogInfoItems.Where(e => e.Key == actionLevel2).SingleOrDefault();
            }

            if (infoItem == null)
            {
                throw new Exception(string.Format("Invalid ActionLog Key: ActionLogType=1 and Action='{0}'.", key));
            }

            return infoItem;
        }

        public static ActionLogGroupInfo GetPortalActionLogInfoByKey(string key)
        {
            ActionLogGroupInfo infoItem = null;

            var splitActions = key.Split(new char[] { '.' });
            if (splitActions.Count() >= 2)
            {
                var actionLevel2 = string.Format("{0}.{1}", splitActions[0], splitActions[1]);

                infoItem = PortalActionLogInfoItems.Where(e => e.Key == actionLevel2).SingleOrDefault();
            }

            if (infoItem == null)
            {
                throw new Exception(string.Format("Invalid ActionLog Key: ActionLogType=2 and Action='{0}'.", key));
            }

            return infoItem;
        }

        public static ActionLogGroupInfo GetActionLogGroupInfoById(int id)
        {
            return ActionLogInfoItems.Where(e => e.Id == id).SingleOrDefault();
        }

        public static ActionLogGroupInfo GetPortalActionLogGroupInfoById(int id)
        {
            return PortalActionLogInfoItems.Where(e => e.Id == id).SingleOrDefault();
        }

        public static string GetClassDescriptionKey(Type type)
        {
            return GetClassDescriptor(type).Key;
        }

        public static ActionLogGroupDescriptor GetClassDescriptor(Type t)
        {
            return classDescriptionKeys.GetOrAdd(t, (type) =>
            {
                var classNames = type.FullName.Split(new char[] { '+' });

                var actionLogType =
                    classNames[0] == typeof(ActionLogGroups).FullName ? ActionLogType.Internal :
                    classNames[0] == typeof(ActionLogPortalGroups).FullName ? ActionLogType.Portal :
                    (ActionLogType?)null;

                if (!actionLogType.HasValue || classNames.Length <= 1)
                {
                    throw new Exception("Invalid ActionLog class type.");
                }

                return new ActionLogGroupDescriptor(actionLogType.Value, string.Join(".", classNames.Skip(1)));
            });
        }

        public static ActionLogGroupDescriptor GetClassDescriptor(Type prefixType, string suffix)
        {
            var prefixDescriptor = GetClassDescriptor(prefixType);

            return new ActionLogGroupDescriptor(prefixDescriptor.ActionLogType, $"{prefixDescriptor.Key}.{suffix}");
        }

        private static List<ActionLogGroupInfo> ExtractLevel2GroupInfoItems(Type rootGroupType)
        {
            List<ActionLogGroupInfo> infoItems = new List<ActionLogGroupInfo>();

            foreach (var rootClassType in rootGroupType.GetNestedTypes(BindingFlags.Public | BindingFlags.Static))
            {
                string rootClassDisplayName = GetDisplayName(rootClassType);

                foreach (var childClassType in rootClassType.GetNestedTypes(BindingFlags.Public | BindingFlags.Static))
                {
                    string childClassDisplayName = GetDisplayName(childClassType);

                    infoItems.Add(new ActionLogGroupInfo(GetNextId(infoItems), GetClassDescriptionKey(childClassType), string.Format("{0} / {1}", rootClassDisplayName, childClassDisplayName)));
                }
            }

            return infoItems;
        }

        private static int GetNextId(List<ActionLogGroupInfo> infoItems)
        {
            if (infoItems != null && infoItems.Count > 0)
            {
                return infoItems.Max(e => e.Id) + 1;
            }
            else
            {
                return 1;
            }
        }

        private static string GetDisplayName(MemberInfo type)
        {
            return ((DisplayNameAttribute)type.GetCustomAttribute(typeof(DisplayNameAttribute), false)).DisplayName;
        }
    }
}
