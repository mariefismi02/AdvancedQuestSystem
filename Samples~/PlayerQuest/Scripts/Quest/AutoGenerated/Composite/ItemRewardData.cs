
using mariefismi02.Quest;
using mariefismi02.Quest.Asset;
using System;
using UnityEngine;

[Serializable]
public class ItemRewardData : QuestRewardData<Player>
{
    public String ItemId;
    public int Amount;

    public override IQuestReward<Player> CreateInstance()
    {
        return new ItemReward(ItemId, Amount);
    }
}