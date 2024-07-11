using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugController : MonoBehaviour
{
    public Canvas debugCanvas; // Reference to the canvas
    public TextMeshProUGUI flashlightText;
    public TextMeshProUGUI sanityText;
    public TextMeshProUGUI speedText; // Text for displaying player speed
    
    public TMP_InputField flashlightInputField; // Input field for flashlight value
    public TMP_InputField sanityInputField;     // Input field for sanity value
    public TMP_InputField speedInputField;      // Input field for speed value

    public GameObject pillPrefab;               // Reference to the pill prefab
    public Transform playerTransform;           // Reference to the player's transform
    public Button spawnPillButton;              // Reference to the spawn pill button
    public Button resetFlashlightDrainButton;   // Reference to the reset flashlight drain button
    public Button resetSanityDrainButton;       // Reference to the reset sanity drain button
    public GameObject sanityGameObject;  
    public GameObject flashlightGameObject;
    public GameObject playerGameObject;  

    
    private PlayerMovement playerMovement;
    private SanityHandler sanityHandler;  

    private PlayerFlashlight playerFlashlight;  

    void Start()
    {
        if (sanityGameObject != null)
        {
            sanityHandler = sanityGameObject.GetComponent<SanityHandler>();
        }

        if (flashlightGameObject != null)
        {
            playerFlashlight = flashlightGameObject.GetComponent<PlayerFlashlight>();
        }

        if (playerGameObject != null)
        {
            playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        }

        // Update sanity text from sanityHandler if available
        if (sanityHandler != null)
        {
            UpdateSanityText(sanityHandler.sanityValue);
        }

        if (playerFlashlight != null)
        {
            UpdateFlashlightText(playerFlashlight.currentBattery);
        }

        if (playerMovement != null)
        {
            UpdateSpeedText(playerMovement.speed);
        }

        // Add listeners for input fields
        flashlightInputField.onEndEdit.AddListener(OnFlashlightInputFieldChanged);
        sanityInputField.onEndEdit.AddListener(OnSanityInputFieldChanged);
        speedInputField.onEndEdit.AddListener(OnSpeedInputFieldChanged);


        // Add listener for the spawn pill button
        if (spawnPillButton != null)
        {
            spawnPillButton.onClick.AddListener(SpawnPillAtPlayerFeet);
        }

        // Add listeners for the reset drain buttons
        if (resetFlashlightDrainButton != null)
        {
            resetFlashlightDrainButton.onClick.AddListener(ResetFlashlightDrain);
        }

        if (resetSanityDrainButton != null)
        {
            resetSanityDrainButton.onClick.AddListener(ResetSanityDrain);
        }

        // Ensure the canvas starts hidden
        debugCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (sanityGameObject != null)
        {
            sanityHandler = sanityGameObject.GetComponent<SanityHandler>();
        }

        if (flashlightGameObject != null)
        {
            playerFlashlight = flashlightGameObject.GetComponent<PlayerFlashlight>();
        }

        if (playerGameObject != null)
        {
            playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        }

        // Update sanity text from sanityHandler if available
        if (sanityHandler != null)
        {
            UpdateSanityText(sanityHandler.sanityValue);
        }

        // Update flashlight text from flashlightHandler if available
        if (playerFlashlight != null)
        {
            UpdateFlashlightText(playerFlashlight.currentBattery);
        }

        // Update speed text from playerMovement if available
        if (playerMovement != null)
        {
            UpdateSpeedText(playerMovement.speed);
        }

        // Check for the "~" key press to toggle the debug canvas
        if (Input.GetKeyDown(KeyCode.BackQuote)) // BackQuote is the "~" key
        {
            ToggleDebugCanvas();
        }
    }

    private void UpdateFlashlightText(float value)
    {
        flashlightText.text = $"{value:F2}";
    }

    private void UpdateSanityText(float value)
    {
        sanityText.text = $"{value:F2}";
    }

    private void UpdateSpeedText(float value)
    {
        speedText.text = $"{value:F2}";
    }

    public void OnFlashlightInputFieldChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            if (playerFlashlight != null)
            {
                playerFlashlight.currentBattery = result;  // Update the flashlight value in the handler
            }
            UpdateFlashlightText(result);
        }
    }

    public void OnSanityInputFieldChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            if (sanityHandler != null)
            {
                sanityHandler.sanityValue = result;  // Update the sanity value in the handler
            }
            UpdateSanityText(result);
        }
    }

    public void OnSpeedInputFieldChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            if (playerMovement != null)
            {
                playerMovement.speed = result;  // Update the player speed in the handler
            }
            UpdateSpeedText(result);
        }
    }

    private void SpawnPillAtPlayerFeet()
    {
        if (pillPrefab != null && playerTransform != null)
        {
            Instantiate(pillPrefab, playerTransform.position, Quaternion.identity);
        }
    }

    private void ResetFlashlightDrain()
    {
        if (playerFlashlight != null)
        {
            playerFlashlight.batteryDrainRate = 0; // Set battery drain rate to 0
        }
    }

    private void ResetSanityDrain()
    {
        if (sanityHandler != null)
        {
            sanityHandler.decreasePercentage = 0; // Set sanity decrease percentage to 0
        }
    }

    private void ToggleDebugCanvas()
    {
        debugCanvas.enabled = !debugCanvas.enabled;

        if (debugCanvas.enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
