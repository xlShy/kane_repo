using UnityEngine;

public class ImageTrigger : MonoBehaviour
{
    public ImageFader imageFader;
    private bool isEnabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnabled)
        {
            isEnabled = true;
            imageFader.StartFadeIn();
        }
    }
}