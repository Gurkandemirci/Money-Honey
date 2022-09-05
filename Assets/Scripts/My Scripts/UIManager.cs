using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : LocalSingleton<UIManager>
{
    public bool isSecond;
    public bool isThird;

    public Player player;


    public Slider statuSlider;
    public TMP_Text statusText;
    public Image rateImage;
    public Button startButton;

    public TMP_Text tapStartText;
    public TMP_Text finishText;
    public Button tryAgainButton;

    void Start()
    {
        tapStartText.transform.DOShakeRotation(5,0.4f).SetLoops(-1);
    }

    public void ActivateStatus()
    {
        statuSlider.gameObject.SetActive(true);
        statusText.gameObject.SetActive(true);
        player.StartWalkAnim();
        tapStartText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);

    }

    public void ActivateFinishLevel(int totalMoney)
    {
        tryAgainButton.gameObject.SetActive(true);
        finishText.gameObject.SetActive(true);
        if (totalMoney < 4000)
        {
            finishText.text = "You are Poor";
            finishText.color = Color.red;
        }
        else if (totalMoney < 9000)
        {
            finishText.text = "Not  Enough";
            finishText.color = Color.yellow;
        }
        else
        {
            finishText.text = "You are Rich";
            finishText.color = Color.green;
        }

        player.PlayFinishAnim();


    }

    public void UpgradeStatus(int totalMoney)
    {

        if (totalMoney > 9000 && !isThird)
            PassThirdLevel();
        else if (totalMoney > 4000 && !isSecond)
            PassSecondLevel();

        BoostSliderValue(totalMoney);
    }

    public void PassSecondLevel()
    {
        isSecond = true;
        rateImage.color = Color.yellow;

        statusText.transform.DOShakeScale(1, 0.02f);
        statusText.text = "Decent";
        statusText.DOColor(Color.yellow, 1);
        player.UpgradeSecondLevelAnim();
    }
    public void PassThirdLevel()
    {
        isThird = true;
        rateImage.color = Color.green;

        statusText.transform.DOShakeScale(1, 0.04f);
        statusText.text = "Rich";
        statusText.DOColor(Color.green, 1);
        player.UpgradeThirdLevelAnim();
    }

    public void BoostSliderValue(int totalMoney)
    {
        statuSlider.value = totalMoney;
    }


}
