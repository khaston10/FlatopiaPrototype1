using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameSetUp : MonoBehaviour
{
    public int width;
    public int height;
    public int availablePoints;
    public int foodCount;
    public int plantEaterCount;

    public Text availablePointsText;
    public Text worldSizeInt;
    public Text foodCountInt;
    public Text plantEaterCountInt;

    public List<Transform> prefabs;

    //At start, load data from GlobalControl.
    void Start()
    {
        width = GameVariablesSingleton.Instance.width;
        foodCount = GameVariablesSingleton.Instance.foodCount;
        plantEaterCount = GameVariablesSingleton.Instance.plantEaterCount;

        Transform t = Instantiate(prefabs[0]);
        t.localPosition = Vector3.zero;
        t.localScale = (Vector3.one * 0.1f);
    }

    //Save data to global control   
    public void SaveData()
    {
        GameVariablesSingleton.Instance.width = width;
        GameVariablesSingleton.Instance.foodCount = foodCount;
        GameVariablesSingleton.Instance.plantEaterCount = plantEaterCount;
    }

    public void ClickStart()
    {
        SaveData();
        SceneManager.LoadScene("01");
    }

    public void ClickFoodCountPlus()
    {
        if (availablePoints > 0)
        {
            foodCount += 1;
            availablePoints -= 1;
        }
    }

    public void ClickFoodCountMinus()
    {
        if (foodCount > 5)
        {
            foodCount -= 1;
            availablePoints += 1;
        }
    }

    public void ClickWorldSizePlus()
    {
        if (availablePoints > 0)
        {
            width += 1;
            availablePoints -= 1;
        }  
    }

    public void ClickWorldSizeMinus()
    {
        if (width > 10)
        {
            width -= 1;
            availablePoints += 1;
        }
    }

    public void ClickPlantEartCountPlus()
    {
        if (availablePoints > 0)
        {
            plantEaterCount += 1;
            availablePoints -= 1;
        }
    }

    public void ClickPlantEaterCountMinus()
    {
        if (plantEaterCount > 1)
        {
            plantEaterCount -= 1;
            availablePoints += 1;
        }
    }

    public void Update()
    {
        availablePointsText.text = availablePoints.ToString();
        worldSizeInt.text = width.ToString();
        foodCountInt.text = foodCount.ToString();
        plantEaterCountInt.text = plantEaterCount.ToString();
    }

}
