using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private Interactable interactableObject;
    private bool isInteracts;
    private IUsable usable;
    private IUpdatable updatable;
    public bool IsInteracts => isInteracts;
    public bool CheckInteractableObject(out GameObject interactableObject)
    {
        interactableObject = Physics.OverlapSphere(transform.position, 0.5f, interactableLayerMask, QueryTriggerInteraction.Collide)[0].gameObject;
        if (interactableObject)
        {
            return true;
        }
        return false;
    }
    public bool ToInteract(Interactable interactable)
    {
        isInteracts = interactableObject.StartInteract(gameObject);
        if (isInteracts)
        {
            usable = interactableObject.GetComponent<IUsable>();
            updatable = interactableObject.GetComponent<IUpdatable>();
        }
        return isInteracts;
    }
    public bool UpdateInteractWithObject(float value)
    {
        if (updatable != null)
        {
            updatable.UpdateInteract(value);
            return true;
        }
        return false;
    }
    public bool UseObject()
    {
        if (usable != null)
        {
            usable.Use();
            return true;
        }
        return false;
    }
    public void StopInteract()
    {
        if (interactableObject != null)
        {
            interactableObject.EndInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == interactableLayerMask)
        {
            //gameObject.GetComponents<Outline>().SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == interactableLayerMask)
        {
            //gameObject.GetComponents<Outline>().SetActive(false);
        }
    }
}
