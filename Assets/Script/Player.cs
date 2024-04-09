using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro.Examples;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    //Singleton
    public static Player instance;
    //
    public float speed;
    private bool canAction;
    public Transform bulletpos;
    public Transform[] shotGunPos;
    public GameObject bullet;
    public Heart heart;
    public float score;
    private float highestScore;
    private float scoreToUpgrade = 20;
    private float scoreToBoss = 50;
    public UI ui;
    AudioManager audio;
    public GameObject mine;
    private float TotalMine = 20;
    public bool isGameOver;
    public float coolDownMineTime = 10;
    public Image MineImg;
    private bool isCoolDownMine = false;
    private float isCoolDownShotGunTime = 3;
    public Image ShotGunImg;
    public bool isCoolDownShotGun = false;
    private Rigidbody2D rb;
    public float dashPower;
    public GameObject theBoss;
    private void Awake()
    {
        Player.instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canAction = true;
        heart = FindObjectOfType<Heart>();
        ui = FindObjectOfType<UI>();
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        PlayerPrefs.GetFloat("HighestScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(score > PlayerPrefs.GetFloat("HighestScore", 0)) 
        {
            PlayerPrefs.SetFloat("HighestScore", score);

        }
        ui.SetHighestScore("Highest Score: " + PlayerPrefs.GetFloat("HighestScore", 0));
        if (!canAction) return;
        if (isGameOver) { return; }
        float HMove = Input.GetAxisRaw("Horizontal");
        float VMove = Input.GetAxisRaw("Vertical");
        transform.position = new Vector2(transform.position.x + HMove * speed * Time.deltaTime, transform.position.y + VMove * speed * Time.deltaTime);
        Shoot();
        if(score == scoreToUpgrade)
        {
            GameManager.instance.ShowUpgradePanel();
            GameManager.instance.SetEnermyHealth();
            scoreToUpgrade += 20;
            canAction = false;
        }
        ui.SetScore(score.ToString());
        ui.SetScoreGameOver("Score: " + score);
        Mine();
        ShotGun();
        if(score == scoreToBoss)
        {
            Instantiate(theBoss,Vector3.zero,Quaternion.identity);
            GameManager.instance.SetBossHealth();
            scoreToBoss += 50;
            Debug.Log("scoreToBoss");
        }

    }
    public void Mine()
    {
        if( Input.GetKeyDown(KeyCode.Space) && isCoolDownMine == false)
        {
            MineImg.fillAmount = 1;
            isCoolDownMine = true;
            Instantiate(mine,gameObject.transform.position, Quaternion.identity);
        }
        if (isCoolDownMine)
        {
            MineImg.fillAmount -= 1/coolDownMineTime*Time.deltaTime;
            if(MineImg.fillAmount <= 0)
            {
                MineImg.fillAmount = 0;
                isCoolDownMine = false;
            }
        }
    }
    public void ShotGun()
    {
        if(Input.GetMouseButtonDown(1) && isCoolDownShotGun == false){
            ShotGunImg.fillAmount = 1;
            isCoolDownShotGun = true;
            for(int i = 0; i< shotGunPos.Length;i++) 
            {
                audio.PlaySFX(audio.shoot);
                Instantiate(bullet, shotGunPos[i].position, shotGunPos[i].rotation);
            }
        }
        if(isCoolDownShotGun) {
            ShotGunImg.fillAmount -= 1 / isCoolDownShotGunTime * Time.deltaTime;
            if(ShotGunImg.fillAmount <= 0)
            {
                ShotGunImg.fillAmount = 0;
                isCoolDownShotGun = false;
            }
        }
    }

    public void Shoot()
    {
        Vector2 shootPos = new Vector2(bulletpos.position.x, bulletpos.position.y);
        if (Input.GetMouseButtonDown(0))
        {
            audio.PlaySFX(audio.shoot);
            Instantiate(bullet, shootPos, transform.rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hitbox"))
        {
            audio.PlaySFX(audio.getAttack);
            heart.TakeDame(3);
        }
        if (collision.CompareTag("bossBullet"))
        {
            audio.PlaySFX(audio.getAttack);
            heart.TakeDame(10);
        }
        if (collision.CompareTag("sand"))
        {
            speed = 3;
        }
        if (collision.CompareTag("ice"))
        {
            speed = 8;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("sand"))
        {
            speed = 5;
        }
        if (collision.CompareTag("ice"))
        {
            speed = 5;
        }
    }
    public void UpScore()
    {
        score++;
    }
    public void GameOver()
    {
        isGameOver = true;
    }
    public void CanAction()
    {
        canAction = true;
    }
    public void CanNotAction()
    {
        canAction = false;
    }
}