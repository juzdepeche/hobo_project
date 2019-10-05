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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xMoveInput = player.Device.LeftStickX; // ?? player.Device.DPadX;
        yMoveInput = player.Device.LeftStickY; // ?? player.Device.DPadY;

        rb.velocity = new Vector3(xMoveInput * speed, rb.velocity.y, yMoveInput * speed);
    }

    public void SetPlayerDevice(Player player)
    {
        this.player = player;
    }
}
