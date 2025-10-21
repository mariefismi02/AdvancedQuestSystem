using mariefismi02.Quest;
using mariefismi02.Quest.Save;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    private QuestSaveManager<Player> saveManager;

    [SerializeField]
    private PlayerQuestAsset[] quests;
    [SerializeField]
    private Player player;

    public static PlayerQuestManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        saveManager = new QuestSaveManager<Player>();
        LoadSaveQuests();
    }

    private void Start()
    {
        var quests = PlayerQuestManager.Instance.GetActiveQuests();
        for (int i = 0; i < quests.Count; i++)
        {
            quests[i].Start();
            quests[i].OnCompleted.AddListener(UpdateQuest);
        }
    }

    private void UpdateQuest(Quest<Player> quest)
    {
        quest.Update(player);
    }

    private void LoadSaveQuests()
    {
        var questList = new List<Quest<Player>>();
        foreach (var questAsset in quests)
        {
            var quest = questAsset.CreateData();
            questList.Add(quest);
        }
        saveManager.LoadAllQuests(questList);
    }

    private void OnDestroy()
    {
        saveManager.SaveAllQuests();
    }

    public void SaveAllQuests()
    {
        saveManager.SaveAllQuests();
    }

    public List<Quest<Player>> GetActiveQuests()
    {
        return saveManager.ActiveQuests;
    }
}
