using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float distanceToGrab;
    public GameObject objectToGrab;

    GameObject[] grabbables;
    

    void Start()
    {
        grabbables = GameObject.FindGameObjectsWithTag("Grabbable");
        Debug.Log(grabbables.Length);
        objectToGrab = null;
    }

    void Update()
    {
        grabbables = GameObject.FindGameObjectsWithTag("Grabbable");
        int i = 0;
        if (grabbables.Length == 0) return;
        foreach (GameObject grab in grabbables)
        {
            i += 1;
            if (Vector3.Distance(grab.transform.position, this.transform.position) < distanceToGrab)
            {
                if (!Input.GetKey(KeyCode.R))
                {
                    DropObject();
                    objectToGrab = null;
                }
                else if (Input.GetKey(KeyCode.R) && objectToGrab == null)
                {
                    objectToGrab = grab;
                    GrabObject();
                }
                
                
            }
            
            
        }



    }

    void GrabObject()
    {
        objectToGrab.GetComponent<Grabbable>().isGrabbed = true;
    }

    void DropObject()
    {
        if (objectToGrab == null) return;
        objectToGrab.GetComponent<Grabbable>().isGrabbed = false;
    }
}
