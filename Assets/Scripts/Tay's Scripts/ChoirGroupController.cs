using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DNExtensions.Utilities;

// Manages the choir group and handles poison death sequence.
// Controls all choir members as one unit.
// Triggers death animations a few seconds after drinking poisoned drinks at 2:00.
public class ChoirGroupController : MonoBehaviour
{
    [Header("Choir Members")]
    [SerializeField] private List<ChoirMember> choirMembers = new List<ChoirMember>();

    [SerializeField] private GameObject ChoirMembersPrefab;

    [SerializeField] private RitualWeaponRegistry disableWeapon;
    
    [Header("Poison Settings")]
    [SerializeField] private float deathDelayAfterDrinking = 0.5f;
    [SerializeField] private float delayBetweenDeath = 0.25f;
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;
    
    [SerializeField,ReadOnly]  private bool isPoisoned = false;
    [SerializeField,ReadOnly]  private bool hasDrankPoison = false;

    // Called by PoisonChoirAction when player poisons the drinks
    public void PoisonChoir()
    {
        isPoisoned = true;
        
        if (enableDebugLogs) Debug.Log("[ChoirGroupController] Choir has been poisoned");
    }

    // Called by Timeline Signal at 2:00 when choir drinks
    public void OnChoirDrinks()
    {
        if (enableDebugLogs) Debug.Log("[ChoirGroupController] Choir drinking");
        
        if (isPoisoned)
        {
            hasDrankPoison = true;
            disableWeapon.SetCelloUnavailable();
            StartCoroutine(TriggerDeathSequence());
        }
    }

    public void SetChoirActive()
    {
        print(hasDrankPoison);
        if (hasDrankPoison)
        {
            ChoirMembersPrefab.SetActive(false);
        }
        else
        {
            ChoirMembersPrefab.SetActive(true);
        }
    }

    // Waits a few seconds, then triggers death animations for all choir members
    private IEnumerator TriggerDeathSequence()
    {
        if (enableDebugLogs) Debug.Log($"[ChoirGroupController] Death sequence starting in {deathDelayAfterDrinking} seconds");
        
        yield return new WaitForSeconds(deathDelayAfterDrinking);
        
        if (enableDebugLogs) Debug.Log("[ChoirGroupController] Choir members dying now");
        
        foreach (var member in choirMembers)
        {
          
            if (member != null)
            {
                yield return new WaitForSeconds(delayBetweenDeath);
                member.Die();
            }
        }
    }
    
}