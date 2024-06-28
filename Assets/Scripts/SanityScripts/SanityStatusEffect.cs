using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SanityStatusEffect : MonoBehaviour
{
    private SanityHandler sanityHandler;
    public Transform playerObject;
    public Transform shedSpawnPoint;
    public RawImage visionDarken;
    public float darkenDuration = 3f;
    public float returnDuration = 3f;
    private Coroutine visionChangeCoroutine;

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
        else if (sanityValue <= .1f)
        {
            OnLowSanity(.97f);
        }
        else if (sanityValue <= .3f)
        {
            OnLowSanity(.90f);
        }
        else if (sanityValue <= .6f)
        {
            OnLowSanity(.65f);
        }
        else
        {
            OnHighSanity();
        }
    }

    public void OnDepletedSanity()
    {
        CharacterController controller = playerObject.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            playerObject.position = shedSpawnPoint.position;
            controller.enabled = true;
        }
        ResetVision();
        sanityHandler.ResetSanity();
    }

    public void OnLowSanity(float alpha)
    {
        if (visionChangeCoroutine != null)
        {
            StopCoroutine(visionChangeCoroutine);
        }
        visionChangeCoroutine = StartCoroutine(ChangeVision(alpha, darkenDuration));
    }

    public void OnHighSanity()
    {
        if (visionChangeCoroutine != null)
        {
            StopCoroutine(visionChangeCoroutine);
        }
        visionChangeCoroutine = StartCoroutine(ChangeVision(0f, returnDuration));
    }

    private IEnumerator ChangeVision(float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = visionDarken.color;
        float startAlpha = color.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            visionDarken.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        visionDarken.color = color;
        Debug.Log("Vision change completed. Target alpha: " + targetAlpha);
    }

    private void ResetVision()
    {
        if (visionChangeCoroutine != null)
        {
            StopCoroutine(visionChangeCoroutine);
        }
        Color color = visionDarken.color;
        color.a = 0f;
        visionDarken.color = color;
    }
}
