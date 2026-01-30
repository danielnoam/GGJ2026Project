using System;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;

// GameAction that marks a specific weapon as stolen/unavailable in the ritual.
// Called when player picks up knife or axe items.
// Removes the item from inventory after marking it as stolen (items are evidence, not usable).
[Serializable]
[SerializableSelectorName("Mark Weapon Stolen", "Ritual")]
public class MarkWeaponStolenAction : GameAction
{
    public enum WeaponType { Knife, Axe }
    
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private SOItem itemToRemove;
    
    public override string ActionName => $"Mark {weaponType} as Stolen";
    
    public override void Execute()
    {
        if (RitualWeaponRegistry.Instance == null)
        {
            Debug.LogError("[MarkWeaponStolenAction] RitualWeaponRegistry not found!");
            return;
        }
        
        // Mark weapon as unavailable
        switch (weaponType)
        {
            case WeaponType.Knife:
                RitualWeaponRegistry.Instance.MarkKnifeStolen();
                break;
            case WeaponType.Axe:
                RitualWeaponRegistry.Instance.MarkAxeStolen();
                break;
        }
        
        // Remove item from inventory (player picked it up as evidence)
        if (itemToRemove != null && PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.TryRemoveItem(itemToRemove);
        }
    }
}