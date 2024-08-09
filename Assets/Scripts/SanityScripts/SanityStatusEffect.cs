using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class SanityStatusEffect : MonoBehaviour
{
    private SanityHandler sanityHandler;
    private SanityPostProcessing postProcessing;

    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private AudioHandler audioHandler;
    [SerializeField] private DeathScreenEnabler deathScreenEnabler;

    public Transform playerObject;

    public RawImage visionDarken;
    public float darkenDuration = 3f;
    public float returnDuration = 3f;
    private Coroutine visionChangeCoroutine;

    [Header("Threshhold for the sanity bar if it reaches the value")]
    [SerializeField] private float threshHold1 = .6f;
    [SerializeField] private float threshHold2 = .3f;
    [SerializeField] private float threshHold3 = .1f;

    [SerializeField] private bool isDead;
    [SerializeField] private bool threshold1Triggered = false;
    [SerializeField] private bool threshold2Triggered = false;
    [SerializeField] private bool threshold3Triggered = false;

    private void Awake()
    {
        sanityHandler = GetComponent<SanityHandler>();
        postProcessing = GetComponent<SanityPostProcessing>();
    }
    public void CheckSanityValue(float sanityValue)
    {
        
        if (sanityValue <= 0 && !isDead)
        {
            audioHandler.StopSanityAudio();
            isDead = true;
            sanityHandler.isSanityDepleted = true;
            StartCoroutine(WaitForFadeAndCallDepletedSanity());
        }
        else if (sanityValue <= threshHold3 && !sanityHandler.isSanityIncreasing)
        {            
            if (threshold3Triggered && !sanityHandler.isSanityIncreasing)
                return;
            postProcessing.StartSanityEffect(0.7f, 0.9f);
            threshold3Triggered = true;
            threshold2Triggered = false;
            threshold1Triggered = false;

            audioHandler.StopSanityAudio();
            audioHandler.PlaySelectedAudio(1);
            
        }
        else if (sanityValue <= threshHold2 && !sanityHandler.isSanityIncreasing)
        {
            if (threshold2Triggered)
                return;
            postProcessing.StartSanityEffect(0.4f, 0.7f);
            threshold2Triggered = true;
            threshold1Triggered = false;

            audioHandler.PlaySelectedAudio(0);
        }
        else if (sanityValue <= threshHold1)
        {
            if (threshold1Triggered)
                return;
            postProcessing.StartSanityEffect(0.0f, 0.4f);
            threshold1Triggered = true;
        }
        else
        {
            audioHandler.StopSanityAudio();
            postProcessing.StopBreathingVignette();
            ResetThresholds();
        }
    }
    private void ResetThresholds()
    {
        threshold1Triggered = false;
        threshold2Triggered = false;
        threshold3Triggered = false;
    }
    public IEnumerator OnDepletedSanity()
    {
        deathScreenEnabler.isDoneShowing = false;
        canvasManager.DisableAllCanvas(canvasManager.UICanvas);
        canvasManager.DisableAllCanvas(canvasManager.PuzzleCanvas);

        deathScreenEnabler.EnableDeathScreen();
        yield return new WaitForSeconds(3f);
        CharacterController controller = playerObject.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            playerObject.position = ShedRespawnPoint.Instance.ShedSpawnPoint.gameObject.transform.position; //takes the shedrespawnpoint 
            controller.enabled = true;
            sanityHandler.ResetSanity();
        }
        sceneFader.FadeIn();      
        yield return new WaitUntil(() => sceneFader.isDoneFading);
        isDead = false;
    }

    private IEnumerator WaitForFadeAndCallDepletedSanity()
    {
        print("hi");
        sceneFader.FadeOut();
        yield return new WaitUntil(() => sceneFader.isDoneFading);
        StartCoroutine(OnDepletedSanity());
    }
}
 