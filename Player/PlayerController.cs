using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* TODO
 1. Spawn points <- edit this shit // access SP script for anim maybe
 2. Y-sorting  <- remember to change to pivot
 3. Scene transition test <- discuss i suppose
 4. Set up GM  <- not bad
 5. Maybe set up audio manager

*/


public class PlayerController : MonoBehaviour
{
    // Private
    [SerializeField]
    private int maxHealth = 3;
    private int dir = 1;
    private Vector3 characterScale;
    private GameManager gm;
    

    // Public
    public int curHealth;
    public int levelNum = 0;
    public int taskNumber;
    public int charNum;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public bool canMove = true;
    

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        characterScale = transform.localScale;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gm.spawnPoint;
        Debug.Log("Spawned at " + gm.spawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        // Input stuff
        checkMove();
        checkFlip();
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
    }


    // Note to self: Rewire to joystick moron.
    // Revisit Unity Touch stuff
    private void checkMove()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }

    // Don't need will delete later
    private void checkFlip()
    {
        if (movement.x > 0)
        {
            characterScale.x = 5f;
            dir = 1;
        }
        if (movement.x < 0)
        {
            characterScale.x = -5f;
            dir = -1;
        }

        transform.localScale = characterScale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            gm.nextLevel();
        }
    }
}
