using System;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
[SerializableSelectorName("Unity Event", "Event")]
public class UnityEventAction : GameAction
{
    [SerializeField] private UnityEvent onExecute;
    
    public override string ActionName => "Unity Event";
    
    public override void Execute()
    {
        onExecute?.Invoke();
    }
}