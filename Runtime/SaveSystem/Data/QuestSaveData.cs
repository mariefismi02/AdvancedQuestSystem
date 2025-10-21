using System.Collections.Generic;

namespace mariefismi02.Quest.Save
{

    [System.Serializable]
    public class QuestSaveData
    {
        public string QuestId;
        public QuestState State;
        public List<QuestObjectiveProgressData> Objectives;
    }
}