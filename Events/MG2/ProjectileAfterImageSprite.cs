using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAfterImageSprite : MonoBehaviour
{

    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMult = 0.7f;

    private Transform proj;

    private SpriteRenderer sr;
    private SpriteRenderer projSR;

    private Color color;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        proj = GameObject.FindGameObjectWithTag("Projectile").transform;
        projSR = proj.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sr.sprite = projSR.sprite;
        transform.position = proj.position;
        transform.rotation = proj.rotation;
        transform.localScale = proj.localScale;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMult;
        color = new Color(1f, 1f, 1f, alpha);
        sr.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            ProjectileAfterImagePool.Instance.AddToPool(gameObject);
        }    
    }
}
