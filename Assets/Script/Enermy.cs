using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enermy : MonoBehaviour
{
    // Start is called before the first frame update
    //singleton
    public static Enermy instance;
    //
    public GameObject target;
    private Transform player;
    public GameManager gm;
    public float speed;
    private float health;
    private Animator ani;
    private Player pl;
    AudioManager audio;
    private bool isDead;


    private void Awake()
    {
        Enermy.instance = this;
    }
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        target = GameObject.FindWithTag("Player");
        player = target.transform;
        ani = GetComponent<Animator>();
        pl = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
        health = GameManager.instance.GetEnenryHealth();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            return;
        }
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        transform.up = direction;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (health <= 0)
        {
            pl.UpScore();
            speed = 0;
            ani.SetTrigger("dead");
            isDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Playerbullet"))
        {
            audio.PlaySFX(audio.hit);
            health -= gm.GetBulletDamage();
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            ani.SetBool("attack", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ani.SetBool("attack", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("explore"))
        {
            
            health -= 100;
        }
    }
}
