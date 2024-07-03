using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressAnyButtontoStart : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pressSound;
    public Animator animator;
    private int leveltoLoad;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            audioSource.PlayOneShot(pressSound);
            fade(1);
            //SceneManager.LoadScene("Main Menu");
        }
    }
    public void fade(int sceneIndex)
    {
        leveltoLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }
    public void fadefinish()
    {
        SceneManager.LoadScene(leveltoLoad);
    }
}
