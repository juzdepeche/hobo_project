using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public InputDevice Device;
    public int PlayerIndex;

    public Player(InputDevice device, int index)
    {
        Device = device;
        PlayerIndex = index;
    }
}
