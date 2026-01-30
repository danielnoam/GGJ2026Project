using UnityEngine;
using UnityEngine.Events;

public class ItemHolder : MonoBehaviour, IInteractable
{
    [Tooltip("The item that will be picked up")]
    [SerializeField] private SOItem item;
    [SerializeField] private UnityEvent onInteract;


    private bool _itemTaken;

    public bool CanInteract()
    {
       return !_itemTaken;
    }

    public void Interact(InteractorData interactorData)
    {
        if (PlayerInventory.Instance && PlayerInventory.Instance.TryAddItem(item))
        {
            _itemTaken = true;
            onInteract?.Invoke();
        }
    }
}
