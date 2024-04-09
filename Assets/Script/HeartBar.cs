using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Heart heart;
    public Image totalHeart;
    public Image curheart;
    void Awake()
    {
        heart = FindObjectOfType<Heart>();
    }

    // Update is called once per frame
    void Update()
    {
        curheart.fillAmount = heart.CurHeart / heart.TotalHeart;
    }
}
