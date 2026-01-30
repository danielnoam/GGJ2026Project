using System;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;

[Serializable]
[SerializableSelectorName("Call Ritual Event", "Ritual")]
public class CallRitualEventAction : GameAction
{
    [SerializeField] private RitualWeapon weaponToPrevent;
    
    public override string ActionName => $"Prevent {weaponToPrevent}";
    
    public override void Execute()
    {
        GameEvents.WeaponPrevented(weaponToPrevent);
    }
}