using System;
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
    [SerializeField] private bool shovelAvailable = true;
    [SerializeField] private bool incenseAvailable = true;
    
    [Header("Choir Status")]
    [SerializeField] private bool choirAlive = true;
    [SerializeField] private ChoirGroupController choirGroup;
    
    public bool KnifeAvailable => knifeAvailable;
    public bool ShovelAvailable => shovelAvailable;
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

    private void OnEnable()
    {
        GameEvents.OnWeaponPrevented += OnWeaponPrevented;
    }

    private void OnDisable()
    {
        GameEvents.OnWeaponPrevented -= OnWeaponPrevented;
    }

    private void OnWeaponPrevented(RitualWeapon ritualWeapon)
    {
        switch (ritualWeapon)
        {
            case RitualWeapon.Knife:
                knifeAvailable = false;
                break;
            case RitualWeapon.Shovel:
                shovelAvailable = false;
                break;
            case RitualWeapon.Cello:
                choirGroup?.PoisonChoir();
                break;
            case RitualWeapon.IncenseBurner:
                incenseAvailable = false;
                break;
        }
    }
    
}