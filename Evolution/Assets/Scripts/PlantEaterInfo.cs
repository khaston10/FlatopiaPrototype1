using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEaterInfo : MonoBehaviour
{
    public bool hasEaten = false;
    public bool hasReproduced = false;
    public bool isActive = true;
    public int dir = 0;
    public int dirCounter = 0;
    

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "TreeCreator_Bush_A(Clone)" && isActive)
        {
            Destroy(col.gameObject);
            hasEaten = true;
        }

        if (col.gameObject.name == "PlantEater(Clone)" && isActive)
        {
            if(gameObject.GetComponent<PlantEaterInfo>().hasReproduced == false && gameObject.GetComponent<PlantEaterInfo>().hasEaten && hasEaten)
            {
                Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
                hasReproduced = true;

            }
        }

        // Turn off physics when colliding with a baby plant eater.
        if (col.gameObject.name == "BabyPlantEater(Clone)" && isActive)
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }
}
