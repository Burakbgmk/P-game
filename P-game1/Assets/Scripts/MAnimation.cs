using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAnimation : MonoBehaviour
{
    private float animMaxSpeed = 5.029f;

    public void RunAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed", animMaxSpeed);
    }

    public void IdleAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed", 0);
    }
}
