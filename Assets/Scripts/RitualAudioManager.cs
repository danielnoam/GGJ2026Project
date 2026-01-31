using UnityEngine;
using System.Collections;

public class RitualAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] ritualLayers = new AudioSource[5];
    
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 2f;
    
    private int currentLayerIndex;
    private Coroutine fadeCoroutine;
    

    private void OnEnable()
    {
        GameEvents.OnWeaponPrevented += OnWeaponPrevented;
        GameEvents.OnTimelineStarted += OnTimelineStarted;
    }

    private void OnDisable()
    {
        GameEvents.OnWeaponPrevented -= OnWeaponPrevented;
        GameEvents.OnTimelineStarted -= OnTimelineStarted;
    }

    private void OnTimelineStarted()
    {
        if (ritualLayers.Length != 5)
        {
            return;
        }
        
        for (int i = 0; i < ritualLayers.Length; i++)
        {
            if (ritualLayers[i] != null)
            {
                ritualLayers[i].volume = (i == 0) ? 1f : 0f;
                ritualLayers[i].Play();
            }
        }
    }

    private void OnWeaponPrevented(RitualWeapon weapon)
    {
        if (currentLayerIndex < ritualLayers.Length - 1)
        {
            int nextLayerIndex = currentLayerIndex + 1;
            
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            
            fadeCoroutine = StartCoroutine(CrossfadeLayers(currentLayerIndex, nextLayerIndex));
            currentLayerIndex = nextLayerIndex;
        }
    }

    private IEnumerator CrossfadeLayers(int fromIndex, int toIndex)
    {
        AudioSource fromSource = ritualLayers[fromIndex];
        AudioSource toSource = ritualLayers[toIndex];
        
        if (!fromSource || !toSource)
        {
            Debug.LogError($"Missing audio source! From: {fromIndex}, To: {toIndex}");
            yield break;
        }
        
        float elapsed = 0f;
        float startVolumeFrom = fromSource.volume;
        float startVolumeTo = toSource.volume;
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            
            fromSource.volume = Mathf.Lerp(startVolumeFrom, 0f, t);
            toSource.volume = Mathf.Lerp(startVolumeTo, 1f, t);
            
            yield return null;
        }
        
        fromSource.volume = 0f;
        toSource.volume = 1f;
    }
}