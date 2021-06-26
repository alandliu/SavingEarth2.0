using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject obstacle;
    public float speed = 2f;
    int rng;
    private void Update()
    {
        rng = Random.Range(0, 5000);
        if (rng < 20)
        {
            Instantiate(obstacle, transform.position, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(1f, 0) * FindObjectOfType<GameManagerMG1>().speed * Time.fixedDeltaTime);
    }

}
