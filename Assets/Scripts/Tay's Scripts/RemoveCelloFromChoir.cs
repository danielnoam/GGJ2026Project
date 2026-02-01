using UnityEngine;

public class RemoveCelloFromChoir : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void RemoveCello()
    {
        animator.SetTrigger("Idle");
    }
}
