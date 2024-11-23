using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BasementDoorScript : InteractableObject
{
    private DoorBase doorBase;
    private Level1Completed level1Completed;
    [SerializeField] private doorLockerScript doorLockerScript;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private SanityHandler sanityHandler;
    [SerializeField] private float openSpeed = 5f;
    [SerializeField] private string scene2Load;

    private AsyncOperation asyncLoad;

    public AudioSource knockingSource;
    private void Awake()
    {
        doorBase = GetComponent<DoorBase>();
        level1Completed = GetComponent<Level1Completed>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (!doorBase.isOpen && doorBase.canOpen)
        {
            doorBase.OpenDoor(openSpeed);
            //level1Completed.CompleteLevel1();
            StartCoroutine(LoadCreditsScene());
        }
        else if (!doorBase.isOpen && !doorBase.canOpen)
        {
            doorBase.doorIsLocked.Play();
        }
        else if (doorBase.isOpen)
        {
            doorBase.CloseDoor(openSpeed);
        }
    }
    public void PlayKnockingNoise()
    {
        knockingSource.Play();
    }
    public void StopKnockingNoise()
    {
        knockingSource.Stop();
    }
    IEnumerator LoadCreditsScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(scene2Load);
        asyncLoad.allowSceneActivation = false;  // Add this line
        yield return new WaitUntil(() => sanityHandler.sanityValue == 0);
        
        sceneFader.FadeOut();
        yield return new WaitUntil(() => sceneFader.isDoneFading);

        asyncLoad.allowSceneActivation = true;
    }
}
