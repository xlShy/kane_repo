using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour
{
    public void ExitGame()
    {
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
