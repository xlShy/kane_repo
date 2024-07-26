using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillEffect : MonoBehaviour
{
    public bool isPillActive = false;
    [SerializeField] private float pillDelayDuration = 0.90f;
    [SerializeField] private float pillEffectDuration = 2f;

    private SanityHandler sanityHandler;

    private void Awake()
    {
        sanityHandler = GetComponent<SanityHandler>();
    }
    private void OnEnable()
    {
        PillsItem.OnUsePills += OnPillTaken;
    }
    private void OnDisable()
    {
        PillsItem.OnUsePills -= OnPillTaken;
    }
    private void OnPillTaken(bool hasTakenPill)
    {
        isPillActive = hasTakenPill;
        IncreaseSanity();
    }
    private IEnumerator PillEffectDuration()
    {
        isPillActive = true;
        yield return new WaitForSeconds(pillEffectDuration * 60);  // Convert minutes to seconds
        isPillActive = false;
    }
    public float GetPillEffect(float changeAmount)
    {
        if (isPillActive && changeAmount < 0)
        {
            return changeAmount * (1 - pillDelayDuration);  // Reduce the decrease by pillDelayDuration(%) when pills are active
        }
        return changeAmount;
    }
    public void IncreaseSanity()
    {
        sanityHandler.sanityValue += 0.4f;
    }
}
