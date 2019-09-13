using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEater : MonoBehaviour
{
    public Transform plantEaterPreFab;
    public Transform babyPreFab;
    public int numberOfBabyPlantEaters;
    public int changeDirectionEveryNumStep;
    public float speedPlantEater;
    public float speedBabyPlantEater;
    List<Vector3> positions = new List<Vector3>();
    List<GameObject> plantEatersList = new List<GameObject>();
    List<GameObject> babyPlantEatersList = new List<GameObject>();

    public void SetUp()
    {
        // Build the list of possible postions for plant eaters to start.
        SetAvailablePositions();

        // Create plant eaters to start.
        CreatePlantEaters();

        // Create baby plant eaters to start.
        CreateBabies();
        GetComponent<GameVariables>().plantEaterCount = 0;

    }

    public void UpdateData()
    {
        positions.Clear();

        // Build the list of possible postions for plant eaters to start.
        SetAvailablePositions();
    }

    public void SetAvailablePositions()
    {
        // Build the list of possible postions for plant eaters to start.
        for (int i = -(GetComponent<GameVariables>().width) + 3; i <= (GetComponent<GameVariables>().width) - 3; i += 2)
        {
            positions.Add(new Vector3(i, 1f, - ((GetComponent<GameVariables>().width)-5)));
        }

        for (int i = -(GetComponent<GameVariables>().width) + 3; i <= (GetComponent<GameVariables>().width) - 3; i += 2)
        {
            positions.Add(new Vector3(i, 1f, ((GetComponent<GameVariables>().width) - 5)));
        }

        for (int i = -(GetComponent<GameVariables>().width) + 3; i <= (GetComponent<GameVariables>().width) - 3; i += 2)
        {
            positions.Add(new Vector3((-GetComponent<GameVariables>().width) + 5, 1f, i));
        }

        for (int i = -(GetComponent<GameVariables>().width) + 3; i <= (GetComponent<GameVariables>().width) - 3; i += 2)
        {
            positions.Add(new Vector3(GetComponent<GameVariables>().width - 3 , 1f, i));
        }
    }

    public void CreatePlantEaters()
    {
        for (int i = 0; i < GetComponent<GameVariables>().plantEaterCount; i++)
        {
            // Pick a random location to spawn plant eater.
            int pos = Random.Range(0, positions.Count);

            //Create plant eater at location.
            Transform t = Instantiate(plantEaterPreFab);
            t.localPosition = positions[pos];

            //Delete the position from the list of available.
            positions.RemoveAt(pos);

            //Store plant eater in a list.
            plantEatersList.Add(t.gameObject);
        }

    }

    public void CountBabies()
    {
        numberOfBabyPlantEaters = 0;
        for (int j = 0; j < plantEatersList.Count; j++)
        {
            if (plantEatersList[j].GetComponent<PlantEaterInfo>().hasReproduced)
            {
                numberOfBabyPlantEaters += 1;
            }
        }

    }

    public void CreateBabies()
    {
        for (int i = 0; i < numberOfBabyPlantEaters / 2; i++)
        {
            // Pick a random location to spawn plant eater.
            int pos = Random.Range(0, positions.Count);

            //Create baby at location.
            Transform t = Instantiate(babyPreFab);
            t.localPosition = positions[pos];
            t.Translate(-Vector3.up * 0.5f);

            //Delete the position from the list of available.
            positions.RemoveAt(pos);

            //Store plant eater in a list.
            babyPlantEatersList.Add(t.gameObject);
        }

    }

    public void UpdateDirection()
    {
        for (int i = 0; i < plantEatersList.Count; i++)
        {
            if ( plantEatersList[i].GetComponent<PlantEaterInfo>().dirCounter >= changeDirectionEveryNumStep)
            {
                plantEatersList[i].GetComponent<PlantEaterInfo>().dirCounter = 0;

                // Pick a direction to move plant eater.
                int direction = Random.Range(0, 4); // 0: -x, 1: x, 2: -z, 3: z.
                plantEatersList[i].GetComponent<PlantEaterInfo>().dir = direction;

                
            }
            plantEatersList[i].GetComponent<PlantEaterInfo>().dirCounter += 1;
        }

        for (int i = 0; i < babyPlantEatersList.Count; i++)
        {
            if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dirCounter >= changeDirectionEveryNumStep)
            {
                babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dirCounter = 0;
                // Pick a direction to move plant eater.
                int direction = Random.Range(0, 4); // 0: -x, 1: x, 2: -z, 3: z.
                babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dir = direction;
            }
            babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dirCounter += 1;
        }
    }

    public void MovePlantEaters()
    {
        for (int i = 0; i < plantEatersList.Count; i++)
        {

            // Move the plant eater.
            if (plantEatersList[i].GetComponent<PlantEaterInfo>().dir == 0 && plantEatersList[i].transform.localPosition.x > -(GetComponent<GameVariables>().width) + 5)
            {
                plantEatersList[i].transform.Translate(Vector3.zero);
                plantEatersList[i].transform.Translate(-Vector3.right * speedPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = plantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, -90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }

            else if (plantEatersList[i].GetComponent<PlantEaterInfo>().dir == 1 && plantEatersList[i].transform.localPosition.x < (GetComponent<GameVariables>().width) - 5)
            {
                plantEatersList[i].transform.Translate(Vector3.zero);
                plantEatersList[i].transform.Translate(Vector3.right * speedPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = plantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, 90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }

            else if (plantEatersList[i].GetComponent<PlantEaterInfo>().dir == 2 && plantEatersList[i].transform.localPosition.z > -(GetComponent<GameVariables>().width) + 5)
            {
                plantEatersList[i].transform.Translate(Vector3.zero);
                plantEatersList[i].transform.Translate(-Vector3.forward * speedPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = plantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, 180, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }

            else if (plantEatersList[i].GetComponent<PlantEaterInfo>().dir == 3 && plantEatersList[i].transform.localPosition.z < (GetComponent<GameVariables>().width) - 5)
            {
                plantEatersList[i].transform.Translate(Vector3.zero);
                plantEatersList[i].transform.Translate(Vector3.forward * speedPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = plantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, 0, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }
        }

        for (int i = 0; i < babyPlantEatersList.Count; i++)
        {
            // Move the plant eater.
            if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dir == 0 && babyPlantEatersList[i].transform.localPosition.x > -(GetComponent<GameVariables>().width) + 5)
            {
                babyPlantEatersList[i].transform.Translate(Vector3.zero);
                babyPlantEatersList[i].transform.Translate((-Vector3.right) * speedBabyPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = babyPlantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, -90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }
            else if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dir == 1 && babyPlantEatersList[i].transform.localPosition.x < (GetComponent<GameVariables>().width) - 5)
            {
                babyPlantEatersList[i].transform.Translate(Vector3.zero);
                babyPlantEatersList[i].transform.Translate((Vector3.right) * speedBabyPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = babyPlantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, 90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }
            else if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dir == 2 && babyPlantEatersList[i].transform.localPosition.z > -(GetComponent<GameVariables>().width) + 5)
            {
                babyPlantEatersList[i].transform.Translate(Vector3.zero);
                babyPlantEatersList[i].transform.Translate((-Vector3.forward) * speedBabyPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = babyPlantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, 180, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }
            else if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().dir == 3 && babyPlantEatersList[i].transform.localPosition.z < (GetComponent<GameVariables>().width) - 5)
            {
                babyPlantEatersList[i].transform.Translate(Vector3.zero);
                babyPlantEatersList[i].transform.Translate((Vector3.forward) * speedBabyPlantEater);

                // Rotate the plant eater into the correct direction.
                Transform eater = babyPlantEatersList[i].GetComponentInChildren<Transform>().Find("plantEater3");
                Vector3 rotationVector = new Vector3(0, 0, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }

        }
    }

    public void KillUnfedPlantEaters()
    {
        for (int i = plantEatersList.Count - 1; i >= 0; i--)
        {
            if (plantEatersList[i].GetComponent<PlantEaterInfo>().hasEaten == false)
            {
                plantEatersList[i].GetComponent<PlantEaterInfo>().isActive = false;
            }

            if (plantEatersList[i].GetComponent<PlantEaterInfo>().isActive == false)
            {
                Destroy(plantEatersList[i]);
                plantEatersList.RemoveAt(i);
                GetComponent<GameVariables>().plantEaterCount -= 1;
            }
        }

        for (int i = babyPlantEatersList.Count - 1; i >= 0; i--)
        {
            if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().hasEaten == false)
            {
                babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().isActive = false;
            }

            if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().hasEaten == false)
            {
                Destroy(babyPlantEatersList[i]);
                babyPlantEatersList.RemoveAt(i);
            }
        }
    }

    public void KillAllPlantEaters()
    {
        GetComponent<GameVariables>().plantEaterCount = 0;
        for (int i = 0; i < plantEatersList.Count; i++)
        {
            if (plantEatersList[i].GetComponent<PlantEaterInfo>().isActive)
            {
                GetComponent<GameVariables>().plantEaterCount += 1;
            }
            Destroy(plantEatersList[i]);
        }
        plantEatersList.Clear();

        for (int i = 0; i < babyPlantEatersList.Count; i++)
        {
            if (babyPlantEatersList[i].GetComponent<BabyPlantEaterInfo>().isActive)
            {
                GetComponent<GameVariables>().plantEaterCount += 1;
            }
            Destroy(babyPlantEatersList[i]);
        }
        babyPlantEatersList.Clear();
    }

}
