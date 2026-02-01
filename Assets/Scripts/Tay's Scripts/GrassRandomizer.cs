using UnityEngine;

public class GrassRandomizer : MonoBehaviour
{
    [Header("Scale Randomization")]
    [SerializeField] private float minScaleOffset = -0.1f;
    [SerializeField] private float maxScaleOffset = 0.1f;

    private void Start()
    {
        Randomize();
    }

    // Main randomization method called on Start
    // Applies flip, scale, and destruction chances in sequence
    private void Randomize()
    {
        // 5% chance to destroy - checked first to avoid unnecessary work
        if (Random.value <= 0.1f)
        {
            Destroy(gameObject);
            return;
        }

        // 50% chance to flip the sprite horizontally
        // Uses the SpriteRenderer's flipX property
        if (Random.value <= 0.5f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        // 100% chance - adds a random offset to the current scale
        // Same random value applied to all axes for uniform scaling
        float scaleOffset = Random.Range(minScaleOffset, maxScaleOffset);
        transform.localScale += Vector3.one * scaleOffset;
    }
}