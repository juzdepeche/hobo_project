using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private List<GameObject> Players;
    private Dictionary<string, State> PlayerState;
    public Transform[] SpawnPoints;
    public Transform DeadzonePoint;

    private void Awake()
    {
        Players = new List<GameObject>();
        PlayerState = new Dictionary<string, State>();
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        WatchState("player_death");
        WatchState("update_apple_ui");
        WatchState("update_money_ui");
    }

    public void NotifyPlayerState(PlayerController player, string marketType, bool activate, Func<GameObject, IStateResponse> actionCallback = null)
    {
        string key = ConcatPlayerKey(player, marketType);
        SetPlayerStateValue(key, new State(marketType, activate, actionCallback));
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
        List<string> keys = new List<string>(PlayerState.Keys);
        foreach (string key in keys)
        {
            if (key.Contains(guid) && PlayerState[key].Activated)
            {
                PlayerState[key].CallbackAction(PlayerManager.GetPlayerGameObjectFromDeviceGUID(Players, guid));
            }
        }
    }

    private void WatchState(string stateType)
    {
        List<string> keys = new List<string>(PlayerState.Keys);
        foreach (string key in keys)
        {
            if (key.Contains(stateType) && PlayerState[key].Activated)
            {
                PlayerState[key].CallbackAction(PlayerManager.GetPlayerGameObjectFromDeviceGUID(Players, GetGUIDFromPlayerState(key)));
            }
        }
    }

    private string GetGUIDFromPlayerState(string state)
    {
        return state.Split('_')[0];
    }

    public Vector3 GetRandomSpawnPoint()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, Instance.SpawnPoints.Length);
        return Instance.SpawnPoints[spawnPointIndex].position;
    }
}
