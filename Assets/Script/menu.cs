using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    private UI ui;
    private UIStart uis;
    public GameObject startTransition;
    public GameObject endTransition;
    private void Awake()
    {
        startTransition.SetActive(true);
        ui = FindObjectOfType<UI>();
       uis = FindObjectOfType<UIStart>();
    }
    public void StartButton()
    {
        Time.timeScale = 1f;
        StartCoroutine(StartGameTransition());
        
    }
    public void BackToStart()
    {
        Time.timeScale = 1f;
        StartCoroutine(BackToStartTransition());
        
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Player.instance.CanAction();
        ui.ShowMenu(false);
    }
    public void Menu()
    {
        Player.instance.CanNotAction();
        Time.timeScale = 0f;
        ui.ShowMenu(true);
    }
    public void StartBack()
    {
        uis.Help(false);
    }
    public void Help()
    {
        uis.Help(true);   
    }
    IEnumerator StartGameTransition()
    {
        endTransition.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
    IEnumerator BackToStartTransition()
    {
        endTransition.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}
