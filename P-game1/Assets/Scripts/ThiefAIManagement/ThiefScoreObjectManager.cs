using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefScoreObjectManager : MonoBehaviour
{
    //private GameObject[] thiefObjects;
    private GameObject thiefObject;
    private GameObject[] thiefObjects;

    private void Start()
    {
        thiefObjects = GameObject.FindGameObjectsWithTag("Grabbable");
    }
    private void Update()
    {
        thiefObjectUpdate();
        this.gameObject.GetComponent<PathManager>().SetCurrentObject(thiefObject);
    }

    private void thiefObjectUpdate()
    {
        thiefObjects = GameObject.FindGameObjectsWithTag("Grabbable");
        thiefObject = ChooseObject();

    }
    
    GameObject ChooseObject()
    {
        foreach (GameObject item in thiefObjects)
        {
            if (item.GetComponent<ThiefScoreObject>() != null && !item.GetComponent<GrabbableStates>().GetIsPlaced())
            {
                thiefObject = item;
            }
        }
        return thiefObject;
    }

}
