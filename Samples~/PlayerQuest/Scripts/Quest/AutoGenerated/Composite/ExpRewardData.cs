
using mariefismi02.Quest;
using mariefismi02.Quest.Asset;
using System;
using UnityEngine;

[Serializable]
public class ExpRewardData : QuestRewardData<Player>
{
    public int ExpAmount;

    public override IQuestReward<Player> CreateInstance()
    {
        return new ExpReward(ExpAmount);
    }
}