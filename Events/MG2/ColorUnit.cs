using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorUnit : MonoBehaviour
{


    public string color;
    public int x, y;
    public float speed = 0.5f;
    public bool canMove;
    public GameObject[,] Grid;
    public GameObject[,] colorGrid;
    public Animator anim;

    private void Start()
    {
        Grid = FindObjectOfType<GridManager>().Grid;
        colorGrid = FindObjectOfType<GridManager>().colorGrid;
        anim = GetComponent<Animator>();
        canMove = true;
    }

    private void FixedUpdate()
    {
        //moveTo(FindObjectOfType<GridManager>().Grid[x, y]);
        //moveTo(Grid[x, y]);
        if (canMove && FindObjectOfType<GridManager>().canMoveEvery)
        {
            moveTo(Grid[x, y]);
            checkMoveDown();
        }
        
    }
    public void moveTo(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
    }

    public void checkMoveDown()
    {
        if (x > 0 && colorGrid[x-1, y] == null)
        {
            colorGrid[x - 1, y] = colorGrid[x, y];
            colorGrid[x, y] = null;
            x--;
        }
    }

    public void destroy()
    {
        //colorGrid[x, y] = null;
        anim.SetTrigger("Die");
        canMove = false;
        colorGrid[x, y] = null;
        if (x > 0 && colorGrid[x - 1, y] != null && colorGrid[x - 1, y].GetComponent<ColorUnit>().color.Equals(this.color))
        {
            colorGrid[x - 1, y].GetComponent<ColorUnit>().destroy();
        }
        if (x < 9 && colorGrid[x + 1, y] != null && colorGrid[x + 1, y].GetComponent<ColorUnit>().color.Equals(this.color))
        {
            colorGrid[x + 1, y].GetComponent<ColorUnit>().destroy();
        }
        if (y > 0 && colorGrid[x, y - 1] != null && colorGrid[x, y - 1].GetComponent<ColorUnit>().color.Equals(this.color))
        {
            colorGrid[x, y - 1].GetComponent<ColorUnit>().destroy();
        }
        if (y < 9 && colorGrid[x, y + 1] != null && colorGrid[x, y + 1].GetComponent<ColorUnit>().color.Equals(this.color))
        {
            colorGrid[x, y + 1].GetComponent<ColorUnit>().destroy();
        }
        //FindObjectOfType<GameManagerMG2>().updateScore(1);
        //Destroy(gameObject);
    }

    public void destroyGO()
    {
        FindObjectOfType<GameManagerMG2>().updateScore(1);
        canMove = true;
        Destroy(gameObject);
    }
}
