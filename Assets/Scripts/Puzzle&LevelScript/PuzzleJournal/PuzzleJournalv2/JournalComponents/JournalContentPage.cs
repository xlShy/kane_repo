using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Page #", menuName = "PuzzleSystem/PuzzleJournal/Page")]
public class JournalContentPage : ScriptableObject
{
    public int pageNumber;
    public List<Sprite> pages;
}
