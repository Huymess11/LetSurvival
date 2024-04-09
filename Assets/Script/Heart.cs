using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
     //singleton
     public static Heart instance;
     //
    public float TotalHeart;
    public float CurHeart {get; private set; }
    public Player pl;
    private Animator ani;
    private Rigidbody2D rb;
    private UI ui;

    private void Awake()
    {
        Heart.instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        ani = GetComponent<Animator>();
        ui = FindObjectOfType<UI>();
        pl = FindObjectOfType<Player>();
        CurHeart = TotalHeart;
    }
    private void Update()
    {
        ui.SetHealth(CurHeart+"/"+TotalHeart);
    }

    // Update is called once per frame
    public void TakeDame(float Dame)
    {
        CurHeart = Mathf.Clamp(CurHeart - Dame, 0, TotalHeart);
        if(CurHeart <= 0) 
        {
            pl.GameOver();
            Destroy(gameObject);
            ani.SetTrigger("dead");
            ui.Showisgameover(true);
            Time.timeScale = 0f;
        }
    }
    public float GetPlayerHeart()
    {
        return CurHeart;
    }
    public void PlusTotalHeart()
    {
        TotalHeart += 15;
        CurHeart = TotalHeart;
    }
}
