using mariefismi02.Quest;
using UnityEngine;

[QuestComposite(typeof(Player))]
public class ExpReward : IQuestReward<Player>
{
    private int expAmount;

    public ExpReward(int expAmount)
    {
        this.expAmount = expAmount;
    }

    public void Give(Player target)
    {
        target.AddExp(expAmount);
    }
}
