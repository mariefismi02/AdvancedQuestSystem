
using mariefismi02.Quest;
using mariefismi02.Quest.Asset;
using System;
using UnityEngine;

[Serializable]
public class KillEnemyObjectiveData : QuestObjectiveData
{
    public String EnemyId;
    public int TargetCount;

    public override QuestObjective CreateInstance()
    {
        return new KillEnemyObjective(EnemyId, TargetCount);
    }
}