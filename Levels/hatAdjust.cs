using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatAdjust : MonoBehaviour
{
    public Animator anim;
    public float waitTime;
    public bool canAdjust;
    void Start()
    {
        canAdjust = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAdjust)
        {
            anim.SetTrigger("adjust");
            StartCoroutine(waitForAdjust());
        }
    }

    private IEnumerator waitForAdjust()
    {
        canAdjust = false;
        yield return new WaitForSeconds(waitTime);
        canAdjust = true;
    }
}
