using System;
using UnityEngine;

// Controls the cult leader's behavior during the ritual.
// Plays the appropriate weapon fetch animation (20 seconds) based on available weapons.
// All animations are on the cult leader's Animator and end with the strike at 3:00.
public class CultLeaderController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Animator jessicasAnimator;
    
    [Header("Animation Trigger Names")]
    [SerializeField] private string knifeAnimationTrigger = "FetchKnife";
    [SerializeField] private string shovelAnimationTrigger = "FetchShovel";
    [SerializeField] private string choirWeaponAnimationTrigger = "FetchChoirWeapon";
    [SerializeField] private string incenseAnimationTrigger = "FetchIncense";
    [SerializeField] private string noWeaponAnimationTrigger = "NoWeapon";
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;
    
    private RitualController.WeaponType currentWeapon = RitualController.WeaponType.None;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Called by RitualController at 2:40 to play the correct 20-second weapon animation
    public void PlayWeaponAnimation(RitualController.WeaponType weapon)
    {
        currentWeapon = weapon;
        
        if (animator == null)
        {
            Debug.LogError("[CultLeaderController] Animator not assigned!");
            return;
        }
        
        // Get the correct animation trigger based on weapon type
        string triggerToPlay = weapon switch
        {
            RitualController.WeaponType.Knife => knifeAnimationTrigger,
            RitualController.WeaponType.Shovel => shovelAnimationTrigger,
            RitualController.WeaponType.Cello => choirWeaponAnimationTrigger,
            RitualController.WeaponType.Incense => incenseAnimationTrigger,
            _ => noWeaponAnimationTrigger
        };
        
        if (enableDebugLogs) Debug.Log($"[CultLeaderController] Playing animation: {triggerToPlay}");
        
        // Trigger the animation (this plays the 20-second animation from 2:40 to 3:00)
        animator.SetTrigger(triggerToPlay);
    }
    public void DecapitateJessica()
    {
        jessicasAnimator.SetTrigger("Death");
    }
    // Returns the currently selected weapon
    public RitualController.WeaponType GetCurrentWeapon()
    {
        return currentWeapon;
    }
}