using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMG1 : MonoBehaviour
{
    public enum playerLoc { top, middle, bottom}
    public playerLoc location;

    public Rigidbody2D rb;
    public GameObject[] points = new GameObject[4];
    public GameObject text;

    public int curLoc;
    public int health = 3;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        curLoc = Random.Range(0, 4);
        transform.position = points[curLoc].transform.position;
        updateLocation();
        text.GetComponent<Text>().text = "Health: " + health;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[curLoc].transform.position, speed);
        rb.MovePosition(rb.position + new Vector2(1f, 0) * FindObjectOfType<GameManagerMG1>().speed * Time.fixedDeltaTime);
    }

    public void moveUp()
    {
        Debug.Log(curLoc);
        curLoc = Mathf.Min(curLoc + 1, 3);
        updateLocation();
    }

    public void moveDown()
    {
        Debug.Log(curLoc);
        curLoc = Mathf.Max(curLoc - 1, 0);
        updateLocation();
    }

    public void updateLocation()
    {
        if (curLoc == 0)
        {
            location = playerLoc.bottom;
        }
        else if (curLoc == 3)
        {
            location = playerLoc.top;
        }
        else
        {
            location = playerLoc.middle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            health--;
            text.GetComponent<Text>().text = "Health: " + health;
            Destroy(collision.gameObject);
            if (health == 0)
            {
                Debug.Log("Game Over");
                GameManager.instance.returnLoss();
            }
        }
    }
}
