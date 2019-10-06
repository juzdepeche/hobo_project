using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashContainer : MonoBehaviour
{
    public GameObject[] Items;
    public Transform SpawnPoint;
    public Behaviour halo;
    private float time = 0;
    private float reloadInterval = 20f;
    private bool activated = true;
    public bool negativeForce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && activated)
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "trash_container_" + this.name, true, ThrowRandomReward);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.Instance.NotifyPlayerState(other.gameObject.GetComponent<PlayerController>(), "trash_container_" + this.name, false, ThrowRandomReward);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= reloadInterval)
        {
            if (!activated)
            {
                activated = true;
                halo.enabled = true;
            }
        }
    }

    private IStateResponse ThrowRandomReward(GameObject player)
    {
        BaseResponse response = new BaseResponse();
        response.Success = true;

        if (activated)
        {
            halo.enabled = false;
            activated = false;
            time = 0;

            var randomItem = Items[Random.Range(0, Items.Length)];

            float z = 2.0f;
            if (negativeForce)
                z *= -1;

            Vector3 throwForce = new Vector3(0f, 5f, z);

            var newItem = Instantiate(randomItem, SpawnPoint.position, gameObject.transform.rotation);
            newItem.GetComponent<Rigidbody>().velocity += throwForce;
        }

        return response;
    }

}
