using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityHandler : MonoBehaviour
{
    private SanityStatusEffect sanityChecker;

    [SerializeField] private float increasePercentage = 0.01f;
    [SerializeField] private float decreasePercentage = 0.01f;
    [SerializeField] private float interval = 1f;
    public float sanityValue;

    public bool isSanityDecreasing = false;
    public bool isSanityIncreasing = false;
    public bool isSanityDepleted = false;

    private void Awake()
    {
        sanityChecker = GetComponent<SanityStatusEffect>();
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
    }
    private void OnDisable()
    {
        PlayerLocationChecker.OnEnterHouse -= PlayerOnHouse;
        PlayerLocationChecker.OnEnterShed -= PlayerOnShed;
        PlayerLocationChecker.OnExitOutside -= PlayerOnOutside;
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
    private void DecreaseSanityOnInterval()
    {
        ChangeSanity(-decreasePercentage);
    }
    private void IncreaseSanityOnInterval()
    {
        ChangeSanity(increasePercentage);
    }
    private void ChangeSanity(float changeAmount)
    {
        sanityValue += changeAmount * interval * Time.deltaTime;
        sanityValue = Mathf.Clamp01(sanityValue);
    }
    private void PlayerOnHouse()
    {
        isSanityDecreasing = true;
    }
    private void PlayerOnShed()
    {
        isSanityIncreasing = true;
    }
    private void PlayerOnOutside()
    {
        isSanityDecreasing = false;
        isSanityIncreasing = false;
    }
    public void ResetSanity()
    {
        sanityValue = 1f;
        isSanityDecreasing = false;
        isSanityIncreasing = false;
        isSanityDepleted = false;
    }
    public void SetSanityDecreaseRate(float sanityRate)
    {
        decreasePercentage = sanityRate;
    }
}
