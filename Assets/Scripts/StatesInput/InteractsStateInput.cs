using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractsStateInput : StateInput
{
    private GameObject hand;
    private InputController inputController;
    private InteractionManager interactionManager;
    private Animator animator;
    private float valueBrokeLink = 0.1f;
    public override void Init(InputController controllableObject)
    {
        hand = controllableObject.gameObject;
        inputController = hand.GetComponent<InputController>();
        interactionManager = hand.GetComponent<InteractionManager>();
        animator = hand.GetComponentInChildren<Animator>();
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
        float powerPinch = inputController.PinchValueInputAction.action.ReadValue<float>();
        if (powerPinch <= valueBrokeLink)
        {
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
