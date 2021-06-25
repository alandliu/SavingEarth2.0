using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 2f;


    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(1f, 0, 0) * speed * Time.fixedDeltaTime;
    }
}
