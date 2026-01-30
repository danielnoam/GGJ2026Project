using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

// Controls screen fade in/out effects using PrimeTween.
// Called by Timeline signals to fade screen to black and back.
// Uses a CanvasGroup for smooth alpha transitions.
public class ScreenFadeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private Image fadeImage;
    
    [Header("Fade Settings")]
    [SerializeField] private float fadeOutDuration = 1f; // Time to fade to black
    [SerializeField] private float fadeInDuration = 1f;  // Time to fade back to clear
    [SerializeField] private Ease fadeEase = Ease.InOutSine;
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;

    private void Start()
    {
        // Ensure we start with a clear screen
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
        }
    }

    // Called by Timeline Signal - fades screen to black
    public void FadeOut()
    {
        if (enableDebugLogs) Debug.Log("[ScreenFadeController] Fading out to black");
        
        if (fadeCanvasGroup != null)
        {
            Tween.Alpha(fadeCanvasGroup, 1f, fadeOutDuration, ease: fadeEase);
        }
    }

    // Called by Timeline Signal - fades screen back to clear
    public void FadeIn()
    {
        if (enableDebugLogs) Debug.Log("[ScreenFadeController] Fading in to clear");
        
        if (fadeCanvasGroup != null)
        {
            Tween.Alpha(fadeCanvasGroup, 0f, fadeInDuration, ease: fadeEase);
        }
    }

    // Custom fade out with specific duration (can be called from code)
    public void FadeOut(float duration)
    {
        if (enableDebugLogs) Debug.Log($"[ScreenFadeController] Fading out to black over {duration} seconds");
        
        if (fadeCanvasGroup != null)
        {
            Tween.Alpha(fadeCanvasGroup, 1f, duration, ease: fadeEase);
        }
    }

    // Custom fade in with specific duration (can be called from code)
    public void FadeIn(float duration)
    {
        if (enableDebugLogs) Debug.Log($"[ScreenFadeController] Fading in to clear over {duration} seconds");
        
        if (fadeCanvasGroup != null)
        {
            Tween.Alpha(fadeCanvasGroup, 0f, duration, ease: fadeEase);
        }
    }

    // Immediately sets screen to black without animation
    public void SetBlack()
    {
        if (enableDebugLogs) Debug.Log("[ScreenFadeController] Screen set to black instantly");
        
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
        }
    }

    // Immediately sets screen to clear without animation
    public void SetClear()
    {
        if (enableDebugLogs) Debug.Log("[ScreenFadeController] Screen set to clear instantly");
        
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
        }
    }
}
