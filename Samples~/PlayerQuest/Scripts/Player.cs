using mariefismi02.Quest;
using UnityEngine;

[QuestOwnership]
public class Player : MonoBehaviour
{
    public void AddExp(int exp)
    {
        Debug.Log("Player received exp: " + exp);
    }

    public void AddItem(string itemId, int amount)
    {
        Debug.Log("Player received item: " + itemId + " x" + amount);
    }
}