using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRecordBox : MonoBehaviour
{
    public Puzzle data;

    public PuzzleInterfaceController controller;

    public void OnClickRecordBox()
    {
        controller.ShowPuzzleData(data);
    }
    public void SetPuzzleData(Puzzle recordData, PuzzleInterfaceController UIController)
    {
        data = recordData;
        controller = UIController;
    }
}
