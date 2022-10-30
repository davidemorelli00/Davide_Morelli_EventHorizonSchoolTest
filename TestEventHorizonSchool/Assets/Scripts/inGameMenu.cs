using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameMenu : MonoBehaviour
{
    public GameObject goToMenu;
    bool goToMenuBool;

    public GameObject RestartMenu;
    bool restartMenuBool;

    
    

    public void GoToMenu()
    {
        goToMenuBool = !goToMenuBool;
        if (goToMenuBool)
        {
            goToMenu.SetActive(true);
        }
        if(goToMenuBool == false)
        {
            goToMenu.SetActive(false);
        }
    }
    public void menuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        restartMenuBool = !restartMenuBool;
        if (restartMenuBool)
        {
            RestartMenu.SetActive(true);
        }
        if (restartMenuBool == false)
        {
            RestartMenu.SetActive(false);
        }
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

}
