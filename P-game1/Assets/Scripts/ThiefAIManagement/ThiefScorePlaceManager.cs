using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefScorePlaceManager : MonoBehaviour
{
    List<GameObject> availableScorePlaces;
    GameObject[] scorePlaces;
    GameObject currentTargetPlace;
    private void Start()
    {
        scorePlaces = GameObject.FindGameObjectsWithTag("TargetPlace");
        availableScorePlaces = new List<GameObject>();
    }
    private void Update()
    {
        FindAvailableScorePlaces();
        ChooseCurrentTargetPlace();
        this.GetComponent<PathManager>().SetCurrentTargetPlace(currentTargetPlace);

    }

    private void FindAvailableScorePlaces()
    {
        foreach (GameObject scorePlace in scorePlaces)
        {
            if (!scorePlace.GetComponent<ScorePlaceDetection>().GetIsOccupied())
            {
                availableScorePlaces.Add(scorePlace);
            }
        }
    }

    private void ChooseCurrentTargetPlace()
    {
        if(currentTargetPlace == null || currentTargetPlace.GetComponent<ScorePlaceDetection>().GetIsOccupied())
        {
            currentTargetPlace = availableScorePlaces[Random.Range(0, availableScorePlaces.Count - 1)];
        }
    }


}
