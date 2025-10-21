using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mariefismi02.Quest.Save
{
    public class QuestSaveManager<T>
    {
        public List<Quest<T>> ActiveQuests { get; private set; } = new();

        private string GetPrefsKey(Quest<T> quest)
            => $"Quest_{quest.QuestId}_{typeof(T).Name}";

        public void SaveQuest(Quest<T> quest)
        {
            var data = quest.GetSaveData();
            string json = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString(GetPrefsKey(quest), json);
            PlayerPrefs.Save();
            Debug.Log($"Saved quest {quest.QuestId}");
        }

        public void LoadQuest(Quest<T> quest)
        {
            string key = GetPrefsKey(quest);
            if (!PlayerPrefs.HasKey(key)) return;

            string json = PlayerPrefs.GetString(key);
            var data = JsonUtility.FromJson<QuestSaveData>(json);
            quest.LoadFromData(data);
            Debug.Log($"Loaded quest {quest.QuestId}");
        }

        public void SaveAllQuests()
        {
            foreach (var quest in ActiveQuests)
                SaveQuest(quest);
        }

        public void LoadAllQuests(IEnumerable<Quest<T>> quests)
        {
            foreach (var quest in quests)
                LoadQuest(quest);

            ActiveQuests = quests.ToList();
        }
    }
}