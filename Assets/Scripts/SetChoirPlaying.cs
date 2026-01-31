using UnityEngine;

public class SetChoirPlaying : MonoBehaviour
{
    [SerializeField] private Animator ChoirAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChoirAnimator.SetTrigger("PlayCello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
