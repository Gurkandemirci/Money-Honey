using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    AnimancerComponent anim;

    private float speed = 4f;
    private float movSpeed = 0.3f;

    public bool isDressed;
    public bool isBooted;

    public GameObject connectedHoney;

    public ParticleSystem disconnectHoneyEffect;
    public ParticleSystem takeClothesEffect;
    public ParticleSystem upgradeEffect;

    public int currentMoney = 0;
    public int totalMoney = 0;


    public Stack<GameObject> moneyStack = new Stack<GameObject>();


    private void Start()
    {
        anim = GetComponent<AnimancerComponent>();
        AnimationManager.instance.PlayAnim(AnimationManager.instance.honeyIdle, anim, 0.8f, 1);
    }

    public void StartWalkAnim()
    {
        
        AnimationManager.instance.PlayAnim(AnimationManager.instance.playerSadWalk, anim, 0.8f, 1);
    }


    private void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (speed * Time.deltaTime));

            if (Input.GetMouseButton(0))
            {
                float x = 0;

                x = movSpeed * Input.GetAxis("Mouse X");

                if (transform.position.x > 4 && x > 0)
                {
                    x = 0;
                }

                if (transform.position.x < -4 && x < 0)
                {
                    x = 0;
                }

                transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
            }

            if(transform.position.z > 125)
            {
                GetComponent<Rigidbody>().freezeRotation = true;
                GameManager.Instance.FinishLevel(totalMoney);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<EnumManager>().myPortType == PortTypes.ClothesPort &&
            other.transform.parent.GetComponent<ClothesPort>().thisPrice <= currentMoney)
        {
            takeClothesEffect.Play();

            ClothesPort port = other.gameObject.GetComponentInParent<ClothesPort>();

            currentMoney = currentMoney - port.thisPrice;
            totalMoney += port.thisPrice;
            UIManager.Instance.UpgradeStatus(totalMoney);

            DisplayMoney(false,port.thisPrice);

            WearClothes(port);

            Destroy(other.gameObject.transform.parent.gameObject);
        }

        else if (other.transform.parent.GetComponent<EnumManager>().myPortType == PortTypes.JobPort)
        {
            if (connectedHoney != null)
            {
                DeleteFormerHoney();
            }
            Bridge bridge = other.gameObject.GetComponentInParent<Bridge>();
            connectedHoney = bridge.SendCharacter();
            ConnectHoney();


            DeleteBridge(other.gameObject.transform.parent.gameObject);

        }
        Debug.Log(currentMoney);


        //ClothesPort.Instance.ChangeColor(currentMoney);

        Clothes.Instance.RunAllScripts(currentMoney);
    }

    public void WearClothes(ClothesPort port)
    {
        for(int i=0; i< transform.GetChild(port.whichPart).childCount; i++)
        {
            transform.GetChild(port.whichPart).GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(port.whichPart).GetChild(port.whichClothes).gameObject.SetActive(true);
    }





    public void DeleteFormerHoney()
    {
        Destroy(connectedHoney.gameObject);
        connectedHoney = null;
        DisplayMoney(false);
    }

    public void ConnectHoney()
    {
        
        Honey honeyScript = connectedHoney.GetComponent<Honey>();

        connectedHoney.transform.parent = transform;
        connectedHoney.transform.Rotate(0,180 , 0);
        connectedHoney.transform.position = new Vector3(transform.position.x + 0.55f,transform.position.y,transform.position.z);
        honeyScript.PlayWalkWithMoneyAnim();
        EmptyStack();
        currentMoney = honeyScript.salary;
        DisplayMoney(true);
    }

    public void EmptyStack()
    {
        while (true)
        {
            try
            {
                MoneyPool.instance.AddToPool(moneyStack.Pop());
            }
            catch
            {
                break;
            }
        }
    }
    
    public void DisplayMoney(bool isAdding, int thisMoney = 0)
    {

        if (isAdding)
        {
            for (int i = 0; i < currentMoney / 50; i++)
            {
                GameObject tempMoney = MoneyPool.instance.TakeOutFromPool();
                tempMoney.transform.parent = transform;
                tempMoney.transform.position = new Vector3(transform.position.x + 0.95f, transform.position.y + 1.2f + 0.03f * i, transform.position.z + 0.4f);

                moneyStack.Push(tempMoney);
            }
            
        }
         
        else
        {
            for (int i = 0; i < thisMoney / 50; i++)
            {
                if (moneyStack.Count != 0)
                {
                    MoneyPool.instance.AddToPool(moneyStack.Pop());
                }

                else
                {
                    disconnectHoneyEffect.Play();
                    Destroy(connectedHoney);
                    currentMoney = 0;
                }

            }
            if(moneyStack.Count == 0)
            {
                disconnectHoneyEffect.Play();
                Destroy(connectedHoney);
                currentMoney = 0;
            }
            
        }
    }

    public void DeleteBridge(GameObject bridge)
    {
        for (int i = 0; i < bridge.transform.parent.childCount ; i++)
        {
            bridge.transform.parent.GetChild(i).gameObject.SetActive(false);
        }
        
    }

    public void UpgradeSecondLevelAnim()
    {
        upgradeEffect.Play();
        AnimationManager.instance.PlayAnim(AnimationManager.instance.playerNormalWalk, anim, 0.8f, 1);
    }

    public void UpgradeThirdLevelAnim()
    {
        upgradeEffect.Play();
        AnimationManager.instance.PlayAnim(AnimationManager.instance.playerModelWalk, anim, 0.8f, 1);
    }

    public void PlayFinishAnim()
    {
        DeleteFormerHoney();
        EmptyStack();

        if (totalMoney < 4000)
        {
            AnimationManager.instance.PlayAnim(AnimationManager.instance.playerSadFinish, anim, 0.8f, 1);
        }
        else if (totalMoney < 9000)
        {
            AnimationManager.instance.PlayAnim(AnimationManager.instance.playerNormalFinish, anim, 0.8f, 1);
        }
        else
        {
            AnimationManager.instance.PlayAnim(AnimationManager.instance.playerVictoryFinish, anim, 0.8f, 0.8f);
        }
    }
}



