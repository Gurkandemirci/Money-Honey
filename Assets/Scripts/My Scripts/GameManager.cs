using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{

    [System.Serializable]   
    public class JobInfo
    {
        public Jobs jobs;
        public GameObject jobPrefab;
    }

    [System.Serializable]
    public class ClothesInfo
    {
        public ClothesEnum clothesEnum;
        public GameObject clothesPrefab;
    }


    public JobInfo[] myJobInfo;

    public bool isGameStarted;
    public bool isGameEnded;

    public void StartGame()
    {
        isGameStarted = true;
        UIManager.Instance.ActivateStatus();
    }

    public void FinishLevel(int totalMoney)
    {
        isGameStarted = false;
        isGameEnded = true;
        UIManager.Instance.ActivateFinishLevel(totalMoney);

    }


    public GameObject GetJobPrefab(Jobs job)
    {
        for (int i = 0; i < myJobInfo.Length; i++)
        {
            if (job == myJobInfo[i].jobs)
            {
                return myJobInfo[i].jobPrefab;
            }
        }
        return null;
    }



}
