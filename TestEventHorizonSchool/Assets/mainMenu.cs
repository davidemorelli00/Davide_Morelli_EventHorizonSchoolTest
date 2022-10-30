using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{


    public GameObject Menu;
    bool MenuBool = true;
    public Text themeText;
    [Header("LevelSettings")]
    public GameObject levelMenu;
    bool levelMenuBool;
    public Text LevelSelected;
    public Text HighestScore;

    [Header("CustomMode")]
    public GameObject CustomModeMenu;
    bool CustomModeBool;
    public Text widthText;
    public Text heightText;
    public Text timeText;
    public Text figureText;

    [Header("DeleteAllDatas")]
    public GameObject deleteDataMenu;
    bool deleteMenu;
    // Start is called before the first frame update
    private void Update()
    {
        // setUpText();

    }
    private void Start()
    {
        themeSelect();
        //PlayerPrefs.SetInt("Score1", 10);
    }
    public void menu()
    {
        MenuBool = !MenuBool;
        if (MenuBool)
        {
            Menu.SetActive(true);
        }
        if (MenuBool == false)
        {
            Menu.SetActive(false);
            setUpText();
        }
        
    }

    public void CustomGameMode()
    {
        CustomModeBool = !CustomModeBool;
        if (CustomModeBool)
        {
            CustomModeMenu.SetActive(true);
            menu();
        }
        if (CustomModeBool == false)
        {
            CustomModeMenu.SetActive(false);
            menu();
        }
        PlayerPrefs.SetInt("Level", 0);
    }
    public void LevelMenu()
    {
        levelMenuBool = !levelMenuBool;
        if (levelMenuBool)
        {
            levelMenu.SetActive(true);
            menu();
        }
        if (levelMenuBool == false)
        {
            levelMenu.SetActive(false);
            menu();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void themeSelect()
    {
        PlayerPrefs.SetInt("ThemeValue", PlayerPrefs.GetInt("ThemeValue") + 1);

        if (PlayerPrefs.GetInt("ThemeValue") > 2)
        {
            PlayerPrefs.SetInt("ThemeValue",0);
        }
        switch (PlayerPrefs.GetInt("ThemeValue"))
        {
            case 0:
                themeText.text = "Pirate";
                break;
            case 1:
                themeText.text = "Geometry";
                break;
            case 2:
                themeText.text = "Fruit";
                break;
        }
        
    }


    public void menuDeleteData()
    {
        deleteMenu = !deleteMenu;
        if (deleteMenu)
        {
            deleteDataMenu.SetActive(true);
            menu();
        }
        if (deleteMenu == false)
        {
            deleteDataMenu.SetActive(false);
            menu();
        }

    }
    
    public void deleteData()
    {
        PlayerPrefs.DeleteAll();
    }



    //_____________________________________________LEVELS SETTINGS_____________________________________________\\

    void levelTxtSet()
    {
        HighestScore.text = "Highest Score: " + PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("Level"));
        LevelSelected.text = "Start Level " + PlayerPrefs.GetInt("Level");
    }
    public void Level1()
    {
        PlayerPrefs.SetInt("height", 5);
        PlayerPrefs.SetInt("width", 5);
        PlayerPrefs.SetInt("figures", 3);
        PlayerPrefs.SetInt("time", 75);

        PlayerPrefs.SetInt("Level", 1);
        levelTxtSet();

    }
    public void Level2()
    {
        PlayerPrefs.SetInt("height", 5);
        PlayerPrefs.SetInt("width", 5);
        PlayerPrefs.SetInt("figures", 3);
        PlayerPrefs.SetInt("time", 60);

        PlayerPrefs.SetInt("Level", 2);
        levelTxtSet();
    }
    public void Level3()
    {
        PlayerPrefs.SetInt("height", 5);
        PlayerPrefs.SetInt("width", 5);
        PlayerPrefs.SetInt("figures",3);
        PlayerPrefs.SetInt("time", 45);

        PlayerPrefs.SetInt("Level", 3);
        levelTxtSet();
    }
    public void Level4()
    {
        PlayerPrefs.SetInt("height", 9);
        PlayerPrefs.SetInt("width", 9);
        PlayerPrefs.SetInt("figures", 4);
        PlayerPrefs.SetInt("time", 70);

        PlayerPrefs.SetInt("Level", 4);
        levelTxtSet();
    }
    public void Level5()
    {
        PlayerPrefs.SetInt("height", 9);
        PlayerPrefs.SetInt("width", 9);
        PlayerPrefs.SetInt("figures", 4);
        PlayerPrefs.SetInt("time", 55);

        PlayerPrefs.SetInt("Level", 5);
        levelTxtSet();
    }
    public void Level6()
    {
        PlayerPrefs.SetInt("height", 9);
        PlayerPrefs.SetInt("width", 9);
        PlayerPrefs.SetInt("figures", 5);
        PlayerPrefs.SetInt("time", 40);

        PlayerPrefs.SetInt("Level", 6);
        levelTxtSet();
    }
    public void Level7()
    {
        PlayerPrefs.SetInt("height", 13);
        PlayerPrefs.SetInt("width", 13);
        PlayerPrefs.SetInt("figures", 5);
        PlayerPrefs.SetInt("time", 35);

        PlayerPrefs.SetInt("Level", 7);
        levelTxtSet();
    }
    public void Level8()
    {
        PlayerPrefs.SetInt("height", 13);
        PlayerPrefs.SetInt("width", 13);
        PlayerPrefs.SetInt("figures", 6);
        PlayerPrefs.SetInt("time", 30);

        PlayerPrefs.SetInt("Level", 8);
        levelTxtSet();
    }
    public void Level9()
    {
        PlayerPrefs.SetInt("height", 17);
        PlayerPrefs.SetInt("width", 17);
        PlayerPrefs.SetInt("figures", 6);
        PlayerPrefs.SetInt("time", 60);

        PlayerPrefs.SetInt("Level", 9);
        levelTxtSet();
    }




    //_____________________________________________CUSTOM MODE SETTINGS_____________________________________________\\
    void setUpText()
    {
        Debug.Log(PlayerPrefs.GetInt("height"));
        heightText.text = PlayerPrefs.GetInt("height").ToString();
        widthText.text = PlayerPrefs.GetInt("width").ToString();
        timeText.text = PlayerPrefs.GetInt("time").ToString();
        figureText.text = PlayerPrefs.GetInt("figures").ToString();


        if (PlayerPrefs.GetInt("height") == 0|| PlayerPrefs.GetInt("width")== 0|| PlayerPrefs.GetInt("time") == 0|| PlayerPrefs.GetInt("figures") == 0)
        {
            PlayerPrefs.SetInt("height", 5);
            PlayerPrefs.SetInt("width", 5);
            PlayerPrefs.SetInt("time", 30);
            PlayerPrefs.SetInt("figures", 5);
        }
    }
    public void addHeight()
    {
        if (PlayerPrefs.GetInt("height") < 20)
        {
            PlayerPrefs.SetInt("height", PlayerPrefs.GetInt("height") + 1);
        }
        heightText.text = PlayerPrefs.GetInt("height").ToString();
    }
    public void subtractHeight()
    {
        if (PlayerPrefs.GetInt("height") > 5)
        {
            PlayerPrefs.SetInt("height", PlayerPrefs.GetInt("height") - 1);
        }
        heightText.text = PlayerPrefs.GetInt("height").ToString();
    }


    public void addWidth()
    {
        if (PlayerPrefs.GetInt("width") <20)
        {
            PlayerPrefs.SetInt("width", PlayerPrefs.GetInt("width") + 1);
        }
        widthText.text = PlayerPrefs.GetInt("width").ToString();
    }
    public void subtractWidth()
    {
        if (PlayerPrefs.GetInt("width") > 5)
        {
            PlayerPrefs.SetInt("width", PlayerPrefs.GetInt("width") - 1);
        }
        widthText.text = PlayerPrefs.GetInt("width").ToString();
    }



    public void addTime()
    {
        if (PlayerPrefs.GetInt("time") < 600)
        {
            PlayerPrefs.SetInt("time", PlayerPrefs.GetInt("time") + 15);
        }
        timeText.text = PlayerPrefs.GetInt("time").ToString();
    }
    public void subtractTime()
    {
        if (PlayerPrefs.GetInt("time") > 30)
        {
            PlayerPrefs.SetInt("time", PlayerPrefs.GetInt("time") - 15);
        }
        timeText.text = PlayerPrefs.GetInt("time").ToString();
    }


    public void addFigures()
    {
        if (PlayerPrefs.GetInt("figures") <6)
        {
            PlayerPrefs.SetInt("figures", PlayerPrefs.GetInt("figures") +1);
        }
        figureText.text = PlayerPrefs.GetInt("figures").ToString();

    }
    public void subtractFigures()
    {
        if (PlayerPrefs.GetInt("figures") > 3)
        {
            PlayerPrefs.SetInt("figures", PlayerPrefs.GetInt("figures") - 1);
        }
        figureText.text = PlayerPrefs.GetInt("figures").ToString();

    }
}
