using UnityEngine;

public class TriggerPrompt : MonoBehaviour
{
    [SerializeField] private JournalUpdatedPromptScript journalPrompt;
    private bool isTriggeredAlready = false;

    private void OnTriggerEnter(Collider other)
    {
        if (journalPrompt != null && !isTriggeredAlready)
        {
            journalPrompt.ShowPrompt();
            isTriggeredAlready = true;
        }
        else
        {
            print(journalPrompt);
            //Debug.LogError("JournalUpdatedPromptScript reference is missing!");
        }
    }
}