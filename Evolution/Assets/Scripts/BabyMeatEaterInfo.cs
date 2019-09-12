﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMeatEaterInfo : MonoBehaviour
{
    public bool hasEaten = false;
    public int dir = 0;
    public int dirCounter = 0;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Food(Clone)")
        {
            Debug.Log("Baby Meat Eater hit food");
        }
    }
}