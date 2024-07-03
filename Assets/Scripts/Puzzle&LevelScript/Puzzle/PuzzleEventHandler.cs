using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEventHandler : MonoBehaviour, IPuzzleHandler
{
    public event Action<Puzzle> OnCompletePuzzle;
    public void InteractPuzzle(Puzzle puzzle)
    {
        OnCompletePuzzle?.Invoke(puzzle);
    }
}
