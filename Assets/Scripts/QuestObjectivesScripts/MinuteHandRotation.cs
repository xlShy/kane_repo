using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MinutehandRotation : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    private float startAngle;
    private float currentRotationAngle;
    private RectTransform rectTransform;
    private Vector2 centerPoint;

    public UnityEvent CheckHands;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentRotationAngle = rectTransform.localEulerAngles.z;
        centerPoint = rectTransform.position; // Center point for angle calculations
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startAngle = GetAngle(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float currentAngle = GetAngle(eventData.position);
        float deltaAngle = currentAngle - startAngle;

        // Ensure deltaAngle is smooth and correct the direction
        if (deltaAngle > 180)
        {
            deltaAngle -= 360;
        }
        else if (deltaAngle < -180)
        {
            deltaAngle += 360;
        }

        currentRotationAngle += deltaAngle;
        rectTransform.localEulerAngles = new Vector3(0f, 0f, currentRotationAngle);
        startAngle = currentAngle;

        // Check if the rotation is within the target range
        CheckHands.Invoke();
    }

    private float GetAngle(Vector2 position)
    {
        Vector2 direction = position - centerPoint;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public float GetCurrentAngle()
    {
        return rectTransform.localEulerAngles.z;
    }
}
