using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAIManager : MonoBehaviour
{
    [SerializeField] float speed = 0.3f;
    [SerializeField] float distanceToObject = 2f;

    MAnimation mAnimation;
    GameObject targetPlace;
    GameObject targetObject;

    bool isInGrabDistance = false;

    Vector3 velocity;
    Vector3 previousVelocity;



    void Start()
    {
        mAnimation = this.gameObject.GetComponent<MAnimation>();
        targetPlace = this.gameObject.GetComponent<PathManager>().CurrentTargetPlace();
        targetObject = this.gameObject.GetComponent<PathManager>().CurrentObject();
    }

    private void Update()
    {
        velocity = transform.position - previousVelocity;
        previousVelocity = transform.position;
        targetObject = this.gameObject.GetComponent<PathManager>().CurrentObject();
        targetPlace = this.gameObject.GetComponent<PathManager>().CurrentTargetPlace();
        if (targetObject == null) return;
        targetPlace.GetComponent<ScorePlaceDetection>().SetTargeted(targetPlace);
        IsInGrabDistance();
        if (isInGrabDistance && !targetPlace.GetComponent<ScorePlaceDetection>().CanBePlaced())
        {
            this.gameObject.GetComponent<ThiefAction>().GrabObject(targetObject);
        }
        else if (targetPlace.GetComponent<ScorePlaceDetection>().CanBePlaced() && targetObject.GetComponent<GrabbableStates>().GetIsGrabbed() == true)
        {
            this.gameObject.GetComponent<ThiefAction>().DropObject(targetObject);
        }

    }

    void FixedUpdate()
    {
        if (targetObject == null) return;
        
        if (targetObject.GetComponent<GrabbableStates>().GetIsGrabbed() == true)
        {
            this.gameObject.GetComponent<ThiefMovement>().MoveWithGrab(targetPlace.transform.position, speed, mAnimation);
        }
        else
        {
            this.gameObject.GetComponent<ThiefMovement>().MoveToObject(targetObject, speed, mAnimation);
        }
        

    }

    void IsInGrabDistance()
    {
        if (targetObject.GetComponent<GrabbableStates>().GetIsGrabbed()) return;
        float distance = Vector3.Distance(targetObject.transform.position - Vector3.up * targetObject.transform.position.y, this.gameObject.transform.position);
        isInGrabDistance = (distance < distanceToObject);
    }

    public Vector3 GetThiefVelocity()
    {
        return velocity;
    }





}
