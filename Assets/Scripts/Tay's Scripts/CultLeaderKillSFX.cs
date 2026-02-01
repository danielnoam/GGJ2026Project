using UnityEngine;

public class CultLeaderKillSFX : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip drawSFX;
    [SerializeField] private AudioClip hitSFX;

    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;

    // Called from animation event when weapon is drawn
    // Plays the draw sound effect through the assigned AudioSource
    public void PlayDrawSFX()
    {
        if (enableDebugLogs)
            Debug.Log("[CultLeaderKillSFX] Draw SFX triggered");

        if (drawSFX != null && audioSource != null)
            audioSource.PlayOneShot(drawSFX);
    }

    // Called from animation event when weapon hits
    // Plays the hit sound effect through the assigned AudioSource
    public void PlayHitSFX()
    {
        if (enableDebugLogs)
            Debug.Log("[CultLeaderKillSFX] Hit SFX triggered");

        if (hitSFX != null && audioSource != null)
            audioSource.PlayOneShot(hitSFX);
    }
}




