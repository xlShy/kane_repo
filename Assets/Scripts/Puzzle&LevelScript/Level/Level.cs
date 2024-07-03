using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContent", menuName = "PuzzleSystem/Level")]
public class Level : ScriptableObject
{
    public Puzzle[] puzzles;

    public bool isLevelCompleted = false;

    private void OnDisable()
    {
        isLevelCompleted = false;
    }
}
