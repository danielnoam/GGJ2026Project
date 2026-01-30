using System;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;

// GameAction that sabotages the priest's incense.
// Called when player interacts with incense while holding poop item.
// Marks incense as unavailable so cult leader cannot use it as a weapon.
[Serializable]
[SerializableSelectorName("Sabotage Incense", "Ritual")]
public class SabotageIncenseAction : GameAction
{
    public override string ActionName => "Sabotage Incense (Poop)";
    
    public override void Execute()
    {
        if (RitualWeaponRegistry.Instance == null)
        {
            Debug.LogError("[SabotageIncenseAction] RitualWeaponRegistry not found!");
            return;
        }
        
        // Mark incense as sabotaged
        RitualWeaponRegistry.Instance.MarkIncenseSabotaged();
    }
}