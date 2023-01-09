using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateInput
{
    public abstract void Init(InputController controllableObject);
    public abstract void Update();
}
