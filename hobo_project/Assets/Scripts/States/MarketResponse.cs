using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketResponse : IStateResponse
{
    private bool success;
    public bool Success
    {

        get { return success; }

        set { success = value; }

    }
}
