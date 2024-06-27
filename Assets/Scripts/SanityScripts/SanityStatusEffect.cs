using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityStatusEffect : MonoBehaviour
{
    private SanityHandler sanityHandler;
    public Transform playerObject;
    public Transform shedSpawnPoint;

    public RawImage visionDarken;
    public bool isVisionReturning = false;
    public float darkenDuration = 3f; 
    public float returnDuration = 3f; 
    private Coroutine visionCoroutine;

    private void Awake()
    {
        sanityHandler = GetComponent<SanityHandler>();
    }
    public void CheckSanityValue(float sanityValue)
    {
        if (sanityValue <= 0)
        {
            sanityHandler.isSanityDepleted = true;
            OnDepletedSanity();
        }
        else if (sanityValue <= .5f)
        {
            OnLowSanity();
        }
        else if (sanityValue > .5f)
        {
            OnHighSanity();
        }
    }
    public void OnDepletedSanity()
    {
        CharacterController controller = playerObject.GetComponent<CharacterController>();//turning off the CC to allow changes in the transform
        if (controller != null)
        {
            controller.enabled = false;
            playerObject.position = shedSpawnPoint.position;

        }
        controller.enabled = true;
        ResetVision();
        sanityHandler.ResetSanity();
    }
    public void OnLowSanity()
    {
        //print("vision darken");
        if (!isVisionReturning && visionCoroutine == null)
        {
            visionCoroutine = StartCoroutine(ChangeVision(0.65f, darkenDuration));
        }
    }
    public void OnHighSanity()
    {
        //print("return vision");
        if (visionCoroutine != null)
        {
            StopCoroutine(visionCoroutine);
        }
        visionCoroutine = StartCoroutine(ChangeVision(0f, returnDuration));
    }
    private IEnumerator ChangeVision(float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = visionDarken.color;
        float startAlpha = color.a;

        isVisionReturning = (targetAlpha < startAlpha);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            visionDarken.color = color;
            yield return null;
        }
        print("vision Darken");
        color.a = targetAlpha;
        visionDarken.color = color;

        isVisionReturning = false;
        visionCoroutine = null;
    }
    private void ResetVision()
    {
        if (visionCoroutine != null)
        {
            StopCoroutine(visionCoroutine);
            visionCoroutine = null;
        }

        Color color = visionDarken.color;
        color.a = 0f; 
        visionDarken.color = color;

        isVisionReturning = false;
    }
}
