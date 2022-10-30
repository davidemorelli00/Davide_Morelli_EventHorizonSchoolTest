using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CameraReposition : MonoBehaviour
{
    GameManager tilesManager;
    public GameObject[] UIimg;

    void Start()
    {
       tilesManager = FindObjectOfType<GameManager>();
       CameraPos(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"));       
    }
    
    void CameraPos(float x, float y)
    {
        Vector3 pos = new Vector3(Mathf.Round(x / 2), Mathf.Round(y / 2), -10);
        transform.position = pos;
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float aspectRatio;
        if(screenHeight > screenWidth)
        {
            aspectRatio = Screen.height / Screen.width;
        }
        else
        {
            aspectRatio = Screen.width/screenHeight;
        }
        Debug.Log(aspectRatio);

        if(PlayerPrefs.GetInt("width") >= PlayerPrefs.GetInt("height"))
        {
            Camera.main.orthographicSize = PlayerPrefs.GetInt("width") + 3 / aspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = PlayerPrefs.GetInt("height") + 3 /aspectRatio;
        }
        

        
    }
    public void SetColor()
    {
        foreach (GameObject img in UIimg)
        {
            Image ImageColor = img.GetComponent<Image>();
            ImageColor.color = new Color(Camera.main.backgroundColor.r - 0.10f, Camera.main.backgroundColor.g - 0.10f, Camera.main.backgroundColor.b - 0.10f);
        }
    }
}
