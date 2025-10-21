#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace mariefismi02.Quest.Asset
{

    [InitializeOnLoad]
    public static class QuestEditorCache
    {
        // Dictionary reward: generic owner type → list of reward types yg boleh
        public static Dictionary<Type, List<Type>> RewardTypesByOwner { get; } = new();

        // Objectives cukup 1 list saja
        public static List<Type> ObjectiveTypes { get; private set; } = new();

        static QuestEditorCache()
        {
            Refresh();
        }

        public static void Refresh()
        {
            RewardTypesByOwner.Clear();

            ObjectiveTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(QuestObjectiveData).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => IsSubclassOfRawGeneric(typeof(QuestRewardData<>), t) && !t.IsAbstract);

            foreach (var t in allTypes)
            {
                var genericParam = GetGenericParameter(t, typeof(QuestRewardData<>));
                if (genericParam != null)
                {
                    if (!RewardTypesByOwner.ContainsKey(genericParam))
                        RewardTypesByOwner[genericParam] = new List<Type>();

                    RewardTypesByOwner[genericParam].Add(t);
                }
            }
        }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        private static Type GetGenericParameter(Type type, Type openGenericBase)
        {
            var current = type;
            while (current != null && current != typeof(object))
            {
                if (current.IsGenericType && current.GetGenericTypeDefinition() == openGenericBase)
                {
                    var genericType = current.GetGenericArguments()[0];
                    return genericType;
                }

                current = current.BaseType;
            }
            return null;
        }


    }
}
#endif