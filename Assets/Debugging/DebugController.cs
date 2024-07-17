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
    public Button resetSanityDrainButton;       // Reference to the reset sanity drain button
    public GameObject sanityGameObject;  
    public GameObject flashlightGameObject;
    public GameObject playerGameObject;  

    public Button teleShed;              
    public Button teleFrontDoor;   
    public Button teleGate;  
    public Button teleOffice;              
    public Button teleKitchen;   
    public Button teleFirstFloorStairs;  
    public Button teleSecondFloorStairs;  
    public Button teleMBedroom;
    public Button teleVBedroom;   
    public Button teleBBedroom;  
    
    private PlayerMovement playerMovement;
    private SanityHandler sanityHandler;  

    private PlayerFlashlight playerFlashlight;  

    private Dictionary<Button, Vector3> teleportLocations;


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


        if (resetSanityDrainButton != null)
        {
            resetSanityDrainButton.onClick.AddListener(ResetSanityDrain);
        }

        // Define teleport locations
        teleportLocations = new Dictionary<Button, Vector3>
        {
            { teleShed, new Vector3(21.2f, 2.5f, 25.6f) }, // Replace with coordinates if needed
            { teleFrontDoor, new Vector3(1f, 2.5f, 0.1f) },
            { teleGate, new Vector3(39.7f, 2.5f, -0.1f) },
            { teleOffice, new Vector3(-20f, 2.6f, 24f) },
            { teleKitchen, new Vector3(0f, 2.6f, -16f) },
            { teleFirstFloorStairs, new Vector3(-26f, 2.6f, -2f) },
            { teleSecondFloorStairs, new Vector3(27f, 9.5f, -2f) },
            { teleMBedroom, new Vector3(-1f, 9.5f, 8f) },
            { teleVBedroom, new Vector3(-18f, 9.5f, -9f) },
            { teleBBedroom, new Vector3(-1f, 9.5f, -9f) },
        };

        // Add listeners for teleport buttons
        foreach (var kvp in teleportLocations)
        {
            Vector3 teleportPosition = kvp.Value; // Capture the position in a local variable
            kvp.Key.onClick.AddListener(() => TeleportPlayer(teleportPosition));
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

    private void ResetSanityDrain()
    {
        if (sanityHandler != null)
        {
            sanityHandler.decreasePercentage = sanityHandler.decreasePercentage == 0 ? 0.01f : 0;
        }
    }

    private void TeleportPlayer(Vector3 position)
    {
        if (playerTransform != null)
        {
            playerTransform.position = position;
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
