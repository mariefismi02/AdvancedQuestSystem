
using mariefismi02.Quest;
using mariefismi02.Quest.Asset;
using System;
using UnityEngine;

[Serializable]
public class CollectItemObjectiveData : QuestObjectiveData
{
    public String ItemId;
    public int TargetCount;

    public override QuestObjective CreateInstance()
    {
        return new CollectItemObjective(ItemId, TargetCount);
    }
}