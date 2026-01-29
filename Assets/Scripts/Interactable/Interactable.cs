using System;
using DNExtensions.Utilities;
using DNExtensions.Utilities.Button;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;

[Serializable]
public class InteractPossibility
{
    public SOItem neededItem;
    [SerializeReference, SerializableSelector] 
    public GameAction[] actionsOnInteract = Array.Empty<GameAction>();
}


[SelectionBase]
[DisallowMultipleComponent]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool limitInteractionsToOnce;
    [SerializeField] private InteractPossibility[] interactPossibilities = Array.Empty<InteractPossibility>();
    
    [SerializeField, ReadOnly] private bool hasInteracted;
    [SerializeField, ReadOnly] private string interactableID = "";

    public string InteractableID => interactableID;


    
    public void Interact(InteractorData interactorData)
    {
        if (limitInteractionsToOnce && hasInteracted) return;
        
        hasInteracted = true;
        
        GameEvents.InteractedWith(this);
        OnInteract(interactorData);

        foreach (InteractPossibility interactPossibility in interactPossibilities)
        {
            bool isItemNeeded = interactPossibility.neededItem;
            bool hasItem = interactorData.equippedItem;
            
            if (!isItemNeeded || hasItem && interactorData.equippedItem == interactPossibility.neededItem)
            {
                foreach (var action in interactPossibility.actionsOnInteract)
                {
                    action?.Execute();
                }
            }
        }
    }
    
    public virtual bool CanInteract()
    {
        return canInteract;
    }
    
    protected abstract void OnInteract(InteractorData interactorData);
    
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(interactableID))
        {
            interactableID = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
    
    [Button(ButtonPlayMode.OnlyWhenNotPlaying)]
    private void RegenerateID()
    {
        interactableID = Guid.NewGuid().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}