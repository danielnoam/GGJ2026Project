using System;
using System.Collections.Generic;

public static partial class GameEvents
{
    public static event Action OnRitualStarted;
    public static event Action<RitualWeapon, HashSet<RitualWeapon>> OnRitualCompleted;
    public static event Action OnRitualStopped;
    
    public static event Action<RitualWeapon> OnWeaponPrevented;
    
    public static event Action<PlayerInventory> OnInventoryChanged;
    public static event Action<SOItem> OnItemEquipped;
    
    public static void RitualStarted()
    {
        OnRitualStarted?.Invoke();
    }
    
    public static void RitualCompleted(RitualWeapon weaponUsed, HashSet<RitualWeapon> preventedWeapons)
    {
        OnRitualCompleted?.Invoke(weaponUsed, preventedWeapons);
    }
    
    public static void RitualStopped()
    {
        OnRitualStopped?.Invoke();
    }
    
    public static void WeaponPrevented(RitualWeapon weapon)
    {
        OnWeaponPrevented?.Invoke(weapon);
    }
    
    public static void InventoryChanged(PlayerInventory inventory)
    {
        OnInventoryChanged?.Invoke(inventory);
    }
    
    public static void ItemEquipped(SOItem item)
    {
        OnItemEquipped?.Invoke(item);
    }
}