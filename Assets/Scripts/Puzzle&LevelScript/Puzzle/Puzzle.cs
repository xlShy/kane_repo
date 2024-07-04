using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleContent", menuName = "PuzzleSystem/Puzzle")]
public class Puzzle : ScriptableObject
{
    public string puzzleName;
    [TextArea(4, 12)]
    public string puzzleDescription;

    public Sprite puzzleIcon;
    public bool isPuzzleCompleted = false;

    private void OnDisable()
    {
        isPuzzleCompleted = false;
    }
}
