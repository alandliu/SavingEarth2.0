using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCloud : MonoBehaviour
{
    public float speed = 2f;
    public int damage;
    public Rigidbody2D rb;
    public CityBar cityBar;
    public SpriteRenderer sr;
    public Animator anim;
    public Sprite[] smogs;
    public SmogCloud tempSmog;
    public ParticleSystem ps;

    public GameObject[] clouds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("" + Random.Range(0, smogs.Length), true);
        //sr.sprite = smogs[Random.Range(0, smogs.Length)];
        cityBar = FindObjectOfType<CityBar>();
        var em = ps.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + new Vector2(-1f, 0) * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "City")
        {
            Destroy(this.gameObject);
            cityBar.setHealth((int)cityBar.slider.value - damage);
        }
    }

    public void destroyAll()
    {
        clouds = GameObject.FindGameObjectsWithTag("Color Orb");
        //clouds = FindObjectsOfType<SmogCloud>();
        foreach (GameObject s in clouds)
        {
            tempSmog = s.GetComponent<SmogCloud>();
            tempSmog.speed = 0;
            tempSmog.anim.SetTrigger("death"); 
        }
    }

    public void death()
    {
        Destroy(this.gameObject);
    }

    public void Rain()
    {
        var em = ps.emission;
        em.enabled = true;
    }
}
