using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] playerBlocker;
    public PuzzleEventHandler pEventHandler;

    public List<Level> LevelList = new List<Level>();
    

    public bool isGameComplete = false;
    public int iteration;

    public static event Action OnCompleteLevel1;

    private void OnEnable()
    {
        pEventHandler.OnCompletePuzzle += OnPuzzleComplete;
    }
    private void OnDisable()
    {
        pEventHandler.OnCompletePuzzle -= OnPuzzleComplete;
    }
    public void CheckLevelStatus() 
    {
        for (int i = 0; i < LevelList.Count; i++)
        {    
            Level level = LevelList[i];

            if (!level.isLevelCompleted)
            {
                bool allPuzzlesCompleted = true;
                foreach (Puzzle puzzle in level.puzzles)
                {
                    if (!puzzle.isPuzzleCompleted)
                    {
                        allPuzzlesCompleted = false;
                        return;
                    }
                }
                if (allPuzzlesCompleted)
                {
                    level.isLevelCompleted = true;
                    if (iteration < LevelList.Count - 1)
                    {
                        iteration++;
                    }
                    OnLevelComplete();
                }
            }
        }
        isGameComplete = true;
        OnGameComplete();
    }
    private void OnLevelComplete()
    {
        //add cases if level is added
        switch (iteration)
        {
            case 1:
                //print("Proceed to level 2");
                OnCompleteLevel1?.Invoke();
                break;
            case 2:
                //print("Proceed to level 3");
                //Destroy(playerBlocker[iteration - 1]);
                break;
            default:
                break;
        }
    }
    private void OnPuzzleComplete(Puzzle puzzle)
    {
        puzzle.isPuzzleCompleted = true;
        //print("Puzzle Completed");
        CheckLevelStatus();
    }
    private void OnGameComplete()
    {
        //print("Game is Finished");
    }
}
