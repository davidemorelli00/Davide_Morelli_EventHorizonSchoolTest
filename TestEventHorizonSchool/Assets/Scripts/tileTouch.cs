using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileTouch : MonoBehaviour
{
    public int TileValue;
    Vector2 touchPos;
    Vector2 releasePos;

    float touchAngle;
    public bool match;
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    GameManager tileManager;
    GameObject tileToSwitch;
    int previousCol;
    int previousRow;

    Vector2 tempPos;


    // Start is called before the first frame update
    void Start()
    {

        tileManager = FindObjectOfType<GameManager>();
     
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        previousCol = column;
        previousRow = row;
    }


    // Update is called once per frame
    private void Update()
    {
        MatchesFinder();
        if (match)
        {
            SpriteRenderer Sprite = GetComponent<SpriteRenderer>();
            Sprite.color = new Color(0, 0, 0, .2f);
        }
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x)> 0.1f)
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, 0.4f);
        }
        else
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = tempPos;
            tileManager.allTiles[column,row] = gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, 0.4f);
        }
        else
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = tempPos;
            tileManager.allTiles[column, row] = gameObject;
        }
    }
    private void OnMouseDown()
    {
        touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    }
    private void OnMouseUp()
    {
        releasePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Angle();
    }
    void Angle()
    {
        touchAngle = Mathf.Atan2(releasePos.y - touchPos.y, releasePos.x - touchPos.x) * 180 / Mathf.PI;
        Debug.Log(touchAngle);
        switchPiece();

    }
    void switchPiece()
    {
        //________________DESTRA___________________________________\\
        if (touchAngle > -45 && touchAngle <= 45)
        {

            tileToSwitch = tileManager.allTiles[column + 1, row];
            tileToSwitch.GetComponent<tileTouch>().column -= 1;
            column += 1;
            Debug.Log("w"); 
        }
        //________________SU________________________________________\\
        else if (touchAngle > 45 && touchAngle <= 135)
        {

            tileToSwitch = tileManager.allTiles[column, row + 1];
            tileToSwitch.GetComponent<tileTouch>().row -= 1;
            row += 1;
          

        }
        //_________________SINISTRA___________________________________\\
        else if (touchAngle > 135 || touchAngle <= -135)
        {

            tileToSwitch = tileManager.allTiles[column - 1, row];
            tileToSwitch.GetComponent<tileTouch>().column += 1;
            column -= 1;
        }//_________________GIU___________________________________________\\
        else if (touchAngle < 45 && touchAngle >= -135)
        {
            tileToSwitch = tileManager.allTiles[column, row-1];
            tileToSwitch.GetComponent<tileTouch>().row += 1;
            row -= 1;
        }
        StartCoroutine("ValueBack");
    }

    void MatchesFinder()
    {
        if(column > 0 &&  column < tileManager.width -1)
        {
            tileTouch leftTile1 = tileManager.allTiles[column - 1,row].GetComponent<tileTouch>();
            tileTouch rightTile1 = tileManager.allTiles[column + 1, row].GetComponent<tileTouch>();
            if(leftTile1.TileValue == TileValue && TileValue == rightTile1.TileValue)
            {
                leftTile1.match = true;
                rightTile1.match = true;
                match = true;
            }
        }
        if (row > 0 && row < tileManager.height - 1)
        {
            tileTouch upTile1 = tileManager.allTiles[column, row + 1].GetComponent<tileTouch>();
            tileTouch downTile1 = tileManager.allTiles[column, row - 1].GetComponent<tileTouch>();
            if (upTile1.TileValue == TileValue && TileValue == downTile1.TileValue)
            {
                upTile1.match = true;
                downTile1.match = true;
                match = true;
            }
        }
    }
    public IEnumerator ValueBack()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        tileTouch CheckTile = tileToSwitch.GetComponent<tileTouch>();
        if (tileToSwitch != null)
        {
            if(match == false && CheckTile.match ==false)
            {
                CheckTile.row = row;
                CheckTile.column = column;

                row = previousRow;
                column = previousCol;
            }
            else
            {
                tileManager.CheckMatchToDestroy();
            }
            tileToSwitch = null;
        }

    }
    private void OnDestroy()
    {
        tileManager.points += 1;
        tileManager.multiply();
        tileManager.ScoreText.text = "Score: " + tileManager.points.ToString();
        if (tileManager.MultiplyValue > 5)
        {
            tileManager.points += Mathf.RoundToInt(1 * (tileManager.MultiplyValue / 5));
        }

    }

}
