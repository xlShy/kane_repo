using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleJournalHandler : MonoBehaviour
{
    [SerializeField] private GameObject puzzleJournalCanvas;

    private bool isOpen;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isOpen = !isOpen;
            puzzleJournalCanvas.SetActive(isOpen);
            //if(isOpen )
            //{
            //    Cursor.visible = true;
            //    Cursor.lockState = CursorLockMode.None;
            //}
            //else
            //{
            //    Cursor.visible = false;
            //    Cursor.lockState = CursorLockMode.Locked;
            //}
        }
    }
}
