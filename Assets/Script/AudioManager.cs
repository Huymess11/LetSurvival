using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{   
    //singleton
    public static AudioManager instance;    
    //
    // Start is called before the first frame update
    [SerializeField] AudioSource SFXsource;
    public AudioClip shoot;
    public AudioClip boom;
    public AudioClip hit;
    public AudioClip getAttack;
    private void Awake()
    {
       AudioManager.instance = this;
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }
    public void ChangePlaySFX(float value)
    {
        SFXsource.volume = value;
    }
}
