using System;

namespace mariefismi02.Quest
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuestOwnershipAttribute : Attribute
    {
        public string DisplayName { get; }
        public QuestOwnershipAttribute(string displayName = null)
        {
            DisplayName = displayName;
        }
    }
}