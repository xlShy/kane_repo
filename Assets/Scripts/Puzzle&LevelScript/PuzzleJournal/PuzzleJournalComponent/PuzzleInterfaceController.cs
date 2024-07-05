using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

    //public List<Puzzle> puzzleRecords;
    public List<GameObject> puzzleRecordBoxes; 

    private void OnEnable()
    {
        GrandfatherClock.OnPuzzleComplete += AddToPuzzleJournals;
        CombinationLockActivateScript.OnPuzzleComplete += AddToPuzzleJournals;
    }
    public void AddToPuzzleJournals(Puzzle puzzleJournalData)
    {
        //puzzleRecords.Add(puzzleJournalData);
        InstantiatePuzzleRecordButton(puzzleJournalData);
    }
    private void InstantiatePuzzleRecordButton(Puzzle puzzleJournalData)
    {
        GameObject puzzleRecord = Instantiate(puzzleRecordBox, puzzleRecordBoxParent.transform);
        TextMeshProUGUI puzzleName = puzzleRecord.transform.Find(recordName.name).GetComponent<TextMeshProUGUI>();
        puzzleName.text = puzzleJournalData.name;
        puzzleRecordBoxes.Add(puzzleRecord);

        PuzzleRecordBox recordBoxScript = puzzleRecord.GetComponent<PuzzleRecordBox>();
        if (recordBoxScript != null)
        {
            recordBoxScript.SetPuzzleData(puzzleJournalData, this);
        }
    }
    public void ShowPuzzleData(Puzzle data)
    {
        puzzleImageHolder.GetComponent<Image>().sprite = data.puzzleIcon;
        puzzleName.GetComponent<TextMeshProUGUI>().text = data.puzzleName;
        puzzleDescription.GetComponent<TextMeshProUGUI>().text = data.puzzleDescription;
    }
}
