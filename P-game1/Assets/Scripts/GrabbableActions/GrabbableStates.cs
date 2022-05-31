using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableStates : MonoBehaviour
{
    enum GrabbableState {grabbed, free, placed};
    GrabbableState grabbableState = GrabbableState.free;
    GameObject currentCarrier;
    GameObject targetPlace;

    void Update()
    {
        switch (grabbableState)
        {
            case GrabbableState.grabbed:
                this.GetComponent<GrabbableMovement>().MoveWithCarrier(currentCarrier);
                break;
            case GrabbableState.free:
                this.GetComponent<GrabbableMovement>().MoveFreely();
                break;
            case GrabbableState.placed:
                this.GetComponent<GrabbableMovement>().StayPlaced(targetPlace);
                break;
        }

    }

    public void SetGrabbed(GameObject carrier)
    {
        grabbableState = GrabbableState.grabbed;
        currentCarrier = carrier;
        
    }
    public void SetFree()
    {
        grabbableState = GrabbableState.free;
    }
    public void SetPlaced(GameObject target)
    {
        grabbableState = GrabbableState.placed;
        targetPlace = target;
        Debug.Log("Object is Placed");
    }

    public bool GetIsGrabbed()
    {
        if (grabbableState == GrabbableState.grabbed) return true;
        else return false;
    }
    
    public bool GetIsFree()
    {
        if (grabbableState == GrabbableState.free) return true;
        else return false;
    }

    public bool GetIsPlaced()
    {
        if (grabbableState == GrabbableState.placed) return true;
        else return false;
    }




}
