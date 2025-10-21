namespace mariefismi02.Quest.Asset
{

    [System.Serializable]
    public abstract class QuestRewardData<T>
    {
        public abstract IQuestReward<T> CreateInstance();
    }
}