using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
    public static GameObjectFactory Instance;

    public GameObject Apple;
    public GameObject MoneyBag;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void SpawnApple(Transform transform)
    {
        var apple = Instantiate(Apple, transform.position, transform.rotation);

        float scaler = UnityEngine.Random.Range(0.8f, 1f);
        apple.transform.localScale = new Vector3(scaler, scaler, scaler);

        var appleRb = apple.GetComponent<Rigidbody>();
        appleRb.velocity = new Vector3(appleRb.velocity.x + getOffSetForce(), appleRb.velocity.y + getOffSetForce(), appleRb.velocity.z + getOffSetForce());
    }

    public void SpawnMoneyBag(Transform transform, int moneyAmount)
    {
        if (moneyAmount <= 0) return;
        var moneyBag = Instantiate(MoneyBag, transform.position, transform.rotation);

        var moneyBagRb = moneyBag.GetComponent<Rigidbody>();
        moneyBagRb.AddForce(new Vector3(getOffSetForce(-5f, 5f), 10f, getOffSetForce(-5f, 5f)), ForceMode.VelocityChange);

        var moneyBagCtrl = moneyBag.GetComponent<MoneyBag>();
        moneyBagCtrl.value = moneyAmount;
    }

    private float getOffSetForce(float x1 = -1f, float x2 = 1f)
    {
        return UnityEngine.Random.Range(x1, x2);
    }
}
