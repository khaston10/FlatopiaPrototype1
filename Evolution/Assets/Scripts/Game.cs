
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    int stepsPerDay;
    public bool stepForward = true;
    bool incomingMeatEater = false;
    int day = 0;
    public int availablePoints;
    public int daysBetweenIncomingMeatEaters;
    int newMeatEaters = 0;
    int incomingMeatEaterCounter = 0;
    int autoPlayCounter = 0;
    float gameSpeed = 1f;
    Vector3 cameraStartVector = new Vector3(10f, 30f, 0f);
    Quaternion cameraStartRotation;

    // Set initial limits and costs.
    public int foodCountLimit;
    public int foodCountLimitCost;
    public int plantEaterLimit;
    public int plantEaterLimitCost;
    public int worldSizeLimit;
    public int worldSizeLimitCost;
    public int plantEaterSpeedCost;

    public Text foodCountText;
    public Text foodCountLimitText;
    public Text plantEaterCountText;
    public Text EaterLimitText;
    public Text worldSizeText;
    public Text availablePointsText;
    public Text dayText;
    public Text sizeLimitText;
    public Text plantEaterSpeedText;
    public Text foodCountLimitCostText;
    public Text plantEaterLimitCostText;
    public Text worldSizeLimitCostText;
    public Text plantEaterSpeedCostText;
    public Text meatEaterNumberText;

    public GameObject controlPanel;
    public GameObject upgradePanel;
    public GameObject warningPanel;
    public GameObject autoPlayPanel;
    public GameObject light;

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
        GetComponent<CameraControler>().UpdateCameraPosition();


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

        // Set the camera rotation to start position.
        int cameraCounterInterval = 3;
        int cameraCounter = 0;
        cameraStartRotation = Quaternion.Euler(cameraStartVector);
        light.transform.rotation = cameraStartRotation;

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

            // Update rotation of light.
            cameraCounter++;
            if (cameraCounter == cameraCounterInterval)
            {
                light.transform.Rotate(Vector3.right);
                cameraCounter = 0;
            }

            yield return new WaitForSeconds(GameVariables.secondsPerStep * gameSpeed);
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

        // Check the incoming Meat Eater Counter variable and update the number of meat eaters in the next level.
        int previousDaysMeatEaters = GetComponent<MeatEater>().numberOfMeatEaters;
        incomingMeatEaterCounter += 1;

        if (daysBetweenIncomingMeatEaters == incomingMeatEaterCounter)
        {
            GetComponent<MeatEater>().numberOfMeatEaters += 1;
            incomingMeatEaterCounter = 0;
        }

        // Update the incoming meat eater bool.
        newMeatEaters = GetComponent<MeatEater>().numberOfMeatEaters - previousDaysMeatEaters;
        if (newMeatEaters > 0)
        {
            incomingMeatEater = true;
        }

        else
        {
            incomingMeatEater = false;
        }

        // Check to see if Auto Play is being used.

        if (autoPlayCounter == 0)
        {
            gameSpeed = 1;
            stepForward = false;

        }

        else
        {
            autoPlayCounter -= 1;
            StartCoroutine(UpdatePlantEaters());
            UpdatePlantEaters();
        }
        
           
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
        if (stepForward == false &&  availablePoints >= foodCountLimitCost)
        {
            foodCountLimit += 1;
            availablePoints -= foodCountLimitCost;
            foodCountLimitCost += 1;
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
        if (stepForward == false && availablePoints >= plantEaterLimitCost)
        {
            availablePoints -= plantEaterLimitCost;
            plantEaterLimit += 1;
            plantEaterLimitCost += 1;
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
        if (stepForward == false && availablePoints >= worldSizeLimitCost)
        {
            worldSizeLimit += 1;
            availablePoints -= worldSizeLimitCost;
            worldSizeLimitCost += 1;
        }
    }

    public void ClickPlantEaterSpeed()
    {
        if (stepForward == false && availablePoints >= plantEaterSpeedCost)
        {
            GetComponent<PlantEater>().speedPlantEater += 1;
            availablePoints -= plantEaterSpeedCost;
            plantEaterSpeedCost += 1;
        }
    }

    public void ClickAutoPlay5()
    {
        gameSpeed = 0.1f;
        autoPlayCounter = 5;
        ClickNextDay();
    }

    public void ClickAutoPlay10()
    {
        gameSpeed = 0.02f;
        autoPlayCounter = 10;
        ClickNextDay();
    }

    public void ClickAutoPlay20()
    {
        gameSpeed = 0.005f;
        autoPlayCounter = 20;
        ClickNextDay();
    }

    void Update()
    {
        foodCountText.text = GetComponent<GameVariables>().foodCount.ToString();
        foodCountLimitText.text = foodCountLimit.ToString();
        plantEaterCountText.text = GetComponent<GameVariables>().plantEaterCount.ToString();
        worldSizeText.text = GetComponent<GameVariables>().width.ToString();
        availablePointsText.text = availablePoints.ToString();
        EaterLimitText.text = plantEaterLimit.ToString();
        plantEaterSpeedText.text = GetComponent<PlantEater>().speedPlantEater.ToString();
        dayText.text = day.ToString();
        sizeLimitText.text = worldSizeLimit.ToString();
        foodCountLimitCostText.text = "(" + foodCountLimitCost.ToString() + ")";
        plantEaterLimitCostText.text = "(" + plantEaterLimitCost + ")";
        worldSizeLimitCostText.text = "(" + worldSizeLimitCost + ")";
        plantEaterSpeedCostText.text = "(" + plantEaterSpeedCost + ")";
        meatEaterNumberText.text = "x" + newMeatEaters.ToString();

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
        warningPanel.SetActive(false);
        autoPlayPanel.SetActive(false);

    }

    void MakeVisible()
    {
        controlPanel.SetActive(true);
        upgradePanel.SetActive(true);
        autoPlayPanel.SetActive(true);

        if (incomingMeatEater)
        {
            warningPanel.SetActive(true);
        }
    }

}
