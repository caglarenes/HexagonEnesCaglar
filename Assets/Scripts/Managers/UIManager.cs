using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject endGameScreen;

    public Text pointText;

    public void ShowEndGameScreen()
    {
        endGameScreen.SetActive(true);
    }

    public void HideEndGameScreen()
    {
        endGameScreen.SetActive(false);
    }

    public void UpdatePoint(int point)
    {
        pointText.text = point.ToString();
    }
}
