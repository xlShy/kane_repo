using UnityEngine;

public class ToiletDrain : InteractableObject
{
    [SerializeField] private ChemicalMixingPlace mixingBucket;
    [SerializeField] private AudioSource drainSound;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (mixingBucket != null)
        {
            mixingBucket.DrainMixture();
            if (drainSound != null)
            {
                drainSound.Play();
            }
            Debug.Log("Mixture drained into the toilet.");
        }
        else
        {
            Debug.LogError("Mixing bucket reference not set in ToiletDrain script.");
        }
    }
}