using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableMovement : MonoBehaviour
{
    GameObject grabbableSelf;
    Vector3 carrierMovingDistance;
    private void Start()
    {
        grabbableSelf = this.gameObject;
    }

    public void MoveWithCarrier(GameObject carrier)
    {
        GameObject currentCarrier = carrier;
        if (currentCarrier.tag == "Thief") carrierMovingDistance = currentCarrier.GetComponent<ThiefAIManager>().GetThiefVelocity();
        else if (currentCarrier.tag == "Player") carrierMovingDistance = currentCarrier.GetComponent<ThirdPersonMovement>().movingDistance;
        grabbableSelf.GetComponent<Rigidbody>().useGravity = false;
        grabbableSelf.transform.position = currentCarrier.transform.position + carrierMovingDistance + currentCarrier.transform.forward * 1.5f + Vector3.up;
    }
    public void MoveFreely()
    {
        grabbableSelf.GetComponent<Rigidbody>().useGravity = true;
    }
    public void StayPlaced(GameObject targetPlace)
    {
        grabbableSelf.transform.position = targetPlace.transform.position;
        grabbableSelf.transform.rotation = Quaternion.identity;
        grabbableSelf.GetComponent<Rigidbody>().useGravity = false;
        grabbableSelf.GetComponent<Collider>().enabled = false;
        grabbableSelf.GetComponent<GrabbableMovement>().enabled = false;
        grabbableSelf.GetComponent<GrabbableStates>().enabled = false;
        grabbableSelf.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
