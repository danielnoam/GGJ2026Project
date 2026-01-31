using System;
using System.Collections.Generic;

public static partial class GameEvents
{
    public static event Action OnTimelineStarted;
    
    public static event Action<RitualWeapon> OnWeaponPrevented;
    
    public static event Action<PlayerInventory> OnInventoryChanged;
    public static event Action<SOItem> OnItemEquipped;
    
    public static void TimelineStarted()
    {
        OnTimelineStarted?.Invoke();
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