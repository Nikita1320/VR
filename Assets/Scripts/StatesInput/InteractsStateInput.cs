using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractsStateInput : StateInput
{
    private GameObject hand;
    private InputController inputController;
    private InteractionManager interactionManager;
    private Animator animator;
    public override void Init(InputController controllableObject)
    {
        hand = controllableObject.gameObject;
        inputController = hand.GetComponent<InputController>();
        interactionManager = hand.GetComponent<InteractionManager>();
        animator = hand.GetComponent<Animator>();
    }

    public override void Update()
    {
        if (inputController.GripInputAction.action.triggered)
        {
            if (interactionManager.UseObject())
            {
                animator.SetFloat("Trigger", 1);
            }
        }
        if (inputController.PinchValueInputAction.action.IsPressed())
        {
            float powerPinch = inputController.PinchValueInputAction.action.ReadValue<float>();
            if (interactionManager.UpdateInteractWithObject(powerPinch))
            {
                animator.SetFloat("Grab", powerPinch);
            }
        }
        else
        {
            interactionManager.StopInteract();
            inputController.ChangeState(StateHand.Idle);
        }
    }
}
