using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementAdvanced : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;  // Referentie naar PlayerStats
    public Slider energySlider;   // UI Slider voor Energy

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Speed")]
    public float sprintMultiplier;
    public float airMultiplier;
    public float crouchMultiplier;
    private float moveSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    private bool readyToJump = true;

    [Header("Crouching")]
    public float crouchYScale;
    private float startYScale;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private Rigidbody rb;

    private MovementState state;

    // Instellingen voor ground drag
    public float groundDrag = 6f; // Pas dit aan naar wens

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;

        readyToJump = true;

        // Init PlayerStats en UI slider
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats niet toegewezen!");
        }

        if (energySlider != null)
        {
            energySlider.maxValue = playerStats.maxEnergy;
            energySlider.value = playerStats.currentEnergy;
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // Pas ground drag alleen toe als de speler op de grond is
        if (grounded)
        {
            rb.drag = groundDrag; // Hier wordt de drag alleen toegepast als de speler op de grond staat
        }
        else
        {
            rb.drag = 0f; // In de lucht mag er geen drag zijn
        }

        // Update UI slider
        if (energySlider != null)
        {
            energySlider.value = playerStats.currentEnergy;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // de moveSpeed ophalen uit de PlayerStats
        moveSpeed = playerStats.currentSpeed;

        //aan de hand van de state de moveSpeed aanpassen
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = moveSpeed * crouchMultiplier;
        }
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = moveSpeed * sprintMultiplier;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = moveSpeed * 1;
        }
        else
        {
            state = MovementState.air;
            moveSpeed = moveSpeed * 1;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            Vector3 slopeDirection = GetSlopeMoveDirection();
            rb.AddForce(slopeDirection * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else if (grounded)
        {
            // Gebruik de moveSpeed zoals gedefinieerd in PlayerStats als de speler op de grond is
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            // Als de speler in de lucht is, vermenigvuldig de snelheid met de jumpSpeedMultiplier
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
