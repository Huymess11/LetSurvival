using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private AudioManager audio;
    public GameObject boomboom;
    private void Awake()
    {
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        
    }
    // Start is called before the first frame update
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enermy"))
        {
            audio.PlaySFX(audio.boom);
            Destroy(gameObject);
            Instantiate(boomboom,transform.position, Quaternion.identity);  
            
        }
    }
}
