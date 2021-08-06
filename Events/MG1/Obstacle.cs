using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject[] obstacle = new GameObject[7];
    public float speed = 2f;
    public float cdTime = 25f;
    public bool cd = false;

    public bool hasSpawnedFinal = false;

    int rng;
    private void Update()
    {
        if (!cd && FindObjectOfType<Timer>().secondsLeft >= 10)
        {
            Instantiate(obstacle[Random.Range(0, obstacle.Length - 1)], transform.position, transform.rotation);
            //Instantiate(obstacle[6], transform.position, transform.rotation);
            StartCoroutine(coolDown());
            Debug.Log("spawned");
        }

        if (!cd && FindObjectOfType<Timer>().secondsLeft < 10 && !hasSpawnedFinal)
        {
            hasSpawnedFinal = true;
            Instantiate(obstacle[6], transform.position, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(1f, 0) * FindObjectOfType<GameManagerMG1>().speed * Time.fixedDeltaTime);
    }

    IEnumerator coolDown()
    {
        cd = true;
        yield return new WaitForSeconds(cdTime);
        cd = false;
    }

}
