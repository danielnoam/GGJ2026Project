using UnityEngine;

// Handles conditional sword pickup logic.
// Checks if sword is available and triggers the correct animation on cult leader's Animator.
// Called by Timeline signal when cult leader arrives at sword location.
public class CultLeaderSwordSequence : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator cultLeaderAnimator;
    
    [Header("Animation Triggers")]
    [SerializeField] private string pickupAnimationTrigger = "PickupSword";
    [SerializeField] private string waitAnimationTrigger = "WaitNoSword";
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;

    // Called by Timeline Signal when cult leader arrives at sword location
    public void OnArrivedAtSwordLocation()
    {
        if (enableDebugLogs) Debug.Log("[CultLeaderSwordSequence] Arrived at sword location");
        
        if (cultLeaderAnimator == null)
        {
            Debug.LogError("[CultLeaderSwordSequence] Cult Leader Animator not assigned!");
            return;
        }
        
        // Check if sword is available
        bool swordAvailable = IsSwordAvailable();
        
        if (swordAvailable)
        {
            if (enableDebugLogs) Debug.Log("[CultLeaderSwordSequence] Sword available - triggering pickup animation");
            cultLeaderAnimator.SetTrigger(pickupAnimationTrigger);
        }
        else
        {
            if (enableDebugLogs) Debug.Log("[CultLeaderSwordSequence] Sword not available - triggering wait animation");
            cultLeaderAnimator.SetTrigger(waitAnimationTrigger);
        }
    }

    // Checks if sword (knife) is available via registry
    private bool IsSwordAvailable()
    {
        if (RitualWeaponRegistry.Instance == null)
        {
            Debug.LogWarning("[CultLeaderSwordSequence] RitualWeaponRegistry not found, assuming sword not available");
            return false;
        }
        
        return RitualWeaponRegistry.Instance.KnifeAvailable;
    }
}