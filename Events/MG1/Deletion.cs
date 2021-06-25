using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(1f, 0) * speed * Time.fixedDeltaTime);
    }
    
}
