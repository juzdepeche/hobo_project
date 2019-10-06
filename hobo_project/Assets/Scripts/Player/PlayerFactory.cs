using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    public static PlayerFactory Instance;
    public GameObject Character;
    public Material Player1Skin;
    public Material Player2Skin;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnPlayer(Player player)
    {
        GameObject character = Instantiate(Character, GameController.Instance.GetRandomSpawnPoint(), Quaternion.identity);

        var ctrl = character.GetComponent<PlayerController>();
        ctrl.SetPlayerDevice(player);
        ctrl.Mesh.GetComponent<Renderer>().material = player.PlayerIndex == 0 ? Player1Skin : Player2Skin;
        return character;
    }
}
