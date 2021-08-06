using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLayer : MonoBehaviour
{
    private GameObject minRef, maxRef;
    private GameManagerMG5 gm;
    private bool canMove;
    public float moveSpeed = 2f;
    private bool ignoreCollision;
    private bool ignoreTrigger;
    private bool gameOver;
    private Rigidbody2D rb;
    public bool canSpawn;
    private float timeBetweenSpawn = 2f;

    public SavedLayer sl;
    public Transform parent;
    public Transform child;
    public Transform finishLine;
    public LayerSpawner parentSpawn;
    public bool hasSaved;
    public bool hasLanded;
    public bool hasClicked;

    public enum moveState { left, right };
    public moveState moveDir;


    private void Awake()
    {
        gm = FindObjectOfType<GameManagerMG5>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        sl = FindObjectOfType<SavedLayer>();
        gameOver = false;
        hasClicked = false;
        canSpawn = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        child = this.transform.GetChild(0);
        parent = this.transform.parent;
        finishLine = parent.GetChild(3);
        parentSpawn = parent.GetComponent<LayerSpawner>();
        this.minRef = parent.GetComponent<LayerSpawner>().minRef;
        this.maxRef = parent.GetComponent<LayerSpawner>().maxRef;
        canMove = true;
        hasLanded = false;

        if (Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1f;
            moveDir = moveState.left;
        }
        else
        {
            moveDir = moveState.right;
        }

        gm.currentLayer = this;
        hasSaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveBox();
    }

    void moveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > maxRef.transform.position.x && moveDir == moveState.right)
            {
                moveSpeed *= -1f;
                moveDir = moveState.left;
            } else if (temp.x < minRef.transform.position.x && moveDir == moveState.left)
            {
                moveSpeed *= -1f;
                moveDir = moveState.right;
            }
            transform.position = temp;
        }
    }

    public void DropLayer()
    {
        if (gameOver) return;
        canMove = false;
        rb.gravityScale = 2;
        hasClicked = true;
    }

    void Landed()
    {

        ignoreCollision = true;
        CancelInvoke("Landed");
        //ignoreTrigger = true;
        //parentSpawn.updateLayerCount();
        gm.spawnNewLayer();
        gm.grounded = true;
        hasLanded = true;
        StartCoroutine(cdLand());
        if (child.position.y >= finishLine.position.y)
        {
            sl.savedLayer = null;
            sl.im.sprite = sl.defIm;
            sl.im.color = new Color(1f, 1f, 1f);
            gm.switchBuildings();
        }
    }

    private IEnumerator cdLand()
    {
        canSpawn = false;
        yield return new WaitForSeconds(timeBetweenSpawn);
        canSpawn = true;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ignoreCollision) return;

        if (collision.gameObject.tag == "Ground")
        {
            if (!gm.grounded) { 
                Invoke("Landed", 2f);
            }
            else
            {
                Debug.Log("Skeeted");
                ignoreCollision = true;
                ignoreTrigger = true;
                CancelInvoke("Landed");
                gm.loseHealth();
                //Destroy(this.gameObject);
                parentSpawn.destroyStructure();
            }
        }

        if (collision.gameObject.tag == "City")
        {
            Invoke("Landed", 2f);
        }
        
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ignoreCollision) return;
        

        if (collision.gameObject.tag == "City" || collision.gameObject.tag == "Ground")
        {
            if (canSpawn) Invoke("Landed", 2f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (ignoreTrigger) return;
        Debug.Log("peep");
        if (collision.gameObject.tag == "Finish" && hasLanded)
        {
            sl.savedLayer = null;
            sl.im.sprite = sl.defIm;
            sl.im.color = new Color(1f, 1f, 1f);
            gm.switchBuildings();
        }

        if (collision.gameObject.tag == "Deletion")
        {
            Debug.Log("Deleted");
            CancelInvoke("Landed");
            ignoreCollision = true;
            ignoreTrigger = true;
            //gm.loseHealth();
            //Destroy(this.gameObject);
            parentSpawn.destroyStructure();
            parentSpawn.SpawnLayer();
        }
    }
}
