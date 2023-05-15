using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnProvider : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject xrOrigin;
    [SerializeField] private InputActionProperty touchPadValueInputAction;
    public InputActionProperty TouchPadValueInputAction => touchPadValueInputAction;

    private void Update()
    {
        Rotate();
    }
    public void Rotate()
    {
        Vector2 rotateDirection = touchPadValueInputAction.action.ReadValue<Vector2>();
        if (rotateDirection != Vector2.zero)
        {
            xrOrigin.transform.rotation = Quaternion.Lerp(xrOrigin.transform.rotation, Quaternion.Euler(0, xrOrigin.transform.rotation.y + 90 * rotateDirection.y, 0), rotateSpeed);
        }
    }
}
