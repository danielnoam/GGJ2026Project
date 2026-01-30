using UnityEngine;

// Individual choir member component.
// Handles death animation trigger when poisoned.
// Attach to each choir member GameObject.
public class ChoirMember : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float animationDuration;
    [SerializeField] private string deathAnimationTrigger = "Die";
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;

    // Triggers the death animation (falling down sprite animation)
    public void Die()
    {
        animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        if (enableDebugLogs) Debug.Log($"[ChoirMember] {gameObject.name} dying");
        
        if (animator != null)
        {
            animator.SetTrigger(deathAnimationTrigger);
        }

        Destroy(animator, animationDuration);
    }
}