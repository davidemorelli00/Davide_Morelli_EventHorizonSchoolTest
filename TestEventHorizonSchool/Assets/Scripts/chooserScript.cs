using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooserScript : MonoBehaviour
{
    public int theme;
    
    void Start()
    {
        PlayerPrefs.SetInt("ThemeValue", theme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
