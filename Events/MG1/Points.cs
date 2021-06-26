using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(1f, 0) * FindObjectOfType<GameManagerMG1>().speed * Time.fixedDeltaTime);
    }
}
