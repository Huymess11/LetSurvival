using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    //Singleton
    public static Boss instance;
    //
    // Start is called before the first frame update
    private float health;
    AudioManager audio;
    private GameManager gm;
    public GameObject bullet;
    public Transform bulletPos;
    public Transform[] bulletPos1;
    private float curHearlth;
    public GameObject bossBoom;
    public Slider bossHealthBar;

    private void Awake()
    {
        Boss.instance = this;
    }
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        gm = FindObjectOfType<GameManager>();
        health = GameManager.instance.GetBossHealth();
        curHearlth = health;
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthBar.value = curHearlth / health;
        if (curHearlth <= 0)
        {
            audio.PlaySFX(audio.boom);
            Player.instance.UpScore();
            Destroy(gameObject);

        }
    }

    public void Attack()
    {
        Instantiate(bullet, bulletPos.position,transform.rotation);
    }
    public void Attack1()
    {
        for(int i = 0; i < bulletPos1.Length; i++)
        {
            Instantiate(bullet, bulletPos1[i].position, bulletPos1[i].rotation);
        }

    }
    public void Attack2()
    {
        for (int i = 0; i < bulletPos1.Length; i++)
        {
            Instantiate(bullet, bulletPos1[i].position, transform.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Playerbullet"))
        {
            audio.PlaySFX(audio.hit);
            curHearlth -= gm.GetBulletDamage();
            Destroy(collision.gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("explore"))
        { 
            curHearlth -= 100;

        }
    }
}
