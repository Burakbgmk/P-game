using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefMovement : MonoBehaviour
{

    public void MoveToObject(GameObject objectToMove, float speed, MAnimation mAnimation)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, objectToMove.transform.position - Vector3.up * objectToMove.transform.position.y, speed);
        this.transform.LookAt(objectToMove.transform.position - Vector3.up * objectToMove.transform.position.y);
        mAnimation.RunAnimator();
    }

    public void MoveWithGrab(Vector3 grabPath, float speed, MAnimation mAnimation)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, grabPath, speed);
        this.transform.LookAt(grabPath);
        mAnimation.RunAnimator();
    }
}
