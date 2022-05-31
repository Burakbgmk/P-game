using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAction : MonoBehaviour
{

    public void GrabObject(GameObject targetObject)
    {
        targetObject.GetComponent<GrabbableStates>().SetGrabbed(this.gameObject);
    }
    public void DropObject(GameObject targetObject)
    {
        targetObject.GetComponent<GrabbableStates>().SetFree();
    }

}
