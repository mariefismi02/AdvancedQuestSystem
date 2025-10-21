using NaughtyAttributes;
using UnityEngine;

public class GameEventTester : MonoBehaviour
{
    [SerializeField]
    private string enemyId = "Enemy_001";
    
    [Button]
    public void KillEnemy()
    {
        GameEvent.Instance.KillEnemy(enemyId);
    }

    [SerializeField]
    private string itemId = "Item_001";

    [Button]
    public void CollectItem()
    {
        GameEvent.Instance.CollectItem(itemId);
    }
}
