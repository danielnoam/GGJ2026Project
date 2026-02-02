using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Sprite defaultInteractIcon;
    
    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.15f;

    private bool _isVisible;
    private Tween _currentTween;

    private void Awake()
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        Hide();
    }

    public void Show(SOItem neededItem = null)
    {
        if (_isVisible) return;
        
        if (neededItem && neededItem.Icon)
        {
            itemBackground.gameObject.SetActive(true);
            itemIcon.sprite = neededItem.Icon;
        }
        else
        {
            if (!defaultInteractIcon)
            {
                itemBackground.gameObject.SetActive(false);
                itemIcon.sprite = null;
            }
            else
            {
                itemBackground.gameObject.SetActive(true);
                itemIcon.sprite = defaultInteractIcon;
            }

        }
        

        _currentTween = Tween.Alpha(canvasGroup, 1f, fadeDuration);
        _currentTween.OnComplete(() =>
        {
            _isVisible = true;
        });
    }

    public void Hide()
    {
        if (!_isVisible) return;
        
        _currentTween = Tween.Alpha(canvasGroup, 0f, fadeDuration);
        _currentTween.OnComplete(() =>
        {
            _isVisible = false;
        });
    }
}