using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    int stepsPerDay;
    public bool stepForward = true;
    int day = 0;
    public int availablePoints;

    // Set initial limits.
    public int foodCountLimit;
    public int plantEaterLimit;
    public int worldSizeLimit;

    public Text foodCountText;
    public Text foodCountLimitText;
    public Text plantEaterCountText;
    public Text EaterLimitText;
    public Text worldSizeText;
    public Text availablePointsText;
    public Text dayText;
    public Text sizeLimitText;

    public GameObject controlPanel;
    public GameObject upgradePanel;

    void Awake()
    {
        GetComponent<GameVariables>().GetData();
        GetComponent<PlantEater>().SetUp();
        GetComponent<CameraControler>().SetUp();

        StartCoroutine(UpdatePlantEaters());
        UpdatePlantEaters();

    }

    IEnumerator UpdatePlantEaters()
    {
        // Update  the day.
        day += 1;
        Debug.Log("Day: " + day);

        // Update all scripts with new data.
        GetComponent<Food>().SetUp();
        GetComponent<PlantEater>().UpdateData();
        GetComponent<MeatEater>().UpdateData();


        // Create clear blocker planes incase objects fall off.
        GetComponent<CreateGroundPlane>().DeleteWalls();
        GetComponent<CreateGroundPlane>().CreateClearPlanes();

        // Create ground for world.
        GetComponent<CreateGroundPlane>().CreateGround();

        // Spawn new meat eaters.
        GetComponent<MeatEater>().SetAvailablePositions();
        GetComponent<MeatEater>().CreateBabies();
        GetComponent<MeatEater>().CreateMeatEaters();

        // Spawn new plant eaters.
        GetComponent<PlantEater>().SetAvailablePositions();
        GetComponent<PlantEater>().CreateBabies();
        GetComponent<PlantEater>().CreatePlantEaters();

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

        // Spawn new food.
        GetComponent<Food>().DeleteFood();

        // Reset aviable points. THe player is rewarded for having plant eaters survive the round.
        availablePoints += GetComponent<GameVariables>().plantEaterCount;

        stepForward = false;
           
    }

    public void ClickNextDay()
    {
        if (stepForward == false)
        {
            stepForward = true;
            StartCoroutine(UpdatePlantEaters());
            UpdatePlantEaters();
        }
    }

    public void ClickFoodCount()
    {
        if (stepForward == false && GetComponent<GameVariables>().foodCount < foodCountLimit && availablePoints > 0)
        {
            GetComponent<GameVariables>().foodCount += 1;
            availablePoints -= 1;
        }
    }

    public void ClickFoodLimit()
    {
        if (stepForward == false &&  availablePoints > 5)
        {
            foodCountLimit += 5;
            availablePoints -= 5;
        }
    }

    public void ClickPlantEaters()
    {
        if (stepForward == false && availablePoints > 0 && GetComponent<GameVariables>().plantEaterCount < plantEaterLimit)
        {
            GetComponent<GameVariables>().plantEaterCount += 1;
            availablePoints -= 1;
        }
    }

    public void ClickEaterLimit()
    {
        if (stepForward == false && availablePoints > 5)
        {
            plantEaterLimit  += 5;
            availablePoints -= 5;
        }
    }

    public void ClickWorldSize()
    {
        if (stepForward == false && GetComponent<GameVariables>().width < worldSizeLimit && availablePoints > 0)
        {
            GetComponent<GameVariables>().width += 1;
            availablePoints -= 1;
        }
    }

    public void ClickWorldSizeLimit()
    {
        if (stepForward == false && availablePoints > 5)
        {
            worldSizeLimit += 5;
            availablePoints -= 5;
        }
    }

    void Update()
    {
        foodCountText.text = GetComponent<GameVariables>().foodCount.ToString();
        foodCountLimitText.text = foodCountLimit.ToString();
        plantEaterCountText.text = GetComponent<GameVariables>().plantEaterCount.ToString();
        worldSizeText.text = GetComponent<GameVariables>().width.ToString();
        availablePointsText.text = availablePoints.ToString();
        EaterLimitText.text = plantEaterLimit.ToString();
        dayText.text = day.ToString();
        sizeLimitText.text = worldSizeLimit.ToString();

        if (stepForward)
        {
            MakeInvisible();
        }

        if (stepForward == false)
        {
            MakeVisible();
        }
    }

    void MakeInvisible()
    {
        controlPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }

    void MakeVisible()
    {
        controlPanel.SetActive(true);
        upgradePanel.SetActive(true);
    }

}
