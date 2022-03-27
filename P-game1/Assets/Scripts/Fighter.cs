using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //[SerializeField] Rigidbody enemy;
    
    [SerializeField] float punchForce = 5f;
    [SerializeField] float punchDistance = 1.5f;
    [SerializeField] float punchAngle = 40f;

    Rigidbody enemy;

    bool isPunched;
    private float timeSinceLastHit;
    private float timeBetweenHits = 0.2f;
    private float distance;

    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Rigidbody>();
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        distance = Vector3.Distance(transform.position, enemy.transform.position);
        isPunched = Input.GetKeyDown(KeyCode.Mouse0);
        if (isPunched)
        {
            LookToHit();
            Punch();
            timeSinceLastHit = 0f;
        }
        if(timeSinceLastHit > timeBetweenHits)
        {
            StopPunch();
        }
        


    }



    private void LookToHit()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                this.transform.LookAt(hit.point);
            }
        }

        
    }

    private void Punch()
    {
        GetComponent<Animator>().ResetTrigger("stoppunch");
        GetComponent<Animator>().SetTrigger("dopunch");
    }
    public void StopPunch()
    {
        GetComponent<Animator>().ResetTrigger("dopunch");
        GetComponent<Animator>().SetTrigger("stoppunch");
        
    }

    void Hit()
    {

        if (timeSinceLastHit < timeBetweenHits)
        {
            return;
        }
        if (distance < punchDistance && InRange())
        {
            PushEnemy();
        }
        else return;
        
    }

    private void PushEnemy()
    {
        Vector3 punchDirection = enemy.transform.position - this.transform.position;
        enemy.AddForce(punchDirection * punchForce, ForceMode.Impulse);
        Debug.Log("Punchhed");
    }

    private bool InRange()
    {
        Vector3 targetDirection = enemy.transform.position - this.transform.position;
        Vector3 currentDirection = this.transform.forward;
        float angle = Vector3.Angle(targetDirection, currentDirection);
        Debug.Log(angle);
        if(angle < punchAngle) return true;
        else return false;

    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }




}
