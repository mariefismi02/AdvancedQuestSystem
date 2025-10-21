using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestObjectiveViewer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private TMP_Text progressText;
    [SerializeField]
    private Image progressBar;

    public void SetProgress(int currentCount, int targetCount)
    {
        progressBar.fillAmount = (float) currentCount / targetCount;
        progressText.text = $"{currentCount}/{targetCount}";
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}
