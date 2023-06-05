using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeightCharacterController : MonoBehaviour
{
    [SerializeField] private InputActionProperty headPosition;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private XROrigin xROrigin;
    [SerializeField] private float defaultCameraOffset = 1.7f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void LateUpdate()
    {
        float yPos = headPosition.action.ReadValue<Vector3>().y;
        if (Mathf.Abs(characterController.height - yPos) > 0.2)
        {
            characterController.center = new Vector3(0, yPos / 2, 0);
            characterController.height = yPos;
        }
    }
}
