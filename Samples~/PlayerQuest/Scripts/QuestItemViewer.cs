using mariefismi02.Quest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemViewer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private GameObject lockedImage;
    [SerializeField]
    private GameObject completedImage;
    [SerializeField]
    private QuestObjectiveViewer[] objectiveViewers;

    public void SetData<T>(Quest<T> quest)
    {
        titleText.text = quest.QuestId;
        switch(quest.State)
        {
            case QuestState.Locked:
                lockedImage.SetActive(true);
                completedImage.SetActive(false);
                break;
            case QuestState.Completed:
                lockedImage.SetActive(false);
                completedImage.SetActive(true);
                break;
            default:
                lockedImage.SetActive(false);
                completedImage.SetActive(false);
                break;
        }

        quest.OnCompleted.AddListener(SetComplete);

        for (int i = 0; i < objectiveViewers.Length; i++)
        {
            if (i < quest.Objectives.Count && quest.State == QuestState.InProgress)
            {
                objectiveViewers[i].gameObject.SetActive(true);
                objectiveViewers[i].SetDescription(quest.Objectives[i].Description);
                var progress = quest.Objectives[i].GetProgress();
                objectiveViewers[i].SetProgress(progress.current, progress.target);
                quest.Objectives[i].OnProgressUpdated.AddListener(objectiveViewers[i].SetProgress);
            }
            else
            {
                objectiveViewers[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetComplete(IQuest quest)
    {
        completedImage.SetActive(true);
        for (int i = 0; i < objectiveViewers.Length; i++)
        {
            objectiveViewers[i].gameObject.SetActive(false);
        }
    }
}
