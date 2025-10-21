using UnityEngine;

namespace mariefismi02.Quest
{
    public interface IQuestSaveable<T>
    {
        T GetSaveData();

        void LoadFromData(T data);
    }
}