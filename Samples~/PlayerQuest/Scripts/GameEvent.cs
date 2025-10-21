using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public static GameEvent Instance {get; private set;}

    public UnityEvent<string> OnEnemyKilled { get; private set; } = new();
    public UnityEvent<string> OnCollectItem { get; private set; } = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void KillEnemy(string id)
    {
        OnEnemyKilled.Invoke(id);
    }

    public void CollectItem(string id)
    {
        OnCollectItem.Invoke(id);
    }
    
    
}