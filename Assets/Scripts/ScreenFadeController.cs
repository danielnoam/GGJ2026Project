using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
using UnityEngine.SceneManagement;


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
    [SerializeField] private bool isStartAlphaZero = true;
    [SerializeField] private bool startWithFade = false;
    
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = false;

    private void Start()
    {
        // Ensure we start with a clear screen
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
        }
        fadeCanvasGroup.alpha = isStartAlphaZero ? 0f : 1f;
        if (startWithFade)
        {
            FadeInDisable(fadeInDuration);
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
    public void FadeInDisable(float duration)
    {
        StartCoroutine(FadeInDelay(duration));
    }

    IEnumerator FadeInDelay(float duration)
    {
        yield return new WaitForSeconds(4);
        if (fadeCanvasGroup != null)
        {
            Tween.Alpha(fadeCanvasGroup, 0f, duration, ease: fadeEase);
            yield return new WaitForSeconds(duration);
            fadeCanvasGroup.blocksRaycasts = false;
        }
    }
    public void TransferToMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
