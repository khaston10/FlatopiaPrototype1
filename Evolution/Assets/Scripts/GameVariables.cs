using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public int width;
    public static int stepsPerDay = 500;
    public static int Days = 10;
    public static float secondsPerStep = .03f;
    public int height;
    public int foodCount;
    public int plantEaterCount;

    //At start, load data from GlobalControl.
    public void GetData()
    {
        width = GameVariablesSingleton.Instance.width;
        foodCount = GameVariablesSingleton.Instance.foodCount;
        plantEaterCount = GameVariablesSingleton.Instance.plantEaterCount;
    }

    //Save data to global control   
    public void SaveData()
    {
        GameVariablesSingleton.Instance.width = width;
        GameVariablesSingleton.Instance.foodCount = foodCount;
        GameVariablesSingleton.Instance.plantEaterCount = plantEaterCount;
    }
}
