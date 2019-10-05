using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    public static PlayerFactory Instance;
    public GameObject Character;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnPlayer(Player player)
    {
        GameObject character = Instantiate(Character, GameController.Instance.GetRandomSpawnPoint(), Quaternion.identity);
        character.GetComponent<PlayerController>().SetPlayerDevice(player);
        return character;
    }
}
