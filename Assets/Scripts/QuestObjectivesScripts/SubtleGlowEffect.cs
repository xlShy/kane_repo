using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SubtleGlow3D : MonoBehaviour
{
    public Color glowColor = new Color(1f, 1f, 1f, 0.5f);
    public float glowIntensity = 1f;
    private Renderer objectRenderer;
    private Material glowMaterial;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        glowMaterial = new Material(objectRenderer.sharedMaterial);
        glowMaterial.EnableKeyword("_EMISSION");
        UpdateGlow();
        objectRenderer.material = glowMaterial;
    }

    private void OnValidate()
    {
        UpdateGlow();
    }

    private void UpdateGlow()
    {
        if (glowMaterial != null)
        {
            glowMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
        }
    }

    private void OnDestroy()
    {
        if (glowMaterial != null)
        {
            Destroy(glowMaterial);
        }
    }
}