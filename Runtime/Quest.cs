using mariefismi02.Quest.Save;
using System.Collections.Generic;
using UnityEngine.Events;

namespace mariefismi02.Quest
{
    public interface IQuest
    {
        string QuestId { get; }

        //TODO: add to quest editor
        QuestState State { get; }

        List<QuestObjective> Objectives { get; }
    }

    public class Quest<T> : IQuest, IQuestSaveable<QuestSaveData>
    {
        public string QuestId { get; private set; }
        public QuestState State { get; private set; } = QuestState.InProgress;
        public List<QuestObjective> Objectives => objectives;

        private List<QuestObjective> objectives;
        private List<IQuestReward<T>> rewards;

        private int completedObjectiveCount = 0;

        public UnityEvent<Quest<T>> OnCompleted { get; private set; }

        public Quest(List<QuestObjective> objectives, List<IQuestReward<T>> rewards, string questId, QuestState state)
        {
            this.objectives = objectives;
            this.rewards = rewards;
            QuestId = questId;
            State = state;
            OnCompleted = new();
        }

        public void Start()
        {
            if (State != QuestState.Locked && State != QuestState.Completed)
            {
                foreach (var objective in objectives)
                {
                    objective.Init();
                    objective.OnCompleted.AddListener(Update);
                }
            }
        }

        private void Update()
        {
            completedObjectiveCount++;
            if (completedObjectiveCount >= objectives.Count)
            {
                OnCompleted.Invoke(this);
            }
        }

        public void Update(T rewardTarget)
        {
            if (State == QuestState.InProgress && objectives.TrueForAll(obj => obj.IsCompleted))
            {
                Complete(rewardTarget);
            }
        }

        private void Complete(T rewardTarget)
        {
            State = QuestState.Completed;
            foreach (var objective in objectives)
            {
                objective.Cleanup();
            }

            foreach (var reward in rewards)
            {
                reward.Give(rewardTarget);
            }
        }

        public QuestSaveData GetSaveData()
        {
            var data = new QuestSaveData
            {
                QuestId = this.QuestId,
                State = this.State,
                Objectives = new List<QuestObjectiveProgressData>()
            };

            for (int i = 0; i < objectives.Count; i++)
            {
                data.Objectives.Add(new QuestObjectiveProgressData
                {
                    ObjectiveIndex = i,
                    Progress = objectives[i].GetProgressValue() // bikin method di QuestObjective
                });
            }
            return data;
        }

        public void LoadFromData(QuestSaveData saveData)
        {
            this.State = saveData.State;
            for (int i = 0; i < saveData.Objectives.Count; i++)
            {
                objectives[i].SetProgressValue(saveData.Objectives[i].Progress);
            }
        }
    }
}