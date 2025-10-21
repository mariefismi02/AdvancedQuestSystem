using UnityEngine;

namespace mariefismi02.Quest
{
    public interface IQuestReward<T>
    {
        void Give(T target);
    }
}