using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedLayer : MonoBehaviour
{

    public BuildingLayer savedLayer;
    public BuildingLayer temp;
    public Image im;
    public Sprite defIm;
    public GameManagerMG5 gm;

    // Start is called before the first frame update
    void Start()
    {
        savedLayer = null;
        gm = FindObjectOfType<GameManagerMG5>();
        im = GetComponent<Image>();
        defIm = im.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        detectInput();
    }

    void detectInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Pressed");
            saveLayer();
        }
    }

    void saveLayer()
    {
        if (savedLayer == null && !gm.currentLayer.hasSaved && !gm.currentLayer.hasClicked)
        {
            Debug.Log("Saved");
            savedLayer = gm.currentLayer;
            im.sprite = gm.currentLayer.GetComponent<SpriteRenderer>().sprite;
            im.color = gm.currentLayer.GetComponent<SpriteRenderer>().color;

            gm.currentLayer.hasSaved = true;
            gm.currentLayer.gameObject.SetActive(false);
            gm.spawnNewLayer();
        } else if (!gm.currentLayer.hasClicked && !gm.currentLayer.hasSaved)
        {



            gm.currentLayer.hasSaved = true;
            gm.currentLayer.gameObject.SetActive(false);
            savedLayer.gameObject.SetActive(true);
            temp = gm.currentLayer;
            gm.currentLayer = savedLayer;
            im.sprite = temp.GetComponent<SpriteRenderer>().sprite;
            im.color = temp.GetComponent<SpriteRenderer>().color;
            //gm.currentLayer = savedLayer;
            savedLayer = temp;

            //gm.currentLayer.hasSaved = true;
            //gm.currentLayer.gameObject.SetActive(false);

            Debug.Log("Loading saved");
            //Destroy(gm.currentLayer.gameObject);
            //savedLayer.gameObject.SetActive(true);
            //gm.currentLayer = savedLayer;
            //im.color = new Color(1f, 1f, 1f);
            //savedLayer = null;
        }
    }
}
