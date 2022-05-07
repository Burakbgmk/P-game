using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isGrabbed;
    public bool isThiefGrabbed;

    GameObject player;
    GameObject thief;
    GameObject[] thieves;
    ThiefAI thiefAI;

    void Start()
    {
        isGrabbed = false;
        player = GameObject.FindGameObjectWithTag("Player");
        isThiefGrabbed = false;
        
    }

    void Update()
    {
        thieves = GameObject.FindGameObjectsWithTag("Thief");
        foreach (var thiefClone in thieves)
        {
            if(thiefClone.transform.position.y > -10f)
            {
                thief = thiefClone;
                thiefAI = thief.GetComponent<ThiefAI>();
            }
        }
        if(this.GetComponent<ThiefScoreObject>() != null)
        {
            GrabbedByThief();
        }
        if (this.GetComponent<PlayerScoreObject>() != null)
        {
            GrabbedByPlayer();
        }
        
    }

    private void GrabbedByPlayer()
    {
        if (isGrabbed)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = player.transform.position + player.GetComponent<ThirdPersonMovement>().movingDistance + player.transform.forward * 1.5f + Vector3.up;
        }
        else if (!isGrabbed) this.GetComponent<Rigidbody>().useGravity = true;
    }

    private void GrabbedByThief()
    {
        if (thief == null)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            return;
        }
        if (isGrabbed)
        {
            return;
        }
        if (isThiefGrabbed)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = thief.transform.position + thief.GetComponent<ThiefAI>().velocity + thief.transform.forward * 1.5f + Vector3.up;
        }
        else if (!isThiefGrabbed) this.GetComponent<Rigidbody>().useGravity = true;


    }



}
