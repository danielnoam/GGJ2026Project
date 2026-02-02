using DNExtensions.Utilities;
using DNExtensions.Utilities.SerializedInterface;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerControllerInput))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private bool canInteractWhileAirborne = true;
    [SerializeField] private float interactCheckRange = 2f;
    [SerializeField] private Vector3 interactCheckOffset = Vector3.zero;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField, ReadOnly] private InterfaceReference<IInteractable> closestInteractable;
    
    private PlayerControllerInput _input;
    private PlayerController _playerController;
    private PlayerInventory _playerInventory;
    private IInteractable _lastInteractable;
    
    private bool CanInteract => canInteractWhileAirborne || _playerController.IsGrounded;
    

    private void Awake()
    {
        _input = GetComponent<PlayerControllerInput>();
        _playerController = GetComponent<PlayerController>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        _input.OnInteractAction += OnInteractAction;
    }

    private void OnDisable()
    {
        _input.OnInteractAction -= OnInteractAction;
    }

    private void FixedUpdate()
    {
        CheckForInteractable();
    }

    private void OnInteractAction(InputAction.CallbackContext context)
    {
        if (CanInteract && context.performed && closestInteractable.Value != null)
        {
            var interactorData = new InteractorData()
            {
                interactor = gameObject,
                inventory = _playerInventory
            };
            closestInteractable.Value.Interact(interactorData);
        }
    }

    private void CheckForInteractable()
    {
        var colliders = Physics.OverlapSphere(transform.position + interactCheckOffset, interactCheckRange, interactableLayer);
        var closestDistance = float.MaxValue;
        IInteractable closest = null;

        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }
        }
        
        closestInteractable.Value = closest;
        
        if (CanInteract && closestInteractable.Value != null)
        {
            if (_lastInteractable != closestInteractable.Value)
            {
                if (_lastInteractable is InteractableBase oldBase)
                {
                    oldBase.HidePrompt();
                }
                
                if (closestInteractable.Value is InteractableBase newBase)
                {
                    newBase.ShowPrompt(_playerInventory);
                }
        
                _lastInteractable = closestInteractable.Value;
            }
        }
        else
        {
            if (_lastInteractable is InteractableBase lastBase)
            {
                lastBase.HidePrompt();
            }
            _lastInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + interactCheckOffset, interactCheckRange);
    }
}