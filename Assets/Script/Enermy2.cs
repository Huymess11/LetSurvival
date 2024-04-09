using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy2 : MonoBehaviour
{
   //Singleton
   public static Enermy2 instance;
   //
    public GameObject target;
    public GameManager gm;
    private Transform player;
    public float speed;
    private int health = 150;
    private Animator ani;
    public Player pl;
    AudioManager audio;

    private void Awake()
    {
        Enermy2.instance = this;
    }
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        target = GameObject.FindWithTag("Player");
        gm = FindObjectOfType<GameManager>();
        player = target.transform;
        ani = GetComponent<Animator>();
        pl = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        transform.up = direction;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    public void SetHealth()
    {
        health += 15;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Playerbullet"))
        {
            audio.PlaySFX(audio.hit);
            health -= gm.GetBulletDamage();
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                pl.UpScore();
                speed = 0;
                ani.SetTrigger("dead");
                
            }
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
        if (collision.CompareTag("mine"))
        {
            audio.PlaySFX(audio.boom);
            health -= 100;
            if (health <= 0)
            {
                pl.UpScore();
                speed = 0;
                ani.SetTrigger("dead");

            }
        }
    }
}
