using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClothesPort : LocalSingleton<ClothesPort>
{
    //public static ClothesPort instance;

    public ClothesEnum myPart;
    public ClothesEnum myClothes;

    public GameObject NakedPlayer;
    public GameObject instantiatedObj;
    public GameObject activeClothes;

    public ParticleSystem particleEffect;

    public Image clothesPriceImage;
    public TMP_Text clothesPriceText;

    public int whichPart;
    public int whichClothes;
    public int thisPrice;

    void Awake()
    {
        
        //instance = this;

        instantiatedObj = Instantiate(NakedPlayer);
        instantiatedObj.transform.parent = transform;
        instantiatedObj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < NakedPlayer.transform.childCount; i++)
        {
            if(NakedPlayer.transform.GetChild(i).TryGetComponent<EnumManager>(out var partEnum))
            {
                if (partEnum.myPart == myPart)
                {
                    whichPart = i;

                    if (i == 6)
                    {
                        instantiatedObj.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                        
                    }
                        
                    else if(i == 7)
                        instantiatedObj.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                    else if(i == 9)
                        instantiatedObj.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);



                    for (int j = 0; j < NakedPlayer.transform.GetChild(i).childCount; j++)
                    {
                        if (NakedPlayer.transform.GetChild(i).GetChild(j).TryGetComponent<EnumManager>(out var clothesEnum))
                        {
                            if (clothesEnum.myClothes == myClothes)
                            {
                                whichClothes = j;
                            }
                        }
                    }
                }
            }
        }

        
        // particle effect
        if(whichPart == 6) {
            particleEffect = Clothes.Instance.effects[0];
        }
        else if(whichPart == 7)
        {
            particleEffect = Clothes.Instance.effects[1];
        }
        else if(whichPart == 8)
        {
            particleEffect = Clothes.Instance.effects[2];
        }
        else if(whichPart == 9)
        {
            particleEffect = Clothes.Instance.effects[3];
        }
        else
        {
            particleEffect = null;
        }

        Instantiate(particleEffect,transform);
        particleEffect.transform.position = new Vector3(0,0,0);
        particleEffect.Play();



        activeClothes = instantiatedObj.transform.GetChild(whichPart).GetChild(whichClothes).gameObject;
        activeClothes.SetActive(true);

        thisPrice = Clothes.Instance.GetPrice(whichPart, whichClothes);
        clothesPriceText.text = thisPrice.ToString();


    }


    void Update()
    {
        instantiatedObj.transform.Rotate(0,0.7f,0);
    }





    //-----------------------------------------------
    public void ChangeColor(int currentMoney)
    {
        if(currentMoney < thisPrice)
        {
            clothesPriceImage.color = Color.red;
        }
        else
        {
            clothesPriceImage.color = Color.green;
        }
    }

}
