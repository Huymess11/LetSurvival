using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( 0,speed * Time.deltaTime,0);
        Destroy(gameObject, 3);
    }
}
