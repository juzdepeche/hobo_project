using InControl;
using System;
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
    public ParticleSystem MoneySparkle;
    public Transform MoneySparklePoint;
    private float time;
    private float dashInterval = 2f;
    public float DashForce;
    public float dashDuration = 0.6f;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        xMoveInput = player.Device.LeftStickX; // ?? player.Device.DPadX;
        yMoveInput = player.Device.LeftStickY; // ?? player.Device.DPadY;

        Vector3 movement = new Vector3(xMoveInput * speed, rb.velocity.y, yMoveInput * speed);
        //adapt to camera angle 
        movement = Quaternion.AngleAxis(-45, Vector3.up) * movement;

        if (canMove) rb.velocity = movement;
        if (movement != Vector3.zero && canMove) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

        if (player.Device.Action1.WasPressed && canMove)
        {
            GameController.Instance.RequestPlayerAction1(GetPlayerGUID());
        }
        else if (player.Device.RightBumper.WasPressed && canMove)
        {
            if(time > dashInterval)
            {
                time = 0;
                StartCoroutine(Dash());
            }
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

    public int GetPlayerIndex()
    {
        return player.PlayerIndex;
    }

    private string GetPlayerGUID()
    {
        return player.Device.GUID.ToString();
    }

    public void SparkMoney()
    {
        Instantiate(MoneySparkle, MoneySparklePoint.position, Quaternion.identity);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        for(var i =0; i < inventory.appleNumber; i++)
        {
            GameObjectFactory.Instance.SpawnApple(gameObject.transform);
        }
        inventory.appleNumber = 0;
    }

    private IEnumerator Dash()
    {
        Vector3 force = transform.forward + new Vector3(rb.velocity.x, 0.05f, rb.velocity.z);
        rb.AddForce(force * DashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
    }

    public void Shank()
    {
        canMove = false;
        transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        StartCoroutine(Revive());
    }

    private IEnumerator Revive()
    {
        yield return new WaitForSeconds(3f);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        canMove = true;
    }
}
