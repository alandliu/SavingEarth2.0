using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isPressed;
    private float maxDragDistance = 1.5f;
    private float minDragDistance = 0.5f;
    private float lastImageXPosition;
    

    public float releaseDelay;
    public float distanceBetweenImages;
    public int shotNum;

    public float rotZ;
    public float rotSpeed;
    public bool isRotating;

    public Rigidbody2D rb;
    public SpringJoint2D sj;
    public Rigidbody2D slingRb;
    public Vector2 spawnPoint;
    public LineRenderer lr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        slingRb = sj.connectedBody;
        lr = GetComponent<LineRenderer>();

        lr.enabled = false;
        spawnPoint = transform.position;
        isRotating = false;

        releaseDelay = 1 / (sj.frequency * 4);
        shotNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            rotZ += Time.deltaTime * rotSpeed;

            if(Mathf.Abs(transform.position.x - lastImageXPosition) > distanceBetweenImages)
            {
                ProjectileAfterImagePool.Instance.GetFromPool();
                lastImageXPosition = transform.position.x;
            }
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (isPressed)
        {
            DragBall();
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
        } else
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
        isPressed = true;
        rb.isKinematic = true;
        lr.enabled = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        if (checkMin())
        {
            ProjectileAfterImagePool.Instance.GetFromPool();
            lastImageXPosition = transform.position.x;
            StartCoroutine(Release());
        } else
        {
            transform.position = spawnPoint;
            lr.enabled = false;
        }
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseDelay);
        isRotating = true;
        sj.enabled = false;
        lr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shotNum++;
        if (collision.CompareTag("Color Orb"))
        {
            collision.GetComponent<ColorUnit>().destroy();
            transform.position = spawnPoint;
            sj.enabled = true;
            rb.velocity = Vector2.zero;
            FindObjectOfType<GridManager>().checkGrid();
            isRotating = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotZ = 0;
        }

        if (collision.CompareTag("Deletion"))
        {
            transform.position = spawnPoint;
            sj.enabled = true;
            rb.velocity = Vector2.zero;
            isRotating = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotZ = 0;
        }

        if (shotNum == 2)
        {
            FindObjectOfType<GridManager>().updateGrid();
            shotNum = 0;
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
