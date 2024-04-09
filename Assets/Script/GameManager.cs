using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    //
    public GameObject[] zb;
    public Transform tomb1;
    public Transform tomb2;
    public Transform tomb3;
    public Transform tomb4;
    public GameObject upgradePanel;
    public float timespawn;
    private float m_timespawn;
    public int bulletDamage = 25;
    private UI ui;
    private Heart heart;
    public float enenrmyHealth = 100;
    public float bossHealth;
    public Slider soundSlider;
    private void Awake()
    {
        GameManager.instance = this;
    }
    void Start()
    {
        m_timespawn = timespawn;
        heart = FindObjectOfType<Heart>();
        ui = FindObjectOfType<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        ui.SetATK("ATK: "+bulletDamage);
        if (zb[0] && zb[1])
        {
            m_timespawn -= Time.deltaTime;
            SpawnZombie();
        }
        AudioManager.instance.ChangePlaySFX(soundSlider.value);
    }

    private void SpawnZombie()
    {
        if(m_timespawn <= 0)
        {
            Vector2 spawnPos = new Vector2(tomb1.position.x, tomb1.position.y);
            Vector2 spawnPos1 = new Vector2(tomb2.position.x, tomb2.position.y);
            Vector2 spawnPos2 = new Vector2(tomb3.position.x, tomb3.position.y);
            Vector2 spawnPos3 = new Vector2(tomb4.position.x, tomb4.position.y);
            Instantiate(zb[0], spawnPos,Quaternion.identity);
            Instantiate(zb[1], spawnPos1, Quaternion.identity);
            Instantiate(zb[0], spawnPos2, Quaternion.identity);
            Instantiate(zb[1], spawnPos3, Quaternion.identity);
            m_timespawn = timespawn;
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public float GetEnenryHealth()
    {
        return enenrmyHealth;
    }
    public void SetEnermyHealth()
    {
        enenrmyHealth += 15;
    }
    public float GetBossHealth()
    {
        return bossHealth;
    }
    public void SetBossHealth()
    {
        bossHealth += 500;
    }
    public int GetBulletDamage()
    {
        return bulletDamage;
    }
    public void SetBulletDamage()
    {
        Player.instance.CanAction();
        bulletDamage += 10;
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }
    public void SetHealth()
    {
        Player.instance.CanAction();
        heart.PlusTotalHeart();
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }
    public void ShowUpgradePanel()
    {
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);
    }
}
