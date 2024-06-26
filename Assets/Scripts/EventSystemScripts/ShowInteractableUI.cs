using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowInteractableUI : MonoBehaviour
{
    public Image interactionImage;

    public void SetInteractionPrompt(Sprite image)
    {
        interactionImage.sprite = image;

    }

    public void EnableInteractableUI()
    {
        interactionImage.gameObject.SetActive(true);
    }

    public void DisableInteractableUI()
    {
        interactionImage.gameObject.SetActive(false);
    }
}
