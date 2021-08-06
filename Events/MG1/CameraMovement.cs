using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 2f;


    private void FixedUpdate()
    {
        speed = FindObjectOfType<GameManagerMG1>().speed;
        if (FindObjectOfType<Timer>().secondsLeft <= 2) speed = 0f;
        transform.position = transform.position + new Vector3(1f, 0, 0) * speed * Time.fixedDeltaTime;
    }
}
