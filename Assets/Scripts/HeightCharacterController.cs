using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class HeightCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterController characterCollider;
    [SerializeField] private XROrigin xROrigin;
    [SerializeField] private float defaultCameraOffset = 1.7f;

    private void Start()
    {
        characterCollider = GetComponent<CharacterController>();
        if (xROrigin.CurrentTrackingOriginMode != UnityEngine.XR.TrackingOriginModeFlags.Floor)
        {
            xROrigin.CameraYOffset = defaultCameraOffset;
        }
    }
    private void LateUpdate()
    {
        if (xROrigin.CurrentTrackingOriginMode == UnityEngine.XR.TrackingOriginModeFlags.Floor)
        {
            characterCollider.center = new Vector3(0, xROrigin.CameraYOffset / 2, 0);
            characterCollider.height = xROrigin.CameraYOffset;
        }
    }
}
