using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartLevel()
    {
        GameManager.Instance.isGameStarted = false;
        GameManager.Instance.isGameEnded = false;
        SceneManager.LoadScene("SampleScene");
    }
}
