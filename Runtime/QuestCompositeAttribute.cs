using System;

namespace mariefismi02.Quest
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class QuestCompositeAttribute : Attribute
    {
        public Type ForType { get; }
        public QuestCompositeAttribute(Type forType = null)
        {
            ForType = forType;
        }
    }
}
