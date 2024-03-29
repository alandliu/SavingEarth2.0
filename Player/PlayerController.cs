﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* TODO
 1. Spawn points <- edit this shit // access SP script for anim maybe
 2. Y-sorting  <- remember to change to pivot
 3. Scene transition test <- discuss i suppose
 4. Set up GM  <- continuing
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
    public Animator[] anims = new Animator[4];

    public int curHealth = 3;
    public int levelNum = 0;
    public int taskNumber = 0;
    public int charNum = -1;
    public float speed = 5f;
    public bool spawned = false;

    public Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 respawnPoint;
    public Vector2 lossPoint;
    public GameObject text;
    public Animator anim;
    public Image[] hearts;
    public Sprite emptySprite;
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        loadPlayer();

        if (curHealth <= 0)
        {
            gm.Reset();
            loadPlayer();
        }

        loadHearts();
        //text.GetComponent<Text>().text = "Health: " + curHealth;

        characterScale = transform.localScale;

        // doesnt look so good
        if (!spawned)
        {
            transform.position = gm.spawnPoint;
            spawned = true;
            Debug.Log("Spawned at " + gm.spawnPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Input stuff
        if (gm.state == GameManager.GameState.freeRoam)
        {
            checkMove();
            checkFlip();
            UpdateWalk();
        }
        else
        {
            movement = Vector2.zero;
            UpdateWalk();
            DialogueManager.Instance.HandleUpdate();
        }
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
            characterScale.x = 0.75f;
            dir = 1;
        }
        else if (movement.x < 0)
        {
            characterScale.x = -0.75f;
            dir = -1;
        } 

        transform.localScale = characterScale;
    }

    private void UpdateWalk()
    {
        if (movement.x > 0.01f || movement.y > 0.01f || movement.x < -0.01f || movement.y < -0.01f)
        {
            if (!anim.GetBool("isWalking")) anim.SetBool("isWalking", true);
        } else if (movement.Equals(Vector2.zero)) {
            if (anim.GetBool("isWalking")) anim.SetBool("isWalking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            if (taskNumber == 3) gm.nextLevel();
        }

        if (collision.CompareTag("event"))
        {
            if (collision.GetComponent<Event>().mgNum == taskNumber)
            {
                Debug.Log("Event");
                collision.GetComponent<Interactable>()?.Interact(collision.GetComponent<Event>().mgName);
                respawnPoint = collision.transform.GetChild(0).position;
                lossPoint = collision.transform.GetChild(1).position;
                savePlayer();
                //gm.loadMG(collision.GetComponent<Event>().mgName);
            }
        }

        if (collision.CompareTag("NPC"))
        {
            Debug.Log("NPC");
            collision.GetComponent<Interactable>()?.Interact("NPC");
        }
    }

    private void loadPlayer()
    {
        curHealth = gm.playerHealth;
        taskNumber = gm.playerTask;
        //transform.position = new Vector2(gm.playerpos.x - 1f, gm.playerpos.y - 1f);
        transform.position = gm.playerpos;
        spawned = gm.spawned;
    }
    private void savePlayer()
    {
        gm.playerHealth = curHealth;
        gm.playerTask = taskNumber;
        gm.winPoint = respawnPoint;
        gm.lossPoint = lossPoint;
        gm.spawned = spawned;
    }

    private void loadHearts()
    {
        for (int i = 0; i < maxHealth - curHealth; i++)
        {
            hearts[i].sprite = emptySprite;
        }
    }
}
