
using DNExtensions.Utilities;
using PrimeTween;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerControllerInput))]
[RequireComponent(typeof(PlayerInventory))]
[SelectionBase]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float maxFallSpeed = 25f;
    
    [Header("Collision Settings")]
    [SerializeField] private float groundCheckRadius = 0.31f;
    [SerializeField] private Vector3 groundCheckOffset = Vector3.down;
    [SerializeField] private LayerMask collisionLayer;
    
    [Separator]
    [SerializeField, ReadOnly] private Vector3 velocity;
    [SerializeField, ReadOnly] private bool isGrounded;
    [SerializeField, ReadOnly] private bool allowControl = true;


    private CharacterController _controller;
    private PlayerControllerInput _input;
    
    public bool IsGrounded => isGrounded;
    public bool AllowControl => allowControl;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;
        
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<PlayerControllerInput>();
    }
    

    private void FixedUpdate()
    {
        CheckCollisions();
        HandleGravity();
        HandleMovement();   
    }

    private void HandleMovement()
    {
        if (!_controller || !_controller.enabled) return;
        
        velocity.x = _input.MoveInput.x * moveSpeed;
        velocity.z = _input.MoveInput.y * moveSpeed;
    
        Vector3 finalVelocity = velocity;
        _controller.Move(finalVelocity * Time.fixedDeltaTime);
    }

    private void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y -= gravity;
            if (velocity.y < -maxFallSpeed)
            {
                velocity.y = -maxFallSpeed;
            }
        }
    }
    
    public void EnableControl(bool state)
    {
        allowControl = state;
        _controller.enabled = state;
        
        if (!allowControl)
        {
            velocity = Vector3.zero;
            _input.ResetInput();
        }
    }

    private void CheckCollisions()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, collisionLayer, QueryTriggerInteraction.Ignore);
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
}