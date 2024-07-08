using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillEffect : MonoBehaviour
{
    public bool isPillActive = false;
    [SerializeField] private float pillDelayDuration = 0.90f;
    [SerializeField] private float pillEffectDuration = 2f;
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
}
