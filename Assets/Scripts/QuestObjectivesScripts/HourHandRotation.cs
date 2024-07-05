using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

public class HourhandRotation : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    private float currentRotationAngle;
    private RectTransform rectTransform;
    private Vector2 centerPoint;
    [SerializeField] private float snapAngle = 30f; // 360 / 12 = 30 degrees per hour
    public UnityEvent CheckHands;

    [SerializeField] private List<AudioSource> audioSources;
    private AudioSource currentAudioSource;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentRotationAngle = rectTransform.localEulerAngles.z;
        centerPoint = rectTransform.position;
        SnapToNearestHour(); // Ensure it starts snapped
    }

    public void OnDrag(PointerEventData eventData)
    {
        float newAngle = GetAngle(eventData.position);
        float snappedAngle = Mathf.Round(newAngle / snapAngle) * snapAngle;

        // Only update if we've moved to a new snap position
        if (snappedAngle != currentRotationAngle)
        {
            currentRotationAngle = snappedAngle;
            ApplyRotation();
            CheckHands.Invoke();
            PlayRandomSounds();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SnapToNearestHour();
        CheckHands.Invoke();
        PlayRandomSounds();
    }

    private void SnapToNearestHour()
    {
        float snappedAngle = Mathf.Round(currentRotationAngle / snapAngle) * snapAngle;
        currentRotationAngle = snappedAngle;
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        rectTransform.localEulerAngles = new Vector3(0f, 0f, currentRotationAngle);
    }

    private float GetAngle(Vector2 position)
    {
        Vector2 direction = position - centerPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return (angle - 90 + 360) % 360;
    }

    public float GetCurrentAngle()
    {
        return currentRotationAngle;
    }

    private void PlayRandomSounds()
    {
        if (audioSources.Count == 0) return;

        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }

        currentAudioSource = audioSources[Random.Range(0, audioSources.Count)];

        currentAudioSource.Play();
    }
}