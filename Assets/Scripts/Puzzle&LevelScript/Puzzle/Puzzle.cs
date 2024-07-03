using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleContent", menuName = "PuzzleSystem/Puzzle")]
public class Puzzle : ScriptableObject
{
    public string puzzleName;
    public bool isPuzzleCompleted = false;

    private void OnDisable()
    {
        isPuzzleCompleted = false;
    }
}
