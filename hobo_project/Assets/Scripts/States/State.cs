using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public string Type;
    public bool Activated = false;
    public Func<GameObject, IStateResponse> CallbackAction;

    public State(string type, bool activated, Func<GameObject, IStateResponse> callbackAction = null)
    {
        Type = type;
        Activated = activated;
        CallbackAction = callbackAction;
    }
}
