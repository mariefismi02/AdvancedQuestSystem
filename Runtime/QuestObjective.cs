using UnityEngine;
using UnityEngine.Events;

namespace mariefismi02.Quest
{
    public abstract class QuestObjective
    {
        public abstract string Description { get; }
        public bool IsCompleted { get; protected set; }

        public UnityEvent<int, int> OnProgressUpdated { get; protected set; } = new();

        public UnityEvent OnCompleted { get; protected set; } = new();

        public abstract void Init();
        public abstract void Cleanup();

        public abstract (int current, int target) GetProgress();
        public abstract int GetProgressValue();
        public abstract void SetProgressValue(int value);
    }
}