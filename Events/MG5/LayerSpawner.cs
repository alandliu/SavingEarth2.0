using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSpawner : MonoBehaviour
{
    public GameObject[] layers;
    public GameObject minRef, maxRef;
    public GameObject layer_obj;
    public GameManagerMG5 gm;
    public int id;
    public int layersNeeded;
    public int curLayers;
    public int seconds;

    private void Start()
    {
        curLayers = 0;
        gm = FindObjectOfType<GameManagerMG5>();
        gm.grounded = false;
        //if (gm.curBuilding == id) SpawnLayer();
    }
    public void SpawnLayer()
    {
        layer_obj = Instantiate(layers[Random.Range(0, layers.Length)]);
        layer_obj.transform.SetParent(this.transform);
        layer_obj.transform.position = transform.position;
    }

    public void SpawnSpecific(BuildingLayer bl)
    {
        layer_obj = Instantiate(bl.gameObject);
        layer_obj.transform.SetParent(this.transform);
        layer_obj.transform.position = transform.position;
    }

    public void updateLayerCount()
    {
        curLayers++;
        if (curLayers >= layersNeeded)
        {
            gm.switchBuildings();
        }
    }

    public void destroyStructure()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.transform.tag == "City")
            {
                Destroy(child.gameObject);
            }
        }
        gm.grounded = false;
        curLayers = 0;
    }
}
