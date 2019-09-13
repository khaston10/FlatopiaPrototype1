using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatEaterInfo : MonoBehaviour
{
    public bool hasEaten = false;
    public bool hasReproduced = false;
    public int dir = 0;
    public int dirCounter = 0;
    public int foodRequiredToSurvive;
    public int foodEaten = 0;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "PlantEater(Clone)" && col.gameObject.GetComponent<PlantEaterInfo>().isActive)
        {

            Transform plantEater3 = col.gameObject.GetComponentInChildren<Transform>().Find("plantEater3");
            Transform body = plantEater3.gameObject.GetComponentInChildren<Transform>().Find("Body");

            body.gameObject.GetComponent<Renderer>().enabled = false;


            //col.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(col.gameObject.GetComponent<Rigidbody>());
            Destroy(col.gameObject.GetComponent<Collider>());
            col.gameObject.GetComponent<PlantEaterInfo>().isActive = false;
            foodEaten += 2;

            if (foodEaten >= foodRequiredToSurvive)
            {
                hasEaten = true;
            }
        }

        if(col.gameObject.name == "BabyPlantEater(Clone)" && col.gameObject.GetComponent<BabyPlantEaterInfo>().isActive)
        {
            Transform plantEater3 = col.gameObject.GetComponentInChildren<Transform>().Find("plantEater3");
            Transform body = plantEater3.gameObject.GetComponentInChildren<Transform>().Find("Body");

            body.gameObject.GetComponent<Renderer>().enabled = false;


            //col.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(col.gameObject.GetComponent<Rigidbody>());
            Destroy(col.gameObject.GetComponent<Collider>());
            col.gameObject.GetComponent<BabyPlantEaterInfo>().isActive = false;
            foodEaten += 1;

            if (foodEaten >= foodRequiredToSurvive)
            {
                hasEaten = true;
            }
        }

        if (col.gameObject.name == "TreeCreator_Bush_A(Clone)")
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (col.gameObject.name == "MeatEater(Clone)")
        {
            if (gameObject.GetComponent<MeatEaterInfo>().hasReproduced == false && gameObject.GetComponent<MeatEaterInfo>().hasEaten && hasEaten)
            {
                Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
                hasReproduced = true;
            }
        }

    }

}
