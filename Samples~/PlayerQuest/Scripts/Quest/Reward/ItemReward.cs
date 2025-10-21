using mariefismi02.Quest;
using UnityEngine;

[QuestComposite(typeof(Player))]
public class ItemReward : IQuestReward<Player>
{
    private string itemId;
    private int amount;

    public ItemReward(string itemId, int amount)
    {
        this.itemId = itemId;
        this.amount = amount;
    }

    public void Give(Player target)
    {
        target.AddItem(itemId, amount);
    }
}
