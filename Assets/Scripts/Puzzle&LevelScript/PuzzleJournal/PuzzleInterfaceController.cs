using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInterfaceController : MonoBehaviour
{
    [Header("UI When Puzzle Record Is Clicked")]
    [SerializeField] private GameObject puzzleImageHolder;
    [SerializeField] private GameObject puzzleName;
    [SerializeField] private GameObject puzzleDescription;

    [Header("Puzzle Records")]
    [SerializeField] private GameObject puzzleRecordBoxParent;
    [SerializeField] private GameObject puzzleRecordBox;
    [SerializeField] private GameObject recordName;

    public List<Puzzle> puzzleRecords;
    public List<GameObject> puzzleRecordBoxes; 

    private void OnEnable()
    {
        GrandfatherClock.OnPuzzleComplete += AddToPuzzleJournals;
        CombinationLockActivateScript.OnPuzzleComplete += AddToPuzzleJournals;
    }
    public void AddToPuzzleJournals(Puzzle puzzleJournalData)
    {
        puzzleRecords.Add(puzzleJournalData);
        InstantiatePuzzleRecordButton();
    }
    private void InstantiatePuzzleRecordButton()
    {
        GameObject puzzleRecord = Instantiate(puzzleRecordBox, puzzleRecordBoxParent.transform);
        puzzleRecordBoxes.Add(puzzleRecord);

    }
    public void ShowPuzzleRecord()
    {

    }
}
