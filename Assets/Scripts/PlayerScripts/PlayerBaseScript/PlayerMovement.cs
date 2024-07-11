using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InteractionHandler interactionHandler;

    public CharacterController characterController;
    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    private bool isGrounded;

    [SerializeField] private SanityStatusEffect sanityScript;

    private void Start()
    {
        //sanityScript.playerFainted.AddListener(PlayerControl);
    }
    void Update()
    {
        if (!interactionHandler.IsAnyCanvasActive())
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //To Do - Create InputHandler Script
        float xDirection = Input.GetAxisRaw("Horizontal");
        float zDirection = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * xDirection + transform.forward * zDirection;
        characterController.Move(direction * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
