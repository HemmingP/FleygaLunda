using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Walking,
    Fleyging
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private PlayerState currentState = PlayerState.Walking;
    [SerializeField] private Rigidbody2D playerRb2d;
    private InputSystem_Actions inputActions;
    [SerializeField] private Collider2D groundDetector;
    private float walkInput;
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 25f;
    [SerializeField] private float airAccelerationMultiplier = 0.25f;
    bool IsGrounded => groundDetector != null && groundDetector.IsTouchingLayers();
    [SerializeField] private HingeJoint2D fleygingJoint;
    [SerializeField] private GameObject walkingObject;
    [SerializeField] private GameObject fleygingObject;
    [SerializeField] private float fleyingMaxSpeed = 720f; // in degrees per second
    [SerializeField] private float fleyingAcceleration = 250f; // in degrees per second squared
    [SerializeField] private float fleyingDeceleration = 180f;
    [SerializeField] private LayerMask allExceptPlayer;

    private float currentFleyingMotorSpeed = 0f;

    private void Awake()
    {
        if (playerRb2d == null)
            playerRb2d = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.Walking)
        {
            bool isGrounded = IsGrounded;
            float currentAcceleration = isGrounded ? acceleration : acceleration * airAccelerationMultiplier;

            float targetSpeed = walkInput * walkSpeed;
            bool isAccelerating = Mathf.Abs(walkInput) > 0.01f && Mathf.Sign(walkInput) == Mathf.Sign(targetSpeed - playerRb2d.linearVelocity.x);

            float accelRate;
            if (isAccelerating)
            {
                // Square root curve: faster burst from start, easing into max speed
                float speedRatio = Mathf.Clamp01(Mathf.Abs(playerRb2d.linearVelocity.x) / walkSpeed);
                float curveMultiplier = Mathf.Sqrt(1f - speedRatio);
                // Keep a small minimum multiplier so the player still reaches top speed smoothly
                accelRate = currentAcceleration * Mathf.Max(curveMultiplier, 0.1f);
            }
            else
            {
                accelRate = deceleration;
            }

            float newVelocityX = Mathf.MoveTowards(playerRb2d.linearVelocity.x, targetSpeed, accelRate * Time.fixedDeltaTime);
            playerRb2d.linearVelocity = new Vector2(newVelocityX, playerRb2d.linearVelocity.y);
        }
        else if (currentState == PlayerState.Fleyging)
        {
            if (fleygingJoint != null)
            {
                if (Mathf.Abs(walkInput) >= 0.1f)
                {
                    fleygingJoint.useMotor = true;
                    float targetMotorSpeed = walkInput * fleyingMaxSpeed;

                    // Snappy, explosive heave: very rapid initial surge tapering towards max angular velocity
                    float currentRatio = Mathf.Clamp01(Mathf.Abs(currentFleyingMotorSpeed) / fleyingMaxSpeed);
                    float heaveCurve = Mathf.Pow(1f - currentRatio, 0.35f);
                    float motorAccelRate = fleyingAcceleration * Mathf.Max(heaveCurve, 0.2f);

                    currentFleyingMotorSpeed = Mathf.MoveTowards(currentFleyingMotorSpeed, targetMotorSpeed, motorAccelRate * Time.fixedDeltaTime);
                    fleygingJoint.motor = new JointMotor2D
                    {
                        motorSpeed = currentFleyingMotorSpeed,
                        maxMotorTorque = fleygingJoint.motor.maxMotorTorque
                    };
                }
                else
                {
                    fleygingJoint.useMotor = false;
                    currentFleyingMotorSpeed = 0f;
                }
            }
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        walkInput = context.ReadValue<Vector2>().x;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (IsGrounded && playerRb2d.linearVelocity.magnitude < 0.01f)
        {
            switch (currentState)
            {
                case PlayerState.Walking:
                    currentState = PlayerState.Fleyging;
                    walkingObject.SetActive(false);
                    fleygingObject.SetActive(true);
                    // Make playerRb2d dynamic again
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    playerRb2d.linearVelocity = Vector2.zero;
                    GameObject fleyingStong = fleygingJoint.gameObject;
                    float baseZRotation = fleyingStong.transform.eulerAngles.z;
                    Vector2 origin = fleyingStong.transform.position;
                    float poleLength = 4f;

                    for (int offset = 0; offset <= 180; offset++)
                    {
                        // Check positive offset (+offset degrees)
                        float anglePos = baseZRotation + offset;
                        float radPos = anglePos * Mathf.Deg2Rad;
                        Vector2 directionPos = new Vector2(Mathf.Cos(radPos), Mathf.Sin(radPos));
                        Vector2 targetPos = origin + directionPos * poleLength;

                        if (!Physics2D.Linecast(origin, targetPos, allExceptPlayer))
                        {
                            Vector3 angles = fleyingStong.transform.eulerAngles;
                            fleyingStong.transform.eulerAngles = new Vector3(angles.x, angles.y, anglePos);
                            break;
                        }

                        if (offset > 0)
                        {
                            // Check negative offset (-offset degrees)
                            float angleNeg = baseZRotation - offset;
                            float radNeg = angleNeg * Mathf.Deg2Rad;
                            Vector2 directionNeg = new Vector2(Mathf.Cos(radNeg), Mathf.Sin(radNeg));
                            Vector2 targetNeg = origin + directionNeg * poleLength;

                            if (!Physics2D.Linecast(origin, targetNeg, allExceptPlayer))
                            {
                                Vector3 angles = fleyingStong.transform.eulerAngles;
                                fleyingStong.transform.eulerAngles = new Vector3(angles.x, angles.y, angleNeg);
                                break;
                            }
                        }
                    }
                    break;
                case PlayerState.Fleyging:
                    currentState = PlayerState.Walking;
                    walkingObject.SetActive(true);
                    fleygingObject.SetActive(false);
                    // Make playerRb2d static
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    break;
                default:
                    // Handle any other states if necessary
                    break;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // if touches anything
            if (IsGrounded && currentState == PlayerState.Walking)
            {
                playerRb2d.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            }
        }
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }
}
