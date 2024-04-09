using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //singleton
    public static UI instance;
    //
    public Text Score;
    public Text ScoreGOV;
    public Text Mine;
    public GameObject GameOverPanel;
    public GameObject Menu;
    public Text HealthTxt;
    public Text AtkTxt;
    public Text highestTxt;
    public void Awake()
    {
        UI.instance = this;
    }
    public void SetScore(string value)
    {
        Score.text = value;
    }
    public void SetHighestScore(string value)
    {
        highestTxt.text = value;
    }
    public void Showisgameover(bool isGameOver)
    {
        GameOverPanel.SetActive(isGameOver);
    }
    public void SetScoreGameOver(string value)
    {
        ScoreGOV.text = value;
    }
    public void SetMine(string value)
    {
        Mine.text = value;
    }
    public void ShowMenu(bool value)
    {
        Menu.SetActive(value);
    }
    public void SetHealth(string value)
    {
        HealthTxt.text = value;
    }
    public void SetATK(string value)
    {
        AtkTxt.text = value;
    }

}
