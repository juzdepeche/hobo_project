using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private List<GameObject> Players;
    private Dictionary<string, State> PlayerState;

    private void Awake()
    {
        Players = new List<GameObject>();
        PlayerState = new Dictionary<string, State>();
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void NotifyPlayerPresenceIntoMarket(PlayerController player, string marketType, Func<GameObject, IStateResponse> actionCallback)
    {
        string key = ConcatPlayerKey(player, marketType);
        SetPlayerStateValue(key, new State(marketType, true, actionCallback));
    }

    public void NotifyPlayerExitFromMarket(PlayerController player, string marketType)
    {
        string key = ConcatPlayerKey(player, marketType);
        SetPlayerStateValue(key, new State(marketType, false));
    }

    public void AddPlayer(GameObject newPlayer)
    {
        Players.Add(newPlayer);
    }

    private string ConcatPlayerKey(PlayerController player, string suffixe)
    {
        return ConcatKey(player.GetPlayerDevice().GUID.ToString(), suffixe);
    }

    private string ConcatKey(string guid, string suffixe)
    {
        return guid + '_' + suffixe;
    }

    private void SetPlayerStateValue(string key, State value)
    {
        if (PlayerState.ContainsKey(key))
        {
            PlayerState[key] = value;
        }
        else
        {
            PlayerState.Add(key, value);
        }
    }

    public void RequestPlayerAction1(string guid)
    {
        NavigateState(guid);
    }

    private void NavigateState(string guid)
    {
        foreach (KeyValuePair<string, State> state in PlayerState)
        {
            if(state.Key.Contains(guid) && state.Value.Activated)
            {
                state.Value.CallbackAction(PlayerManager.GetPlayerGameObjectFromDeviceGUID(Players, guid));
            }
        }
    }
}
