
using mariefismi02.Quest;
using mariefismi02.Quest.Asset;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerQuestAsset", menuName = "Quests/Player Quest")]
public class PlayerQuestAsset : ScriptableObject
{
    public string QuestId;

    // serialized references, jadi bisa isi turunan QuestObjectiveData/QuestRewardData langsung di inspector
    [SerializeReference]
    public List<QuestObjectiveData> Objectives = new List<QuestObjectiveData>();

    [SerializeReference]
    public List<QuestRewardData<Player>> Rewards = new List<QuestRewardData<Player>>();

    public Quest<Player> CreateData()
    {
        var objectivesInstances = new List<QuestObjective>();
        foreach (var o in Objectives)
            objectivesInstances.Add(o.CreateInstance());

        var rewardInstances = new List<IQuestReward<Player>>();
        foreach (var r in Rewards)
            rewardInstances.Add((IQuestReward<Player>)r.CreateInstance());

        return new Quest<Player>(objectivesInstances, rewardInstances, QuestId, QuestState.InProgress);
    }
}