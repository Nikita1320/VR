using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchInputAction;
    [SerializeField] private InputActionProperty gripInputAction;
    [SerializeField] private Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchInputAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripInputAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
