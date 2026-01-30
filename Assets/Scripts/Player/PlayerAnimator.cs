using System;
using DNExtensions;
using DNExtensions.Utilities;
using PrimeTween;
using UnityEngine;

[RequireComponent(typeof(PlayerControllerInput))]
public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator Instance;
    
    private static readonly int StartWalking = Animator.StringToHash("StartWalking");
    private static readonly int StopWalking = Animator.StringToHash("StopWalking");
    private static readonly int Pickup = Animator.StringToHash("Pickup");

    [Header("Change Direction Animation")] 
    [SerializeField] private float verticalDirectionRotation = 30f;
    [SerializeField] private float directionDuration = 0.15f;
    [SerializeField] private Ease directionEase = Ease.InOutCubic;

    [Header("References")] 
    [SerializeField] private Transform modelTransform;
    [SerializeField] private Animator animator;
    [SerializeField, ReadOnly] private bool facingLeft;
    [SerializeField, ReadOnly] private bool facingUp;
    [SerializeField, ReadOnly] private bool facingDown;
    [SerializeField, ReadOnly] private bool isWalking;

    private PlayerControllerInput _input;
    private Sequence _rotationTween;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        _input = GetComponent<PlayerControllerInput>();
    }
    
    private void Update()
    {
        HandleMovementAnimation();
        HandleHorizontalViewDirection();
        HandleVerticalViewDirection();
    }
    
    private void HandleMovementAnimation()
    {
        bool hasInput = _input.MoveInput.sqrMagnitude > 0.01f;
    
        if (hasInput && !isWalking)
        {
            isWalking = true;
            animator.SetTrigger(StartWalking);
        }
        else if (!hasInput && isWalking)
        {
            isWalking = false;
            animator.SetTrigger(StopWalking);
        }
    }

    public void PlayPickUpAnimation()
    {
        animator.SetTrigger(Pickup);
    }
    

    private void HandleVerticalViewDirection()
    {
        float currentYInput = _input.MoveInput.y;
        bool shouldUpdate = false;

        if (currentYInput == 0f && (facingUp || facingDown))
        {
            facingUp = false;
            facingDown = false;
            shouldUpdate = true;
        } 
        else if (currentYInput > 0f && !facingUp)
        {
            facingUp = true;
            facingDown = false;
            shouldUpdate = true;
        }
        else if (currentYInput < 0f && !facingDown)
        {
            facingDown = true;
            facingUp = false;
            shouldUpdate = true;
        }

        if (shouldUpdate)
        {
            AnimateRotation(false);
        }
    }

    private void HandleHorizontalViewDirection()
    {
        float currentXInput = _input.MoveInput.x;

        if (currentXInput < 0 && !facingLeft)
        {
            facingLeft = true;
            AnimateRotation(true);
        }
        else if (currentXInput > 0 && facingLeft)
        {
            facingLeft = false;
            AnimateRotation(true);
        }
    } 

    private void AnimateRotation(bool slower)
    {
        if (_rotationTween.isAlive)
        {
            _rotationTween.Complete();
        }

        var targetRotation = GetTargetRotation();
        _rotationTween = Sequence.Create();

        _rotationTween.Group(slower
            ? Tween.LocalRotation(modelTransform, Quaternion.Euler(targetRotation), directionDuration, directionEase)
            : Tween.LocalRotation(modelTransform, Quaternion.Euler(targetRotation), directionDuration * 0.5f,
                directionEase));
    }

    private Vector3 GetTargetRotation()
    {
        float horizontalAngle = facingLeft ? 180f : 0f;
        float verticalAngle = facingUp ? -verticalDirectionRotation : (facingDown ? verticalDirectionRotation : 0f);
        float angleMultiplier = facingLeft ? -1f : 1f;
        
        return new Vector3(0f, horizontalAngle + (verticalAngle * angleMultiplier), 0f);
    }
}