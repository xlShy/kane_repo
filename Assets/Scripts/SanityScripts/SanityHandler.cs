using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SanityHandler : MonoBehaviour
{
    private SanityStatusEffect sanityChecker;
    private PillEffect pillEffect;

    [SerializeField] public float increasePercentage = 0.01f;
    [SerializeField] public float decreasePercentage = 0.01f;
    [SerializeField] private float originalInterval = 1f;
    [SerializeField] private float intervalValueOnCanvasEnabled;
    public float currentInterval;
    public float sanityValue;

    
    public bool isSanityDecreasing = false;
    public bool isSanityIncreasing = false;
    public bool isSanityDepleted = false;

    public bool isCanvasOn = false;

    public UnityEvent OnPlayerShed;
    public UnityEvent OnPlayerHouse;
    public UnityEvent OnPlayerOutside;

    private void Awake()
    {
        sanityChecker = GetComponent<SanityStatusEffect>();
        pillEffect = GetComponent<PillEffect>();
    }
    private void Start()
    {
        sanityValue = 1f;
    }
    private void OnEnable()
    {
        PlayerLocationChecker.OnEnterHouse += PlayerOnHouse;
        PlayerLocationChecker.OnEnterShed += PlayerOnShed;
        PlayerLocationChecker.OnExitOutside += PlayerOnOutside;
        RoomChecker.OnRoomChanged += SetSanityDecreaseRate;

        //canvas is on
        CanvasManager.OnCanvasEnabled += IsCanvasOn;
    }
    private void OnDisable()
    {
        PlayerLocationChecker.OnEnterHouse -= PlayerOnHouse;
        PlayerLocationChecker.OnEnterShed -= PlayerOnShed;
        PlayerLocationChecker.OnExitOutside -= PlayerOnOutside;
        RoomChecker.OnRoomChanged -= SetSanityDecreaseRate;

        //canvas is on
        CanvasManager.OnCanvasEnabled += IsCanvasOn;
    }
    private void Update()
    {
        if (isSanityDecreasing)
        {
            sanityChecker.CheckSanityValue(sanityValue);
            DecreaseSanityOnInterval();
        }
        else if (isSanityIncreasing)
        {
            sanityChecker.CheckSanityValue(sanityValue);    
            IncreaseSanityOnInterval();
        }
        else if (isSanityDepleted)
        {
            sanityChecker.CheckSanityValue(sanityValue);
            ResetSanity();
        }
    }
    public void SetSanity(float amountSet)
    {
        sanityValue = amountSet;
    }
    private void DecreaseSanityOnInterval()
    {
        ChangeSanity(-decreasePercentage);
    }
    private void IncreaseSanityOnInterval()
    {
        isSanityDepleted = false;
        ChangeSanity(increasePercentage);
    }
    private void ChangeSanity(float changeAmount)
    {
        float adjustedChangeAmount = pillEffect.GetPillEffect(changeAmount); //Stores the calculated amount of the duration and effect amount of the Pill(Consumable Item)

        currentInterval = isCanvasOn ? intervalValueOnCanvasEnabled : originalInterval;

        sanityValue += adjustedChangeAmount * currentInterval * Time.deltaTime;
        sanityValue = Mathf.Clamp01(sanityValue);
    }
    private void PlayerOnHouse()
    {
        print("on house");
        OnPlayerHouse?.Invoke();
        isSanityDecreasing = true;
    }
    private void PlayerOnShed()
    {
        print("on shed");
        OnPlayerShed?.Invoke();
        isSanityIncreasing = true;
    }
    private void PlayerOnOutside()
    {
        print("on outside");
        OnPlayerOutside?.Invoke();
        isSanityDecreasing = false;
        isSanityIncreasing = false;
    }
    public void ResetSanity()
    {
        isSanityDecreasing = false;
        isSanityIncreasing = true;
        isSanityDepleted = true;
    }
    public void SetSanityDecreaseRate(float sanityRate)
    {
        decreasePercentage = sanityRate;
    }
    public void DisableSanity()
    {
        isSanityDecreasing = false;
    }
    public void EnableSanity()
    {
        isSanityDecreasing = true;
    }
    private void IsCanvasOn(bool isOn)
    {
        isCanvasOn = isOn;
    }
}
