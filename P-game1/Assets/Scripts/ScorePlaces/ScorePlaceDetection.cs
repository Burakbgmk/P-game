using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlaceDetection : MonoBehaviour
{
    bool canBePlaced = false;
    bool isTargeted = false;
    bool isOccupied = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Grabbable" && !other.gameObject.GetComponent<GrabbableStates>().GetIsGrabbed() && isTargeted && isOccupied == false)
        {
            other.gameObject.GetComponent<GrabbableStates>().SetPlaced(this.gameObject);
            isOccupied = true;
        }
        else if (other.gameObject.tag == "Grabbable" && other.gameObject.GetComponent<GrabbableStates>().GetIsGrabbed() && isOccupied == false && canBePlaced == false)
        {
            canBePlaced = true;
        }
        else canBePlaced = false;
    }

    public bool CanBePlaced()
    {
        return canBePlaced;
    }

    public void SetTargeted(GameObject targetPlace)
    {
        if(targetPlace == this.gameObject) isTargeted = true;

    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }
}
