using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateInput : StateInput
{
    private InputController inputController;
    private GameObject hand;
    private Animator animator;
    private InteractionManager interactionManager;
    private MovementController movementController;
    public override void Init(InputController controllableObject)
    {
        inputController = controllableObject;
        hand = controllableObject.gameObject;
        animator = hand.GetComponent<Animator>();
        interactionManager = hand.GetComponent<InteractionManager>();
        movementController = hand.GetComponentInParent<MovementController>();
    }

    public override void Update()
    {
        if (inputController.PinchInputAction.action.triggered)
        {
            if (interactionManager.IsInteracts == false)
            {
                interactionManager.CheckInteractableObject(out GameObject interactable);
                if (interactable != null)
                {
                    if (interactionManager.ToInteract(interactable.GetComponent<Interactable>()))
                    {
                        inputController.ChangeState(StateHand.InteractsWithObject);
                        return;
                    }
                }
            }
        }
        Vector2 direction = inputController.TouchPadValueInputAction.action.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            movementController.Move(direction);
        }

        float triggerValue = inputController.PinchInputAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", triggerValue);

        float gripValue = inputController.GripInputAction.action.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);
    }
}
