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
        Vector2 target = new Vector2(0, 0);

        GameObject character = Instantiate(Character, target, Quaternion.identity);
        character.GetComponent<PlayerController>().SetPlayerDevice(player);
        return character;
    }
}
