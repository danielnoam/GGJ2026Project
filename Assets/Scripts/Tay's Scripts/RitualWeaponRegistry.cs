using DNExtensions.Utilities.Button;
using UnityEngine;

// Central registry that tracks the availability of all ritual weapons.
// Acts as a singleton to allow GameActions and RitualController to communicate.
// Weapons are marked as unavailable when player steals/sabotages them.
public class RitualWeaponRegistry : MonoBehaviour
{
    public static RitualWeaponRegistry Instance { get; private set; }
    
    [Header("Weapon Availability")]
    [SerializeField] private bool knifeAvailable = true;
    [SerializeField] private bool axeAvailable = true;
    [Header("Choir Status")]
    [SerializeField] private bool choirAlive = true;
    
    [SerializeField] private bool incenseAvailable = true;
    
    [SerializeField] private ChoirGroupController choirGroup;
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;
    
    public bool KnifeAvailable => knifeAvailable;
    public bool AxeAvailable => axeAvailable;
    public bool IncenseAvailable => incenseAvailable;
    public bool ChoirAlive => choirAlive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    // Called by MarkWeaponStolenAction when knife is picked up
    public void MarkKnifeStolen()
    {
        knifeAvailable = false;
        if (enableDebugLogs) Debug.Log("[RitualWeaponRegistry] Knife marked as stolen");
    }

    // Called by MarkWeaponStolenAction when axe is picked up
    public void MarkAxeStolen()
    {
        axeAvailable = false;
        if (enableDebugLogs) Debug.Log("[RitualWeaponRegistry] Axe marked as stolen");
    }

    // Called by SabotageIncenseAction when incense is sabotaged
    public void MarkIncenseSabotaged()
    {
        incenseAvailable = false;
        if (enableDebugLogs) Debug.Log("[RitualWeaponRegistry] Incense marked as sabotaged");
    }

    // Called by PoisonChoirAction when drinks are poisoned
    public void MarkChoirPoisoned()
    {
        choirAlive = false;
        if (enableDebugLogs) Debug.Log("[RitualWeaponRegistry] Choir marked as poisoned");
    }
    
    // Reset for testing or restarting ritual
    public void ResetWeapons()
    {
        knifeAvailable = true;
        axeAvailable = true;
        incenseAvailable = true;
        choirAlive = true;
        
        if (enableDebugLogs) Debug.Log("[RitualWeaponRegistry] All weapons reset");
    }

    [Button]
    public void KillChoir()
    {
        // Notify choir group for death animation sequence
        choirGroup.PoisonChoir();
        MarkChoirPoisoned();
        choirGroup.OnChoirDrinks();
        choirAlive = false;
    }
}