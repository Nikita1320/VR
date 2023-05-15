using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public enum StateHand
{
    Idle,
    InteractsWithObject,
    InteractsWithInterface
}
public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchInputAction;
    [SerializeField] private InputActionProperty gripInputAction;
    [SerializeField] private InputActionProperty pinchValueInputAction;
    [SerializeField] private InputActionProperty gripValueInputAction;
    [SerializeField] private InputActionProperty touchPadValueInputAction;
    [SerializeField] private ActionBasedController controller;
    private List<StateInput> statesInput = new();
    private StateHand currentStateHand;

    public InputActionProperty PinchInputAction => pinchInputAction;
    public InputActionProperty GripInputAction => gripInputAction;
    public InputActionProperty PinchValueInputAction => pinchValueInputAction;
    public InputActionProperty GripValueInputAction => gripValueInputAction;
    public InputActionProperty TouchPadValueInputAction => touchPadValueInputAction;

    private void Start()
    {
        InitState();
        currentStateHand = StateHand.Idle;
    }

    private void Update()
    {
        statesInput[(int)currentStateHand].Update();
    }

    public void ChangeState(StateHand nextStateHand)
    {
        currentStateHand = nextStateHand;
    }
    public void InitState()
    {
        statesInput.Add(new IdleStateInput());
        statesInput.Add(new InteractsStateInput());
        for (int i = 0; i < statesInput.Count; i++)
        {
            statesInput[i].Init(this);
        }
    }
}
