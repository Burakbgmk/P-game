using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float distanceToPlayer = 1f;
    [SerializeField] float speed = 0.04f;

    private float animMaxSpeed = 5.029f;
    void Start()
    {
    }

    void FixedUpdate()
    {
        MoveToPlayer();
        
    }
    void RunAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed", animMaxSpeed);
    }

    void IdleAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed", 0);
    }
    void MoveToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance > distanceToPlayer)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed);
            this.transform.LookAt(player.transform.position);
            RunAnimator();
        }
        else
        {
            IdleAnimator();
        }

    }
}
