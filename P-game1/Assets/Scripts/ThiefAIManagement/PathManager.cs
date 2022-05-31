using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    GameObject currentObject;
    GameObject currentTargetPlace;

    private void Start()
    {
        currentObject = null;
        currentTargetPlace = null;
    }
    public void SetCurrentTargetPlace(GameObject target)
    {
        currentTargetPlace = target;
    }
    public GameObject CurrentTargetPlace()
    {
        return currentTargetPlace;
    }
    public void SetCurrentObject(GameObject target)
    {
        currentObject = target;
    }
    public GameObject CurrentObject()
    {
        return currentObject;
    }
}
