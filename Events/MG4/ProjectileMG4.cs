using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMG4 : MonoBehaviour
{
    private bool isPressed;
    private float maxDragDistance = 1.75f;
    private float minDragDistance = 0.5f;
    private float minCannonDistance = 0.01f;


    public float releaseDelay;
    public int shotNum;
    public int costPerShot;

    public float rotZ;
    public float rotSpeed;
    public float relGrav = 1.5f;
    public bool isRotating;
    public bool canPress;

    public Rigidbody2D rb;
    public SpringJoint2D sj;
    public Rigidbody2D slingRb;
    public Vector2 spawnPoint;
    public LineRenderer lr;
    public WaterBar waterBar;
    public GameObject parent;
    public SmogCloud tempCloudSave;
    public NetCloud tempNetSave;
    public SpriteRenderer sr;
    public ParticleSystem ps;

    public GameObject cannon;

    private Vector3 difference;
    private float fRotZ;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        slingRb = sj.connectedBody;
        lr = GetComponent<LineRenderer>();
        sr = GetComponent<SpriteRenderer>();
        waterBar = FindObjectOfType<WaterBar>();

        lr.enabled = false;
        spawnPoint = transform.position;
        isRotating = false;
        rb.gravityScale = 0f;
        canPress = true;

        releaseDelay = 1 / (sj.frequency * 4);
        shotNum = 0;
        sr.enabled = false;

        var emission = ps.emission;
        emission.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotating)
        {
            rotZ += Time.deltaTime * rotSpeed;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (isPressed)
        {
            DragBall();

            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cannon.transform.position;
            difference.Normalize();
            fRotZ = Mathf.Atan2(-difference.y, -difference.x) * Mathf.Rad2Deg;

            cannon.transform.rotation = Quaternion.Euler(0f, 0f, fRotZ); 
        }
    }



    private void DragBall()
    {
        SetLineRendererPositions();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, slingRb.position);

        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePosition - slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePosition;
        }
    }

    private void SetLineRendererPositions()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = rb.position;
        positions[1] = slingRb.position;
        lr.SetPositions(positions);
    }

    private void OnMouseDown()
    {
        if (!canPress) return;
        isPressed = true;
        rb.isKinematic = true;
        lr.enabled = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        if (checkMin() && waterBar.slider.value >= costPerShot)
        {
            waterBar.setHealth((int) waterBar.slider.value - costPerShot);
            StartCoroutine(Release());
            FindObjectOfType<LaunchPoint>().createProj();
            sr.enabled = true;
            var emission = ps.emission;
            emission.enabled = true;
        }
        else
        {
            transform.position = spawnPoint;
            lr.enabled = false;
        }
    }

    private IEnumerator Release()
    {
        canPress = false;
        yield return new WaitForSeconds(releaseDelay);
        isRotating = true;
        sj.enabled = false;
        lr.enabled = false;
        rb.gravityScale = relGrav;
        transform.SetParent(null, true);
        Destroy(parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shotNum++;
        if (collision.CompareTag("Color Orb"))
        {
            tempCloudSave = collision.GetComponent<SmogCloud>();
            tempCloudSave.anim.SetTrigger("death");
            tempCloudSave.speed = 0f;
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Net Cloud"))
        {
            tempNetSave = collision.GetComponent<NetCloud>();
            tempNetSave.speed = 0;
            tempNetSave.anim.SetTrigger("death");
            collision.GetComponent<NetCloud>().destroyAll();
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Deletion"))
        {
            Destroy(this.gameObject);
        }
    }

    private bool checkMin()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, slingRb.position);

        if (distance >= minDragDistance)
        {
            return true;
        }
        return false;
    }
}
