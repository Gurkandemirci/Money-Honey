using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPool : MonoBehaviour
{

    public static MoneyPool instance;

    public GameObject moneyClone;


    public List<GameObject> moneys;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject tempMoney = Instantiate(moneyClone, this.transform);
            moneys.Add(tempMoney);
        }
    }

    public GameObject TakeOutFromPool()
    {
        for (int i = 0; i < moneys.Count; i++)
        {
            if (!moneys[i].activeSelf)
            {
                moneys[i].SetActive(true);
                GameObject tempMoney = moneys[i];
                moneys.RemoveAt(i);
                return tempMoney;
            }
        }
        return null;
    }

    public void AddToPool(GameObject money)
    {
        money.SetActive(false);
        money.transform.parent = transform;
        moneys.Add(money);
    }
}
