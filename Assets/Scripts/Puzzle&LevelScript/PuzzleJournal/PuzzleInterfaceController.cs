using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject puzzleImageHolder;
    [SerializeField] private GameObject puzzleName;
    [SerializeField] private GameObject puzzleDescription;

    public List<Puzzle> puzzleJournals;

    private void OnEnable()
    {
        GrandfatherClock.OnPuzzleComplete += AddToPuzzleJournals;
        CombinationLockActivateScript.OnPuzzleComplete += AddToPuzzleJournals;
    }
    public void AddToPuzzleJournals(Puzzle puzzleJournalData)
    {
        puzzleJournals.Add(puzzleJournalData);
    }
    //On Puzzle Complete
    public void SetDetailsToUIConfig()
    {

    }
}
