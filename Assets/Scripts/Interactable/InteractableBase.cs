using System;
using DNExtensions.Utilities;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    [SerializeField] private bool limitInteractionsToOnce;
    [SerializeField] private AudioClip interactSfx;
    [SerializeField] private InteractionPrompt interactionPrompt;
    [SerializeField] private SOItem neededItem;
    [SerializeReference, SerializableSelector] 
    private GameAction[] actionsOnInteract = Array.Empty<GameAction>();
    [Separator]
    [SerializeField, ReadOnly] private bool hasInteracted;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact(InteractorData interactorData)
    {
        if (limitInteractionsToOnce && hasInteracted) return;
        
        bool isItemNeeded = neededItem;
        bool playerHasItem = interactorData.inventory;
            
        if (!isItemNeeded || playerHasItem && interactorData.inventory.HasItem(neededItem))
        {
            foreach (var action in actionsOnInteract)
            {
                action?.Execute();
            }
            
            hasInteracted = true;
                
            if (interactSfx) audioSource?.PlayOneShot(interactSfx);
        }
        
        OnInteract(interactorData);
    }
    
    public virtual bool CanInteract()
    {
        return !limitInteractionsToOnce || !hasInteracted;
    }

    public void ShowPrompt(PlayerInventory playerInventory)
    {
        if (!interactionPrompt || !CanInteract()) return;
        
        if (neededItem && !playerInventory.HasItem(neededItem))
        {
            return;
        }
    
        interactionPrompt.Show(neededItem);
    }

    public void HidePrompt()
    {
        if (interactionPrompt)
        {
            interactionPrompt.Hide();
        }
    }
    
    protected abstract void OnInteract(InteractorData interactorData);
}