using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clothes : LocalSingleton<Clothes>
{

    [System.Serializable]
    public class ClothesInfo
    {
        public int whichType;
        public int whichOrder;

        public GameObject clothes;
        public int price;
    }


    public ClothesInfo[] myClothes;

    public ParticleSystem[] effects;

    public ClothesPort[] ports;
    

    public int GetPrice(int whichPart, int whichClothes)
    {
        

        for (int i = 0; i < myClothes.Length; i++)
        {
            if(myClothes[i].whichType == whichPart - 6 && myClothes[i].whichOrder == whichClothes)
            {
                return myClothes[i].price;
            }
        }
        return 0;

    }

    public void RunAllScripts(int currentMoney)
    {
        for (int i = 0; i < ports.Length; i++)
        {
            try
            {
                ports[i].ChangeColor(currentMoney);
            }
            catch
            {

            }
        }
    }
    
    
    
}
