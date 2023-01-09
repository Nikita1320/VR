using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected AnimatorOverrideController overrideAnimator;
    protected GameObject hand;
    protected FixedJoint fixedJoint;
    public virtual bool StartInteract(GameObject _hand)
    {
        hand = _hand;
        hand.GetComponent<Animator>().runtimeAnimatorController = overrideAnimator;
        fixedJoint.connectedBody = hand.GetComponent<Rigidbody>();
        return true;
    }
    public virtual void EndInteract()
    {
        fixedJoint.connectedBody = null;
    }
}
