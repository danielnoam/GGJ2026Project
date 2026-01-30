using UnityEngine;
using UnityEngine.Playables;

// Master orchestrator for the ritual ceremony.
// Manages Timeline playback, responds to signals at key moments (2:00, 2:40, 3:00).
// Determines weapon availability via RitualWeaponRegistry and triggers correct animation on cult leader at 2:40.
// Triggers win/lose animations between 3:00-3:10 based on ritual outcome.
public class RitualController : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] private PlayableDirector ritualTimeline;
    
    [Header("Cult Leader")]
    [SerializeField] private CultLeaderController cultLeader;
    
    [Header("Win/Lose Animations")]
    [SerializeField] private Animator outcomeAnimator;
    [SerializeField] private string winAnimationTrigger = "PlayerWins";
    [SerializeField] private string loseAnimationTrigger = "PlayerLoses";
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;
    
    private WeaponType selectedWeapon = WeaponType.None;

    private void Start()
    {
        // Start the ritual timeline - plays through entire song
        if (ritualTimeline != null)
        {
            ritualTimeline.Play();
        }
    }

    // Called by Timeline Signal at 2:40 - selects weapon and triggers animation on cult leader
    public void OnSelectWeapon()
    {
        if (enableDebugLogs) Debug.Log("[RitualController] Selecting weapon");
        
        // Determine highest priority available weapon from registry
        selectedWeapon = DetermineAvailableWeapon();
        
        if (enableDebugLogs) Debug.Log($"[RitualController] Selected weapon: {selectedWeapon}");
        
        // Tell cult leader to play the correct 20-second animation
        if (cultLeader != null)
        {
            cultLeader.PlayWeaponAnimation(selectedWeapon);
        }
    }

    // Checks weapons in priority order via registry: Knife > Axe > Choir Weapon > Incense
    private WeaponType DetermineAvailableWeapon()
    {
        if (RitualWeaponRegistry.Instance == null)
        {
            Debug.LogError("[RitualController] RitualWeaponRegistry not found!");
            return WeaponType.None;
        }
        
        // Highest priority: Knife
        if (RitualWeaponRegistry.Instance.KnifeAvailable)
        {
            if (enableDebugLogs) Debug.Log("[RitualController] Knife is available");
            return WeaponType.Knife;
        }
        
        // Second priority: Axe
        if (RitualWeaponRegistry.Instance.ShovelAvailable)
        {
            if (enableDebugLogs) Debug.Log("[RitualController] Shovel is available");
            return WeaponType.Shovel;
        }
        
        // Third priority: Choir Weapon (only if choir is alive)
        if (RitualWeaponRegistry.Instance.ChoirAlive)
        {
            if (enableDebugLogs) Debug.Log("[RitualController] Choir weapon is available");
            return WeaponType.Cello;
        }
           

        // Fourth priority: Incense
        if (RitualWeaponRegistry.Instance.IncenseAvailable)
        {
            if (enableDebugLogs) Debug.Log("[RitualController] Incense is available");
            return WeaponType.Incense;
        }
        
        // No weapons available - player wins!
        if (enableDebugLogs) Debug.Log("[RitualController] No weapons available");
        return WeaponType.None;
    }

    // Called by Timeline Signal at 3:00 - determines outcome and plays appropriate animation
    public void OnStrikeMoment()
    {
        if (enableDebugLogs) Debug.Log("[RitualController] Strike moment at 3:00");
        
        // Player wins if no weapons available
        bool playerWins = (selectedWeapon == WeaponType.None);
        
        if (outcomeAnimator != null)
        {
            if (playerWins)
            {
                if (enableDebugLogs) Debug.Log("[RitualController] Player wins - no weapons available!");
                outcomeAnimator.SetTrigger(winAnimationTrigger);
            }
            else
            {
                if (enableDebugLogs) Debug.Log("[RitualController] Player loses - leader has weapon!");
                outcomeAnimator.SetTrigger(loseAnimationTrigger);
            }
        }
    }

    public enum WeaponType { None, Knife, Shovel, Cello, Incense }
}