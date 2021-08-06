using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPoint : MonoBehaviour
{
    public GameObject proj;

    private void Start()
    {
        createProj();
    }
    public void createProj()
    {
        Instantiate(proj, transform.position, Quaternion.identity);
    }
}
