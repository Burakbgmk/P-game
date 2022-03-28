using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [SerializeField] float punchForce = 5f;
    [SerializeField] float punchAngle = 40f;
    [SerializeField] float punchDistance = 1.5f;
    
    CombatTarget combatTarget;
    GameObject[] combatTargets;

    bool isPunched;
    private float timeSinceLastHit;
    private float timeBetweenHits = 0.2f;
    

    void Start()
    {
        combatTargets = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void TargetClosest()
    {
        float[] dist = new float[combatTargets.Length];
        int i=0;
        combatTarget = null;
        foreach (GameObject target in combatTargets)
        {
            dist[i] = Vector3.Distance(target.transform.position, this.transform.position);
            if(combatTarget == null)
            {
                combatTarget = target.GetComponent<CombatTarget>();
                i += 1;
                continue;
            }
            else if (dist[i] < dist[i - 1])
            {
                combatTarget = target.GetComponent<CombatTarget>();
            }
            i += 1;
        }
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        TargetClosest();
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
        if (combatTarget.GetDistance() < punchDistance && combatTarget.InRange())
        {
            combatTarget.PushEnemy();
        }
        else return;
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public float GetPunchForce()
    {
        return punchForce;
    }

    public float GetPunchAngle()
    {
        return punchAngle;
    }

}
