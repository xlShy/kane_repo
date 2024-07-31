using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Floor Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] floorAudioClips;
    [SerializeField] private float timeBetweenClips = 0.5f;
    private bool isPlayingFloorAudio = false;
    private Coroutine currentAudioSequence;

    [Header("Dirt Audio")]
    [SerializeField] private AudioSource dirtAudioSource;
    [SerializeField] private AudioClip[] dirtAudioClips;
    [SerializeField] private float timeBetweenDirtClips = 0.5f;
    private bool isPlayingDirtAudio = false;
    private Coroutine currentDirtAudioSequence;

    public CharacterController characterController;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;
    [SerializeField] private SanityStatusEffect sanityScript;
    public bool isCanvasEnabled;

    private void OnEnable()
    {
        CanvasManager.OnCanvasEnabled += isAnyCanvasOn;
    }

    private void OnDisable()
    {
        CanvasManager.OnCanvasEnabled -= isAnyCanvasOn;
    }

    private void Start()
    {
        //sanityScript.playerFainted.AddListener(PlayerControl);
    }

    void Update()
    {
        if (!isCanvasEnabled)
        {
            PlayerControl();
            CheckFloorContact();
        }
    }

    private void CheckFloorContact()
    {
        // Ignore the player's own collider
        int layerMask = ~(1 << gameObject.layer);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f, layerMask))
        {

            if (hit.collider is TerrainCollider)
            {
                TryPlayDirtAudio();
                if (isPlayingFloorAudio)
                {
                    StopCoroutine(currentAudioSequence);
                    audioSource.Stop();
                    isPlayingFloorAudio = false;
                }
            }
            else if (hit.collider.CompareTag("FirstFloor"))
            {
                TryPlayFloorAudio();
                if (isPlayingDirtAudio)
                {
                    StopCoroutine(currentDirtAudioSequence);
                    dirtAudioSource.Stop();
                    isPlayingDirtAudio = false;
                }
            }
        }
        else
        {
            Debug.Log("No hit detected");
        }
    }

    private void PlayerControl()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float xDirection = Input.GetAxisRaw("Horizontal");
        float zDirection = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.right * xDirection + transform.forward * zDirection;
        // Check if the player is moving
        isMoving = direction.magnitude > 0.1f;
        characterController.Move(direction * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void isAnyCanvasOn(bool isOn)
    {
        isCanvasEnabled = isOn;
        if (isOn)
        {
            StopAllAudio();
        }
    }
    private void TryPlayFloorAudio()
    {
        if (isMoving && !isPlayingFloorAudio)
        {
            if (currentAudioSequence != null)
            {
                StopCoroutine(currentAudioSequence);
            }
            currentAudioSequence = StartCoroutine(PlayFloorAudioSequence());
        }
        else if (!isMoving && isPlayingFloorAudio)
        {
            StopCoroutine(currentAudioSequence);
            audioSource.Stop();
            isPlayingFloorAudio = false;
        }
    }

    private IEnumerator PlayFloorAudioSequence()
    {
        isPlayingFloorAudio = true;
        while (isMoving)
        {
            foreach (AudioClip clip in floorAudioClips)
            {
                audioSource.clip = clip;
                audioSource.Play();
                float elapsedTime = 0f;
                while (elapsedTime < clip.length + timeBetweenClips && isMoving)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                if (!isMoving) break;
            }
        }
        isPlayingFloorAudio = false;
    }

    private void TryPlayDirtAudio()
    {
        if (isMoving && !isPlayingDirtAudio)
        {
            if (currentDirtAudioSequence != null)
            {
                StopCoroutine(currentDirtAudioSequence);
            }
            currentDirtAudioSequence = StartCoroutine(PlayDirtAudioSequence());
        }
        else if (!isMoving && isPlayingDirtAudio)
        {
            StopCoroutine(currentDirtAudioSequence);
            dirtAudioSource.Stop();
            isPlayingDirtAudio = false;
        }
    }

    private IEnumerator PlayDirtAudioSequence()
    {
        isPlayingDirtAudio = true;
        while (isMoving)
        {
            foreach (AudioClip clip in dirtAudioClips)
            {
                dirtAudioSource.clip = clip;
                dirtAudioSource.Play();
                float elapsedTime = 0f;
                while (elapsedTime < clip.length + timeBetweenDirtClips && isMoving)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                if (!isMoving) break;
            }
        }
        isPlayingDirtAudio = false;
    }
    private void StopAllAudio()
    {
        // Stop floor audio
        if (isPlayingFloorAudio)
        {
            StopCoroutine(currentAudioSequence);
            audioSource.Stop();
            isPlayingFloorAudio = false;
        }

        // Stop dirt audio
        if (isPlayingDirtAudio)
        {
            StopCoroutine(currentDirtAudioSequence);
            dirtAudioSource.Stop();
            isPlayingDirtAudio = false;
        }
    }
}