using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FurnitureToMoveScript : InteractableObject
{
    [SerializeField] private ReadableDocumentScript readableDocumentScript;
    [SerializeField] private AudioSource movingFurnitureAudio;


    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            Vector3 targetPosition = new Vector3(15f, 14.5f, 28f);
            StartCoroutine(MoveSmoothlyCo(targetPosition, 1f));
            gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        }

    }
    public void canBeMoved()
    {
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }
    private IEnumerator MoveSmoothlyCo(Vector3 targetPosition, float duration)
    {
        movingFurnitureAudio.Play();
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}
