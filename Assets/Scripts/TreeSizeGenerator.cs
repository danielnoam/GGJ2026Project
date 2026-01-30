using DNExtensions.Utilities.Button;
using DNExtensions.Utilities.RangedValues;
using UnityEngine;

public class TreeSizeGenerator : MonoBehaviour
{
    [SerializeField, MinMaxRange(0.5f,2f)] private RangedFloat heightRange = new RangedFloat(0.8f, 1.2f);

    [Button]
    private void RandomizeTreeSize()
    {

        // var trees = transform.Get
    }
}
