using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject help;
    public void Help(bool value)
    {
        help.SetActive(value);
    }
}
