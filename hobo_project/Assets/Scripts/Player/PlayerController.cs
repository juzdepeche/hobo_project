using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    public float xMoveInput;
    public float yMoveInput;
    public float speed = 10f;
    private Rigidbody rb;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        xMoveInput = player.Device.LeftStickX; // ?? player.Device.DPadX;
        yMoveInput = player.Device.LeftStickY; // ?? player.Device.DPadY;

        Vector3 movement = new Vector3(xMoveInput * speed, rb.velocity.y, yMoveInput * speed);
        rb.velocity = movement;
        if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

        if (player.Device.Action1.WasPressed)
        {
            GameController.Instance.RequestPlayerAction1(GetPlayerGUID());
        }
    }

    public void SetPlayerDevice(Player player)
    {
        this.player = player;
    }

    public InputDevice GetPlayerDevice()
    {
        return player.Device;
    }

    private string GetPlayerGUID()
    {
        return player.Device.GUID.ToString();
    }

    public void Die()
    {
        Destroy(gameObject);
        for(var i =0; i < inventory.appleNumber; i++)
        {
            GameObjectFactory.Instance.SpawnApple(gameObject.transform);
        }
    }
}
