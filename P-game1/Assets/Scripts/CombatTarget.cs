using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTarget : MonoBehaviour
{
    
    private float distance;
    Fighter fighter;
    Rigidbody enemy;
    Rigidbody player;

    void Start()
    {
        enemy = this.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, enemy.transform.position);
    }
    public float GetDistance()
    {
        return distance;
    }

    public void PushEnemy()
    {
        Vector3 punchDirection = enemy.transform.position - player.transform.position;
        enemy.AddForce(punchDirection * fighter.GetPunchForce(), ForceMode.Impulse);
        Debug.Log("Punchhed");
    }

    public bool InRange()
    {
        Vector3 targetDirection = enemy.transform.position - player.transform.position;
        Vector3 currentDirection = player.transform.forward;
        float angle = Vector3.Angle(targetDirection, currentDirection);
        Debug.Log(angle);
        if (angle < fighter.GetPunchAngle()) return true;
        else return false;

    }
}
