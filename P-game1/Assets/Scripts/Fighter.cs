using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] float punchForce = 5f;
    [SerializeField] float punchAngle = 40f;
    [SerializeField] float punchDistance = 1.5f;
    [SerializeField] float punchLookDistance = 1f;

    CombatTarget combatTarget;
    GameObject[] combatTargets;

    bool isPunched;
    private float timeSinceLastHit;
    private float timeBetweenHits = 0.2f;

    void Start()
    {
        
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        if (!CanHit())
        {
            return;
        }
        GetHitObjects();
        isPunched = Input.GetKeyDown(KeyCode.Mouse0);
        if (isPunched)
        {
            LookToHit();
            Punch();
            timeSinceLastHit = 0f;
        }
        if (timeSinceLastHit > timeBetweenHits)
        {
            StopPunch();
        }
    }

    private void GetHitObjects()
    {
        GameObject[] thiefObjects = GameObject.FindGameObjectsWithTag("Thief");
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] grabObjects = GameObject.FindGameObjectsWithTag("Grabbable");
        combatTargets = new GameObject[enemyObjects.Length + grabObjects.Length + thiefObjects.Length];
        enemyObjects.CopyTo(combatTargets, 0);
        grabObjects.CopyTo(combatTargets, enemyObjects.Length);
        thiefObjects.CopyTo(combatTargets, enemyObjects.Length + grabObjects.Length);
    }
    /*
    private void TargetClosest()
    {
        float[] dist = new float[combatTargets.Length];
        int i = 0;
        combatTarget = null;
        foreach (GameObject target in combatTargets)
        {
            dist[i] = Vector3.Distance(target.transform.position, this.transform.position);
            if (combatTarget == null)
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
    }*/

    private void TargetInRange()
    {
        if (combatTargets.Length == 0)
        {
            Debug.Log("No enemy");
            return;
        }
        foreach (GameObject target in combatTargets)
        {
            if(target.GetComponent<CombatTarget>().GetDistance() < punchDistance && target.GetComponent<CombatTarget>().InRange())
            {
                combatTarget = target.GetComponent<CombatTarget>();
                combatTarget.PushEnemy();
            }
            else
            {
                target.GetComponent<CombatTarget>().isPushed = false;
                combatTarget = null;
            }
        }
    }

    private void LookToHit()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (!hasHit)
        {
            return;
        }
        if(Vector3.Distance(hit.point,this.transform.position) < punchLookDistance)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            this.transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
        }
        
    }

    private bool CanHit()
    {
        return this.GetComponent<PlayerInteraction>().objectToGrab == null;
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
        TargetInRange();
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
