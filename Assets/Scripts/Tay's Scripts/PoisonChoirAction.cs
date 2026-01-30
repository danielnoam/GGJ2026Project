using System;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;

// GameAction that poisons the choir's drinks.
// Called when player interacts with drinks while holding poison item.
// Notifies both the registry and the ChoirGroupController for death sequence.
[Serializable]
[SerializableSelectorName("Poison Choir", "Ritual")]
public class PoisonChoirAction : GameAction
{
    [SerializeField] private ChoirGroupController choirGroup;
    
    public override string ActionName => "Poison Choir Drinks";
    
    public override void Execute()
    {
        if (RitualWeaponRegistry.Instance == null)
        {
            Debug.LogError("[PoisonChoirAction] RitualWeaponRegistry not found!");
            return;
        }
        
        if (choirGroup == null)
        {
            Debug.LogError("[PoisonChoirAction] ChoirGroupController reference not set!");
            return;
        }
        
        // Mark choir as poisoned in registry
        RitualWeaponRegistry.Instance.MarkChoirPoisoned();
        
        // Notify choir group for death animation sequence
        choirGroup.PoisonChoir();
    }
}