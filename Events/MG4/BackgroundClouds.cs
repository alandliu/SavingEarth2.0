using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundClouds : MonoBehaviour
{
    public float speed;
    private float textureUnitSizeX;
    private float offsetPositionX;
    private float traveledDistance;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        traveledDistance = 0;
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        lastCameraPosition = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, cameraTransform.position.y);
        }
    }
}
