using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAI : MonoBehaviour
{
    [SerializeField] float distanceToObject = 1.9f;
    [SerializeField] float speed = 0.04f;
    [SerializeField] float grabDistance = 2f;
    public Vector3 velocity;

    GameObject[] thiefObjects;
    GameObject thiefObject;
    MAnimation mAnimation;
    
    Vector3 previousVelocity;
    Vector3 randomGrabPath;


    void Start()
    {
        thiefObjects = GameObject.FindGameObjectsWithTag("Grabbable");
        mAnimation = GetComponent<MAnimation>();
        thiefObject = ChooseObject();
        randomGrabPath = new Vector3(Random.Range(-15, -20), 0, Random.Range(-15, -20));
    }

    private void Update()
    {
        velocity = (transform.position - previousVelocity);
        previousVelocity = transform.position;
    }

    void FixedUpdate()
    {
        if(thiefObject.GetComponent<Grabbable>().isThiefGrabbed == false)
        {
            MoveToObject();
        }
        else
        {
            MoveOnGrab();
        }
        if (this.GetComponent<CombatTarget>().isPushed == true)
        {
            thiefObject.GetComponent<Grabbable>().isThiefGrabbed = false;
        }

    }

    void MoveToObject()
    {
        float distance = Vector3.Distance(thiefObject.transform.position, this.transform.position);
        if (distance > distanceToObject)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, thiefObject.transform.position, speed);
            this.transform.LookAt(thiefObject.transform.position);
            mAnimation.RunAnimator();
        }
        if (distance < grabDistance)
        {
            GrabObject();
        }


    }
    GameObject ChooseObject()
    {
        return thiefObjects[Random.Range(0, thiefObjects.Length)];
    }

    void GrabObject()
    {
        thiefObject.GetComponent<Grabbable>().isThiefGrabbed = true;
    }

    void MoveOnGrab()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, randomGrabPath, speed);
        this.transform.LookAt(randomGrabPath);
        mAnimation.RunAnimator();
    }

}
