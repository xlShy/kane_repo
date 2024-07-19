using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class quitGame : MonoBehaviour
{
    public void ExitGame()
    {
        //if (EditorApplication.isPlaying == true)
        //{
        //    EditorApplication.isPlaying = false;
        //}
        //else
        //{
        //    Application.Quit();
        //}
        Application.Quit();
    }
}
