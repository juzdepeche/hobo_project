﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private List<GameObject> Players;
    private Dictionary<string, State> PlayerState;
    public Transform[] SpawnPoints;
    public Transform DeadzonePoint;

    private bool isGameOver = false;
    private float timePeriode = 0.0f;
    private float timeEndGame = 180.0f;
    private float timeRestartToMenu = 5.0f;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (!isGameOver)
        {
            timePeriode += Time.deltaTime;
            updateTimerText(timePeriode, timeEndGame);
            if (timePeriode >= timeEndGame)
            {
                isGameOver = true;
                timePeriode = 0.0f;
                var canvas = GetUICanvas();
                var uiManager = canvas?.GetComponent<UIManager>();
                if (uiManager)
                {
                    int moneyPlayer1 = 0;
                    int moneyPlayer2 = 0;
                    if (Players.Any())
                    {
                        var inv = Players[0].GetComponent<Inventory>();
                        if (inv)
                            moneyPlayer1 = inv.money;

                        if (Players.Count > 1)
                        {
                            inv = Players[1].GetComponent<Inventory>();
                            if (inv)
                                moneyPlayer2 = inv.money;
                        }

                    }
                    uiManager.showGameOver(moneyPlayer1, moneyPlayer2);
                }
            }
        }
        else
        {
            timePeriode += Time.deltaTime;
            if (timePeriode >= timeRestartToMenu)
            {
                SceneManager.LoadScene(0); //Return to menu
            }
        }
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

    private Canvas GetUICanvas()
    {
        var canvas = GameObject.FindObjectOfType<Canvas>();

        if (!canvas)
            return null;

        return canvas;
    }

    private void updateTimerText(float time, float maxTime)
    {
        var canvas = GetUICanvas();
        var uiManager = canvas?.GetComponent<UIManager>();        
        if (uiManager)
            uiManager.updateTimer(time, maxTime);       
    }
}
