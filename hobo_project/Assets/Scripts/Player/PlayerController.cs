using InControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public ParticleSystem DustSteps;
    public Transform MoneySpawnPoint;
    private float time;
    private float dashInterval = 2f;
    public float DashForce;
    public float dashDuration = 0.2f;
    public bool canMove = true;
    public bool canBeShank = true;
    public bool isShanked = false;
    public Behaviour shankHalo;
    private Animator Animator;
    public GameObject Mesh;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        shankHalo.enabled = false;
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

        if (canMove)
        {
            rb.velocity = movement;
            if (movement != Vector3.zero) Animator.SetBool("walk", true);
            else Animator.SetBool("walk", false);
        }
        if (movement != Vector3.zero && canMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }

        if (player.Device.Action1.WasPressed && canMove)
        {
            Animator.SetTrigger("attack");
            StartCoroutine(Attack());
        }
        else if (player.Device.RightBumper.WasPressed && canMove)
        {
            if(time > dashInterval)
            {
                time = 0;
                StartCoroutine(Dash());
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Shank();
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
        for (var i =0; i < inventory.appleNumber; i++)
        {
            GameObjectFactory.Instance.SpawnApple(gameObject.transform);
        }
        inventory.appleNumber = 0;
    }

    private IEnumerator Dash()
    {
        var audio = gameObject.GetComponents<AudioSource>().FirstOrDefault(a => a.clip.name.Contains("run"));
        if (audio)
            audio.Play();

        Vector3 force = transform.forward + new Vector3(rb.velocity.x, 0.05f, rb.velocity.z);
        rb.AddForce(force * DashForce, ForceMode.VelocityChange);
        DustSteps.Play();
        yield return new WaitForSeconds(dashDuration);
        DustSteps.Stop();
        rb.velocity = Vector3.zero;
    }

    public void Shank()
    {
        if (canBeShank)
        {
            var audio = gameObject.GetComponentsInParent<AudioSource>().FirstOrDefault(a => a.clip.name.Contains("carhit"));
            if (audio)
                audio.Play();

            shankHalo.enabled = true;
            isShanked = true;
            canMove = false;
            canBeShank = false;
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
            GameObjectFactory.Instance.SpawnMoneyBag(MoneySpawnPoint, inventory.money == 1 ? 1 : inventory.money / 2);
            inventory.money = inventory.money == 1 ? 1 : inventory.money / 2;
            StartCoroutine(Revive());
        }
    }

    private IEnumerator Revive()
    {
        yield return new WaitForSeconds(3f);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        canMove = true;
        isShanked = false;
        StartCoroutine(SetCanBeShank(true));
    }

    private IEnumerator SetCanBeShank(bool can)
    {
        yield return new WaitForSeconds(2f);
        canBeShank = can;
        shankHalo.enabled = false;
    }

    private void SetBasicRigidBodyConstrait()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void SetRigidBodyConstraitForDash()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.7f);
        GameController.Instance.RequestPlayerAction1(GetPlayerGUID());
    }
}
