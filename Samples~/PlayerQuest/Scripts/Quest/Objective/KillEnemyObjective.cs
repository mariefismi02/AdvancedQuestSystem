using mariefismi02.Quest;
using UnityEngine;

[QuestComposite]
public class KillEnemyObjective : QuestObjective
{
    private string enemyId;
    private int currentCount, targetCount;

    public override string Description => $"Kill {targetCount} of enemy '{enemyId}'";

    public KillEnemyObjective(string enemyId, int targetCount)
    {
        this.enemyId = enemyId;
        this.targetCount = targetCount;
    }
    
    private void OnEnemyKilled(string id)
    {
        if (id != enemyId)
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
        GameEvent.Instance.OnEnemyKilled.AddListener(OnEnemyKilled);
    }

    public override void Cleanup()
    {
        GameEvent.Instance.OnEnemyKilled.RemoveListener(OnEnemyKilled);
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
