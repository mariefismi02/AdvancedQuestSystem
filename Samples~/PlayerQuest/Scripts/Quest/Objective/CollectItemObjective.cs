using mariefismi02.Quest;
using UnityEngine;

[QuestComposite]
public class CollectItemObjective : QuestObjective
{
    private string itemId;
    private int currentCount, targetCount;

    //TODO: pindahin ke scriptable object
    public override string Description => $"Collect {targetCount} of item '{itemId}'";

    public CollectItemObjective(string itemId, int targetCount)
    {
        this.itemId = itemId;
        this.targetCount = targetCount;
    }

    private void OnCollectItem(string id)
    {
        if (id != itemId)
        {
            return;
        }

        if (IsCompleted)
        {
            return;
        }

        currentCount++;

        OnProgressUpdated.Invoke(currentCount, targetCount);

        if (currentCount >= targetCount)
        {
            IsCompleted = true;
            OnCompleted.Invoke();
        }
    }
    
    public override void Init()
    {
        GameEvent.Instance.OnCollectItem.AddListener(OnCollectItem);
    }

    public override void Cleanup()
    {
        GameEvent.Instance.OnCollectItem.RemoveListener(OnCollectItem);
    }

    public override int GetProgressValue()
    {
        var progress = currentCount / (float)targetCount;
        return Mathf.RoundToInt(progress * 100);
    }

    public override void SetProgressValue(int value)
    {
        currentCount = (targetCount * value) / 100;
    }

    public override (int current, int target) GetProgress()
    {
        return (currentCount, targetCount);
    }
}
