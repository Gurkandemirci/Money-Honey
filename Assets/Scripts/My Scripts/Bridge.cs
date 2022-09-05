using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bridge : MonoBehaviour
{
    public Jobs myJob;
    public GameObject instantiatedObj;

    public Transform place;

    public TMP_Text title;

    void Start()
    {

        instantiatedObj=Instantiate(GameManager.Instance.GetJobPrefab(myJob));

        instantiatedObj.transform.SetParent(transform);
        instantiatedObj.transform.position = place.position;
        title.text = myJob.ToString();

    }

    public GameObject SendCharacter()
    {
        return instantiatedObj;
    }

}
