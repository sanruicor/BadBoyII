using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private enum PlayerState { Idle, Walk, Shot, Shield, Die }
    private PlayerState playerState;
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Animator animator;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject rifle;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    
    private Quaternion defaultFacingRotation;
    private Quaternion currentMovementRotation;

    private bool gameOver = false;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference shotAction;
    public InputActionReference shieldAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
        shotAction.action.Enable();
        shieldAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        shotAction.action.Disable();
        shieldAction.action.Disable();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rifle.transform.rotation = Quaternion.LookRotation(leftHand.position - rightHand.position) * Quaternion.Euler(0, 180, 0);

        defaultFacingRotation = transform.rotation;
        currentMovementRotation = defaultFacingRotation;
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;

            float targetAngle = 0f;
            if (input.x > 0.1f) targetAngle = 90f;
            else if (input.x < -0.1f) targetAngle = -90f;

            SetMovementDirection(targetAngle);

            ChangeState(PlayerState.Walk);
        }
        else if (shotAction.action.IsPressed())
        {
            ChangeState(PlayerState.Shot);
            rifle.GetComponent<RifleController>().Shot();
        }
        else if (shieldAction.action.IsPressed())
        {
            ChangeState(PlayerState.Shield);
        }
        else
        {
            SetMovementDirection(0f);
            ChangeState(PlayerState.Idle);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;    

        // Move
        Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyObus") || other.CompareTag("EnemyObject"))
        {
            ChangeState(PlayerState.Die);
            gameOver = true;
        }
    }

    public void SetMovementDirection(float angleY)
    {
        currentMovementRotation = defaultFacingRotation * Quaternion.Euler(0, angleY, 0);
        
        // Aplicamos el cambio de inmediato (asume que está en Walk/Idle)
        transform.rotation = currentMovementRotation;
    }

    public void SetCombatRotation(bool isInCombat)
    {
        if (isInCombat)
        {
            transform.rotation = currentMovementRotation * Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = currentMovementRotation;
        }
    }

    private void ChangeState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                animator.SetBool("Walk", false);
                animator.SetBool("Shot", false);
                animator.SetBool("Shield", false);
                shield.SetActive(false);
                break;
            case PlayerState.Walk:
                animator.SetBool("Walk", true);
                shield.SetActive(false);
                break;
            case PlayerState.Shot:
                animator.SetBool("Shot", true);
                shield.SetActive(false);
                SetCombatRotation(true);
                break;
            case PlayerState.Shield:
                animator.SetBool("Shield", true);
                shield.SetActive(true);
                SetCombatRotation(true);
                break;
            case PlayerState.Die:
                animator.SetBool("Die", true);
                shield.SetActive(false);
                break;
        }
    }
}
