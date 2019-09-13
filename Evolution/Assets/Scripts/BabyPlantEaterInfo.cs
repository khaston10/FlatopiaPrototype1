using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPlantEaterInfo : MonoBehaviour
{
    public bool hasEaten = false;
    public bool isActive = true;
    public int dir = 0;
    public int dirCounter = 0;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "TreeCreator_Bush_A(Clone)")
        {
            Destroy(col.gameObject);
            hasEaten = true;
        }
    }
}
