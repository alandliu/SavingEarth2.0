using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMG1 : MonoBehaviour
{
    public enum playerLoc { top, middle, bottom}
    public playerLoc location;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public GameObject[] points = new GameObject[4];
    public GameObject[] hearts = new GameObject[3];
    public GameObject loseScreen;
    public Sprite heartEmpty;

    public int curLoc;
    public int health = 3;
    public float speed = 5f;
    public float invincTime = 2f;
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        curLoc = Random.Range(0, 4);
        transform.position = points[curLoc].transform.position;
        updateLocation();
        sr = GetComponent<SpriteRenderer>();
        
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
        if (collision.CompareTag("Obstacle") && !isInvincible)
        {
            StartCoroutine(invincible());
            StartCoroutine(blink());
            health--;
            //text.GetComponent<Text>().text = "Health: " + health;
            hearts[health].GetComponent<Image>().sprite = heartEmpty;
            if (health == 0)
            {
                Debug.Log("Game Over");
                Time.timeScale = 0;
                loseScreen.SetActive(true);
            }
        }
    }

    IEnumerator invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincTime);
        isInvincible = false;
    }

    IEnumerator blink()
    {
        while (isInvincible)
        {
            if (sr.color.a == 0)
            {
                sr.color = new Color(1, 1, 1, 1);
            }
            else
            {
                sr.color = new Color(1, 1, 1, 0);
            }
            yield return new WaitForSeconds(1f / 30f);
        }
        sr.color = new Color(1, 1, 1, 1);
    }
}
