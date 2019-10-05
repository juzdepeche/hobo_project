using InControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int currentPlayerIndex = 0;

    public List<Player> players;
    private List<Guid> deviceGUID;
    
    void Start()
    {
        players = new List<Player>();
        deviceGUID = new List<Guid>();
    }
    
    void Update()
    {
        /*
        foreach (var device in InputManager.Devices)
        {
            if (device.GetControl(InputControlType.Action1).WasPressed)
            {
                Player newPlayer = CreatePlayerForDevice(device);
                if (newPlayer != null)
                {
                    PlayerFactory.Instance.SpawnPlayer(newPlayer);
                }
            }
        }
        */
    }

    private Player GetPlayerFromDeviceGUID(Guid guid)
    {
        foreach (var player in players)
        {
            if (player.Device == null) continue;
            if (player.Device.GUID == guid)
            {
                return player;
            }
        }
        return null;
    }

    private Player CreatePlayerForDevice(InputDevice device)
    {
        bool alreadyCreated = AddDeviceGUID(device.GUID);
        if (alreadyCreated) return null;
        
        Player newPlayer = new Player(device, currentPlayerIndex);

        players.Add(newPlayer);
        currentPlayerIndex++;
        return newPlayer;
    }

    private bool AddDeviceGUID(Guid gUID)
    {
        if (deviceGUID.Contains(gUID)) return true;

        deviceGUID.Add(gUID);

        return false;
    }
}
