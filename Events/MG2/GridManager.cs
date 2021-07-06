using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject[,] Grid;
    public GameObject[,] colorGrid;
    public GameObject[] sprites = new GameObject[4];
    public int vert, horiz, columns, rows;
    public GameObject sprite;
    int randNum;
    public bool canMoveEvery;

    // Start is called before the first frame update
    void Start()
    {
        canMoveEvery = true;
        vert = (int)Camera.main.orthographicSize;
        horiz = vert * (Screen.width / Screen.height);
        //columns = horiz * 2;
        //rows = vert * 2;
        columns = 10;
        rows = 10;
        Grid = new GameObject[columns, rows];
        colorGrid = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                //Grid[i, j] = Random.Range(0, 4);
                Grid[i, j] = SpawnTile(i, j);
                colorGrid[i, j] = null;
            }
        }

        loadTiles();
    }


    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            updateGrid();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            colorGrid[Random.Range(0, 10), Random.Range(0, 10)].GetComponent<ColorUnit>().destroy();
            checkGrid();
        }*/
        checkEvery();
    }

    private GameObject SpawnTile(int x, int y)
    {
        //GameObject g = new GameObject("x: " + x  + " y: " + y);
        GameObject g = Instantiate(sprite, new Vector3(x - (horiz - 0.5f), y - (vert - 0.5f)), transform.rotation);
        g.GetComponent<GridPoint>().x = x;
        g.GetComponent<GridPoint>().y = y;
        return g;
        //g.transform.position = new Vector3(x - (horiz - 0.5f), y - (vert - 0.5f));
    }

    public void loadTiles()
    {
        for (int i = 0; i < rows; i++)
        {
            GameObject g;
            if (colorGrid[1, i] != null)
            {
                randNum = Random.Range(0, 2);
                if (randNum == 0)
                {
                    g = Instantiate(colorGrid[1, i], Grid[0, i].transform.position, transform.rotation);
                    colorGrid[0, i] = g;
                    g.GetComponent<ColorUnit>().x = 0;
                    g.GetComponent<ColorUnit>().y = i;
                    continue;
                }

            }
            randNum = Random.Range(0, 4);
            g = Instantiate(sprites[randNum], Grid[0, i].transform.position, transform.rotation);
            colorGrid[0,i] = g;
            g.GetComponent<ColorUnit>().x = 0;
            g.GetComponent<ColorUnit>().y = i;
            Debug.Log(colorGrid[0, i].GetComponent<ColorUnit>().color);
        }
    }

    public void updateGrid()
    {
        ColorUnit cur;
        for (int i = columns - 1; i >= 0; i--)
        {
            for (int j = rows - 1; j >= 0; j--)
            {
                if (colorGrid[i, j] != null)
                {
                    cur = colorGrid[i, j].GetComponent<ColorUnit>();

                    if (cur.x >= 9)
                    {
                        GameManager.instance.returnLoss();
                        break;
                    }
                    cur.x += 1;
                    //cur.y++;
                    colorGrid[cur.x, cur.y] = colorGrid[i, j];
                    colorGrid[i, j] = null;
                }
            }
        }
        loadTiles();
    }

    public void checkGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (colorGrid[i, j] != null) colorGrid[i, j].GetComponent<ColorUnit>().checkMoveDown();
            }
        }
    }

    public void checkEvery()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (colorGrid[i, j] != null && colorGrid[i, j].GetComponent<ColorUnit>().canMove == false)
                {
                    canMoveEvery = false;
                    return;
                }
            }
        }

        canMoveEvery = true;
    }    
}
