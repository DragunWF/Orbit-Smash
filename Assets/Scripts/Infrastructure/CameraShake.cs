using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float defaultDuration = 0.3f;
    public float defaultMagnitude = 1.5f;

    private Vector3 originalPos;
    private bool isShaking = false;

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    public void TriggerShake()
    {
        TriggerShake(defaultDuration, defaultMagnitude);
    }

    public void TriggerShake(float duration, float magnitude)
    {
        if (!isShaking)
        {
            StartCoroutine(Shake(duration, magnitude));
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = CalculateForce(magnitude);
            float y = CalculateForce(magnitude);

            // Add shake offset to original position, maintain Z
            transform.localPosition = new Vector3(
                originalPos.x + x,
                originalPos.y + y,
                originalPos.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return to original position
        transform.localPosition = originalPos;
        isShaking = false;
    }

    private float CalculateForce(float magnitude)
    {
        return Random.Range(-1f, 1f) * magnitude;
    }

    public void UpdateOriginalPosition()
    {
        if (!isShaking)
        {
            originalPos = transform.localPosition;
        }
    }
}