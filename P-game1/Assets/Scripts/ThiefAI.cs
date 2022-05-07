using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAI : MonoBehaviour
{
    [SerializeField] float distanceToObject = 1.9f;
    [SerializeField] float speed = 0.04f;
    [SerializeField] float grabDistance = 2f;
    public Vector3 velocity;
    public bool isScored;

    GameObject[] thiefObjects;
    GameObject thiefObject;
    GameObject[] targetPlaces;
    GameObject initialTargetPlace;
    GameObject targetPlace;
    GameObject objectToDiscard;
    MAnimation mAnimation;
    
    Vector3 previousVelocity;
    Vector3 grabPath;

    int placedScore;
    int initalTargetPlaceIndex;

    bool toNextObject = true;


    void Start()
    {
        placedScore = 0;
        isScored = false;
        mAnimation = GetComponent<MAnimation>();
        targetPlaces = GameObject.FindGameObjectsWithTag("TargetPlace");
        initalTargetPlaceIndex = Random.Range(0, targetPlaces.Length);
        initialTargetPlace = targetPlaces[initalTargetPlaceIndex];
        //grabPath = new Vector3(Random.Range(-15, -20), 0, Random.Range(-15, -20));
        thiefObjectUpdate();
    }

    private void Update()
    {
        velocity = (transform.position - previousVelocity);
        previousVelocity = transform.position;
        if (this.GetComponent<CombatTarget>().isPushed == true)
        {
            DropObject();
        }
        thiefObjectUpdate();
        ChoosingPath();
    }



    void FixedUpdate()
    {
        if (thiefObject == null)
        {
            Debug.Log("Thief object is null");
            return;
        }
        if (thiefObject.GetComponent<Grabbable>().isThiefGrabbed == false)
        {
            Debug.Log("Moving to thief object");
            MoveToObject();
        }
        else if (toNextObject == false)
        {
            MoveOnGrab();
        }
    }
    //ThiefMovement
    void MoveToObject()
    {
        float distance = Vector3.Distance(thiefObject.transform.position - Vector3.up * thiefObject.transform.position.y, this.transform.position);
        if (distance > distanceToObject)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, thiefObject.transform.position - Vector3.up * thiefObject.transform.position.y, speed);
            this.transform.LookAt(thiefObject.transform.position - Vector3.up * thiefObject.transform.position.y);
            mAnimation.RunAnimator();
        }
        if (distance < grabDistance)
        {
            GrabObject();
        }

    }
    void MoveOnGrab()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, grabPath, speed);
        this.transform.LookAt(grabPath);
        mAnimation.RunAnimator();
    }
    // //

    // Thief Object Selection and Action
    private void thiefObjectUpdate()
    {
        thiefObjects = GameObject.FindGameObjectsWithTag("Grabbable");
        thiefObject = ChooseObject();
    }

    GameObject ChooseObject()
    {
        foreach (GameObject item in thiefObjects)
        {
            if (item.GetComponent<ThiefScoreObject>() != null && item != null && item.GetComponent<Grabbable>().isActiveAndEnabled == true)
            {
                objectToDiscard = item;
                return item;
            }
        }
        return null;
    }

    void GrabObject()
    {
        thiefObject.GetComponent<Grabbable>().isThiefGrabbed = true;
        toNextObject = false;
    }

    public void DropObject()
    {
        thiefObject.GetComponent<Grabbable>().isThiefGrabbed = false;
    }

    private void SetObjectPlaced()
    {
        thiefObject.GetComponent<Rigidbody>().useGravity = true;
        thiefObject.GetComponent<Grabbable>().enabled = false;
        toNextObject = true;
    }

    // //

    // Path Manager
    void ChoosingPath()
    {
        if(isScored == false)
        {
            grabPath = initialTargetPlace.transform.position;
            
        }
        else if(isScored == true)
        {
            if(targetPlace == null)
            {
                targetPlace = targetPlaces[Random.Range(0, targetPlaces.Length)];
            }
            grabPath = targetPlace.transform.position;
            
        }


    }

    private void RemoveTargetPlace()
    {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(targetPlaces);
        list.Remove(targetPlace);
        targetPlaces = list.ToArray();
        targetPlace = null;
    }

    private void RemoveInitialPlace()
    {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(targetPlaces);
        list.Remove(initialTargetPlace);
        targetPlaces = list.ToArray();
    }

    // //

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == initialTargetPlace)
        {
            DropObject();
            SetObjectPlaced();
            Debug.Log("Object dropped");
            //objectToDiscard = null;
            isScored = true;
            placedScore += 1;
            RemoveInitialPlace();
        }
        if (other.gameObject == targetPlace && isScored == true)
        {
            DropObject();
            SetObjectPlaced();
            placedScore += 1;
            RemoveTargetPlace();
        }
    }




}
