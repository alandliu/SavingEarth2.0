using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogCloud : MonoBehaviour
{

    public float speed = 2f;
    public int damage;
    public Rigidbody2D rb;
    public CityBar cityBar;
    public Sprite[] smogs;
    public SpriteRenderer sr;
    public Animator anim;
    public ParticleSystem ps;
    public int randInt;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cityBar = FindObjectOfType<CityBar>();
        sr = GetComponent<SpriteRenderer>();
        randInt = Random.Range(0, smogs.Length);
        //sr.sprite = smogs[randInt];
        anim = GetComponent<Animator>();
        anim.SetBool("" + randInt, true);
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
