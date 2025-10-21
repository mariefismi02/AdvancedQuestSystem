using UnityEngine;

public class PlayerQuestViewer : MonoBehaviour
{
    [SerializeField]
    private QuestItemViewer[] items;

    private void Start()
    {
        var quests = PlayerQuestManager.Instance.GetActiveQuests();
        for(int i = 0; i < items.Length; i++)
        {
            if (i < quests.Count)
            {
                items[i].gameObject.SetActive(true);
                items[i].SetData(quests[i]);
            } 
            else
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }
}
