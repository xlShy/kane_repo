using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerCrosshairScript : MonoBehaviour
{
    [SerializeField] private InteractionHandler interactionHandlerScript;

    private Image crosshairImage;
    private void Start()
    {
        interactionHandlerScript.isInteracting.AddListener(DisableCrosshair);
        interactionHandlerScript.isNotInteracting.AddListener(EnableCrosshair);
        crosshairImage = GetComponent<Image>();
    }

    private void DisableCrosshair()
    {
        crosshairImage.enabled = false;
    }

    private void EnableCrosshair()
    {
        crosshairImage.enabled = true;
    }
}
