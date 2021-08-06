using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogSpawner : MonoBehaviour
{

    public GameObject[] smog;
    public float smogCooldown;
    public bool canSpawn;
    public int randInt;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;
        smogCooldown = smogCooldown + Random.Range(0, 3) * Random.Range(-1, 2);
        StartCoroutine(smogCD());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            randInt = Random.Range(0, 11);
            if (randInt <= 6) Instantiate(smog[0], transform.position, Quaternion.identity);
            else if (randInt <= 9) Instantiate(smog[1], transform.position, Quaternion.identity);
            else
            {
                Instantiate(smog[2], transform.position, Quaternion.identity);
            }
            StartCoroutine(smogCD());
        }
    }

    private IEnumerator smogCD()
    {
        canSpawn = false;
        yield return new WaitForSeconds(smogCooldown + Random.Range(0, 3) * Random.Range(-1, 2));
        canSpawn = true;
    }
}
