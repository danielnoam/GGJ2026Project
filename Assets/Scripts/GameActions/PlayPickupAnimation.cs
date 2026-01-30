using System;
using DNExtensions.Utilities.SerializableSelector;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
[SerializableSelectorName("Play Pickup", "Player")]
public class PlayPickupAnimation : GameAction
{
    public override string ActionName => "Play Pickup Animation";

    public override void Execute()
    {
        if (PlayerAnimator.Instance)
        {
            PlayerAnimator.Instance.PlayPickUpAnimation();
        }
    }
}