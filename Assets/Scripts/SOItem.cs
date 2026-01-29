using DNExtensions.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
public class SOItem : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private new string name;
    [SerializeField, Preview] private Sprite icon;
    
    
    
    public string Name => name;
    public Sprite Icon => icon;
    
}