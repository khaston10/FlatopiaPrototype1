using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    int stepsPerDay;
    public Transform groundPrefab;


    void Awake()
    {
        StartCoroutine(UpdatePlantEaters());
        UpdatePlantEaters();

    }

    IEnumerator UpdatePlantEaters()
    {
        // Create ground for world.
        GetComponent<CreateGroundPlane>().CreateGround();

        // Create clear blocker planes incase objects fall off.
        GetComponent<CreateGroundPlane>().CreateClearPlanes();

        // Create local camera.
        GetComponent<CreateCamera>().CreateLocalCamera();

        for (int day = 0; day < GameVariables.Days; day++)
        {
            Debug.Log("Day: " + day);
            for (int step = 0; step < GameVariables.stepsPerDay; step++)
            {
                // Pick a direction to move plant eater.
                GetComponent<PlantEater>().UpdateDirection();

                // Pick a direction to move meat eater.
                GetComponent<MeatEater>().UpdateDirection();

                // Move plant eaters.
                GetComponent<PlantEater>().MovePlantEaters();

                // Move meat eaters.
                GetComponent<MeatEater>().MoveMeatEaters();

                yield return new WaitForSeconds(GameVariables.secondsPerStep);
            }
            // Check to see if the meat eaters have eaten. Delete the ones who have not.
            GetComponent<MeatEater>().KillUnfedMeatEaters();

            // Check to see if the plant eaters have eaten. Delete the ones who have not.
            GetComponent<PlantEater>().KillUnfedPlantEaters();

            // Count how many babies to create.
            GetComponent<PlantEater>().CountBabies();
            GetComponent<MeatEater>().CountBabies();

            // Delete all plant eaters.
            GetComponent<PlantEater>().KillAllPlantEaters();

            // Delete all meat eaters.
            GetComponent<MeatEater>().KillAllMeatEaters();

            // Spawn new plant eaters.
            GetComponent<PlantEater>().SetAvailablePositions();
            GetComponent<PlantEater>().CreateBabies();
            GetComponent<PlantEater>().CreatePlantEaters();

            // Spawn new meat eaters.
            GetComponent<MeatEater>().SetAvailablePositions();
            GetComponent<MeatEater>().CreateBabies();
            GetComponent<MeatEater>().CreateMeatEaters();

            // Spawn new food.
            GetComponent<Food>().DeleteFood();
            GetComponent<Food>().PlaceFood();
        }
           
    }

}
