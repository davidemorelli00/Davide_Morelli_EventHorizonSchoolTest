using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int width,height;
    public GameObject tile;
    Tile[,] Tiles;
    public GameObject[] type;
    public GameObject[,] allTiles;

    [SerializeField]
    int gameTime;

    [SerializeField]
    int elementSize;
    public int points;

    public Text ScoreText;
    public Text MultiplyText;
    public Text TimeLeftText;

    [Header("End Game Menu")]
    public GameObject endGameMenu;
    public Text yourScore;
    public Text HighestScore;

    [Header("Theme")]
    int themeSelector;
    public GameObject[] Pirate;
    public GameObject[] Geometric;
    public GameObject[] fruits;
    public GameObject[] bgTile;
    public AudioClip[] clips;
    AudioSource audioSource;

    [Header("Multiply")]
    public int MultiplyValue;
    [SerializeField]
    bool multiplyBool;

    public AudioSource matchSound;

    
    void Start()
    {
        ScoreText.text = "Score: " + points.ToString();

        height = PlayerPrefs.GetInt("height");
        width = PlayerPrefs.GetInt("width");
        gameTime = PlayerPrefs.GetInt("time");
        elementSize = PlayerPrefs.GetInt("figures");

        TimeLeftText.text = "Time Left: " + gameTime.ToString();

        selectTheme(PlayerPrefs.GetInt("ThemeValue"));
        Tiles = new Tile[width, height];
        allTiles = new GameObject[width, height];
        initializeBoard();
        MultiplyText.text = "x1";
        FindObjectOfType<CameraReposition>().SetColor();
        InvokeRepeating("TimeLeft", 1, 1);
    }
    void selectTheme(int n)
    {
        type = new GameObject[elementSize];
        Debug.Log(PlayerPrefs.GetInt("ThemeValue"));
        audioSource = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        switch (n)
        {
            case 0:
                for (int i = 0; i < elementSize; i++)
                {
                    type[i] = Pirate[i];
                }
                tile = bgTile[n];
                Debug.Log("w I PIRATI");
                audioSource.clip = clips[n];


                break;
            case 1:
                for (int i = 0; i < elementSize; i++)
                {

                    type[i] = Geometric[i];
                    Debug.Log("w I la geometria");
                }
                tile = bgTile[n];
                audioSource.clip = clips[n];
                break;
            case 2:
                for (int i = 0; i < elementSize; i++)
                    {
                        type[i] = fruits[i];
                    }
                tile = bgTile[n];
                audioSource.clip = clips[n];
                break;

        }
        Color color = tile.GetComponent<SpriteRenderer>().color;
        Camera.main.backgroundColor = color;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void TimeLeft()
    {
        gameTime = gameTime - 1;
        TimeLeftText.text = "Time Left: " + gameTime.ToString();
        if(gameTime <= 0)
        {
            GameEnd();
            CancelInvoke("TimeLeft");
        }
    }
    void GameEnd()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Destroy(allTiles[x, y].GetComponent<Collider2D>());
            }
        }
        if (points > PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("Level"))){
            PlayerPrefs.SetInt("Score" + PlayerPrefs.GetInt("Level"), points);
        }
        endGameMenu.SetActive(true);
        ScoreText.gameObject.SetActive(false);
        yourScore.text = points.ToString();
        HighestScore.text = PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("Level")).ToString();

    }
    void initializeBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 Pos = new Vector2(x, y);
                GameObject BgTile= Instantiate(tile, Pos, Quaternion.identity);
                BgTile.transform.parent = this.transform;

                int randomNum = Random.Range(0, type.Length);
                while(initializeMatchFound(x, y, type[randomNum]))
                {
                   randomNum = Random.Range(0, type.Length);
                }

                GameObject randomTile = Instantiate(type[randomNum], Pos, Quaternion.identity);
                allTiles[x, y] = randomTile;
            }
        }
    }


    bool initializeMatchFound(int column,int row, GameObject tile)
    {
        if (column > 1 && row > 1)
        {
            tileTouch tileToCheck = tile.GetComponent<tileTouch>();

            tileTouch columnCheck1 = allTiles[column - 1, row].GetComponent<tileTouch>();
            tileTouch columnCheck2 = allTiles[column - 2, row].GetComponent<tileTouch>();

            tileTouch rowCheck1 = allTiles[column, row - 1].GetComponent<tileTouch>();
            tileTouch rowCheck2 = allTiles[column, row - 2].GetComponent<tileTouch>();

            if (columnCheck1.TileValue == tileToCheck.TileValue && columnCheck2.TileValue == tileToCheck.TileValue)
            {
                return true;
            }
            if(rowCheck1.TileValue == tileToCheck.TileValue && rowCheck2.TileValue == tileToCheck.TileValue)
            {
                return true;
            }else if (column <= 1|| row <= 1)
            {
                if(row > 1)
                {
                    if(rowCheck1.TileValue == tileToCheck.TileValue && rowCheck2.TileValue == tileToCheck.TileValue)
                    {
                        return true;

                    }
                }
                if (column > 1)
                {
                    if (columnCheck1.TileValue == tileToCheck.TileValue && columnCheck2.TileValue == tileToCheck.TileValue)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }



    void MatchDestroyer(int column, int row)
    {
        tileTouch tileToCheck = allTiles[column, row].GetComponent<tileTouch>();
        if (tileToCheck.match)
        {
            Destroy(tileToCheck.gameObject);
            matchSound.Play();
            allTiles[column, row] = null;
        }
    }
    public void CheckMatchToDestroy()
    {
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(allTiles[x,y]!= null)
                {
                    MatchDestroyer(x, y);
                }
            }
        }
        StartCoroutine(movePositionsOnDestroy());
    }
    IEnumerator movePositionsOnDestroy()
    {
        int count = 0;
        bool bomb = false;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(allTiles[x,y] == null)
                {
                    count++;
                    Vector2 Pos =new Vector2(x, y);
                    int randomNum = Random.Range(0, type.Length);
                    if(count >= 4)
                    {
                        Debug.Log("bomba!");
                        bomb = true;
                        count = 0;
                    }
                        while (initializeMatchFound(x, y, type[randomNum]))
                        {
                            randomNum = Random.Range(0, type.Length);
                        }
                        GameObject randomTile = Instantiate(type[randomNum], Pos, Quaternion.identity);
                        allTiles[x, y] = randomTile;
                    
                }
                /*else if (count > 0)
                {
                    allTiles[x, y].GetComponent<tileTouch>().row -= count;
                }*/
            }
        }
        yield return new WaitForSeconds(0.4f);
        CheckMatchToDestroy();
    }


     IEnumerator rawColumnDestroy(int x,int y)
    {
        for (int i = 0; i < width; i++)
        {
            if(allTiles[i,y] != null)
            {
                Destroy(allTiles[i, y].GetComponent<GameObject>());
                allTiles[i, y] = null;
                allTiles[i, y].GetComponent<tileTouch>().match = true;
                MatchDestroyer(i,y);

            }
        }


        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                Vector2 Pos = new Vector2(x, y);
                int randomNum = Random.Range(0, type.Length);
                   /* while (initializeMatchFound(x, y, type[randomNum]))
                    {
                        randomNum = Random.Range(0, type.Length);
                    }*/
                    GameObject randomTile = Instantiate(type[randomNum], Pos, Quaternion.identity);
                    allTiles[x, y] = randomTile;
                
                
            }
        }
        yield return new WaitForSeconds(0.4f);
        CheckMatchToDestroy();
    }
    public void multiply()
    {
        MultiplyValue += 2;
        if (MultiplyValue >= 5)
        {
            MultiplyText.text = "x" + Mathf.RoundToInt(MultiplyValue / 5).ToString();
        }
        StopCoroutine(Multiply());
        StartCoroutine(Multiply());
        
        

    }
    IEnumerator Multiply()
    {
        multiplyBool = true;
        int oldMultiply = MultiplyValue;
        yield return new WaitForSeconds(2f);
        if(oldMultiply == MultiplyValue)
        {
            multiplyBool = false;
            MultiplyValue = 0;
            MultiplyText.text = "x1";
        }

    }
}
