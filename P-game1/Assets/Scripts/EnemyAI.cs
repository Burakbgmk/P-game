using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float distanceToPlayer = 1f;
    [SerializeField] float speed = 0.04f;

    GameObject player;
    MAnimation mAnimation;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mAnimation = GetComponent<MAnimation>();
    }

    void FixedUpdate()
    {
        if(player == null)
        {
            return;
        }
        MoveToPlayer();
        
    }

    void MoveToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance > distanceToPlayer)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed);
            this.transform.LookAt(player.transform.position - Vector3.up * player.transform.position.y);
            mAnimation.RunAnimator();
        }
        else
        {
            mAnimation.IdleAnimator();
        }

    }

}
