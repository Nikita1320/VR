using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private Interactable interactableObject;
    [SerializeField] private InputActionProperty pinchInputAction;
    [SerializeField] private InputActionProperty gripInputAction;
    [SerializeField] private InputActionProperty pinchValueInputAction;
    [SerializeField] private InputActionProperty gripValueInputAction;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorOverrideController baseAnimatorController;
    private List<GameObject> detectedObjects = new();
    private bool isInteracts;
    private IUsable usable;
    private IUpdatable updatable;
    public bool IsInteracts => isInteracts;
    private void Update()
    {
        if (pinchInputAction.action.triggered)
        {
            if (isInteracts == false)
            {
                TryInteract();
            }
        }
        float triggerValue = pinchInputAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", triggerValue);

        float gripValue = gripInputAction.action.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);

        if (isInteracts != false)
        {
            if (gripValue > 0)
            {
                UseObject();
            }
            UpdateInteractWithObject(triggerValue);
        }
    }
    public bool CheckInteractableObject(out GameObject interactableObject)
    {
        interactableObject = Physics.OverlapSphere(transform.position, 0.5f, interactableLayerMask, QueryTriggerInteraction.Collide)[0].gameObject;
        if (interactableObject)
        {
            return true;
        }
        return false;
    }
    public bool TryInteract()
    {
        if (detectedObjects.Count > 0)
        {
            usable = detectedObjects[0].GetComponent<IUsable>();
            updatable = detectedObjects[0].GetComponent<IUpdatable>();
            isInteracts = interactableObject.StartInteract(gameObject);
            return true;
        }
        return false;
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
            isInteracts = false;
        }
        usable = null;
        updatable = null;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (interactableLayerMask == (interactableLayerMask | (1 << collision.gameObject.layer)))
        {
            detectedObjects.Remove(collision.gameObject);
            collision.gameObject.GetComponent<Outline>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (interactableLayerMask == (interactableLayerMask | (1 << collision.gameObject.layer)))
        {
            detectedObjects.Add(collision.gameObject);
            collision.gameObject.GetComponent<Outline>().enabled = true;
        }
    }
}
