using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public abstract class Interactable : MonoBehaviour
{
    public abstract bool StartInteract(GameObject _hand);
    public abstract void EndInteract();
}
