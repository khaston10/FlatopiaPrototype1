using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatEater : MonoBehaviour
{
    public Transform meatEaterPreFab;
    public Transform babyPreFab;
    public int numberOfMeatEaters;
    public int numberOfBabyMeatEaters;
    public int numberOfIncomingMeatEaters;
    public int changeDirectionEveryNumStep;
    public float speedMeatEater;
    public float speedBabyMeatEater;
    List<Vector3> positions = new List<Vector3>();
    List<GameObject> meatEatersList = new List<GameObject>();
    List<GameObject> babyMeatEatersList = new List<GameObject>();

    public void Awake()
    {
        // Build the list of possible postions for meat eaters to start.
        SetAvailablePositions();
    }

    public void UpdateData()
    {
        positions.Clear();
        SetAvailablePositions();
    }

    public void SetAvailablePositions()
    {
        // Build the list of possible postions for meat eaters to start.
        for (int i = -(GetComponent<GameVariables>().width) +1; i <= (GetComponent<GameVariables>().width) -1; i += 2)
        {
            positions.Add(new Vector3(i, 3f, -GetComponent<GameVariables>().width + 1));
        }

        for (int i = -(GetComponent<GameVariables>().width) +1; i <= (GetComponent<GameVariables>().width) -1; i += 2)
        {
            positions.Add(new Vector3(i, 3f, GetComponent<GameVariables>().width - 1));
        }

        for (int i = -(GetComponent<GameVariables>().width) +1; i <= (GetComponent<GameVariables>().width); i += 2)
        {
            positions.Add(new Vector3(-GetComponent<GameVariables>().width + 2, 3f, i));
        }

        for (int i = -(GetComponent<GameVariables>().width) +1; i <= (GetComponent<GameVariables>().width) -1; i += 2)
        {
            positions.Add(new Vector3(GetComponent<GameVariables>().width, 3f, i));
        }
    }

    public void CreateMeatEaters()
    {
        for (int i = 0; i < numberOfMeatEaters; i++)
        {
            // Pick a random location to spawn meat eater.
            int pos = Random.Range(0, positions.Count);

            //Create meat eater at location.
            Transform t = Instantiate(meatEaterPreFab);
            t.localPosition = positions[pos];

            //Delete the position from the list of available.
            positions.RemoveAt(pos);

            //Store meat eater in a list.
            meatEatersList.Add(t.gameObject);
        }

        for (int i = 0; i < numberOfIncomingMeatEaters; i++)
        {
            // Pick a random location to spawn meat eater.
            int pos = Random.Range(0, positions.Count);

            //Create meat eater at location.
            Transform t = Instantiate(meatEaterPreFab);
            t.localPosition = positions[pos];

            //Delete the position from the list of available.
            positions.RemoveAt(pos);

            //Store meat eater in a list.
            meatEatersList.Add(t.gameObject);
        }

    }

    public void CountBabies()
    {
        numberOfBabyMeatEaters = 0;
        for (int j = 0; j < meatEatersList.Count / 2; j++)
        {
            if (meatEatersList[j].GetComponent<MeatEaterInfo>().hasReproduced)
            {
                numberOfBabyMeatEaters += 1;
            }
        }

    }

    public void CreateBabies()
    {
        for (int i = 0; i < numberOfBabyMeatEaters; i++)
        {
            // Pick a random location to spawn meat eater.
            int pos = Random.Range(0, positions.Count);

            //Create baby at location.
            Transform t = Instantiate(babyPreFab);
            t.localPosition = positions[pos];
            t.Translate(-Vector3.up * 1f);

            //Delete the position from the list of available.
            positions.RemoveAt(pos);

            //Store meat eater in a list.
            babyMeatEatersList.Add(t.gameObject);
        }

    }

    public void UpdateDirection()
    {
        for (int i = 0; i < meatEatersList.Count; i++)
        {
            if (meatEatersList[i].GetComponent<MeatEaterInfo>().dirCounter >= changeDirectionEveryNumStep)
            {
                meatEatersList[i].GetComponent<MeatEaterInfo>().dirCounter = 0;
                // Pick a direction to move meat eater.
                int direction = Random.Range(0, 4); // 0: -x, 1: x, 2: -z, 3: z.
                meatEatersList[i].GetComponent<MeatEaterInfo>().dir = direction;
            }
            meatEatersList[i].GetComponent<MeatEaterInfo>().dirCounter += 1;
        }

        for (int i = 0; i < babyMeatEatersList.Count; i++)
        {
            if (babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dirCounter >= changeDirectionEveryNumStep)
            {
                babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dirCounter = 0;
                // Pick a direction to move meat eater.
                int direction = Random.Range(0, 4); // 0: -x, 1: x, 2: -z, 3: z.
                babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dir = direction;
            }
            babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dirCounter += 1;
        }
    }

    public void MoveMeatEaters()
    {
        for (int i = 0; i < meatEatersList.Count; i++)
        {

            // Move the meat eater.
            if (meatEatersList[i].GetComponent<MeatEaterInfo>().dir == 0 && meatEatersList[i].transform.localPosition.x > -(GetComponent<GameVariables>().width) + 5)
            {
                meatEatersList[i].transform.Translate(Vector3.zero);
                meatEatersList[i].transform.Translate(-Vector3.right * speedMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = meatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, -90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }

            else if (meatEatersList[i].GetComponent<MeatEaterInfo>().dir == 1 && meatEatersList[i].transform.localPosition.x < (GetComponent<GameVariables>().width) - 5)
            {
                meatEatersList[i].transform.Translate(Vector3.zero);
                meatEatersList[i].transform.Translate(Vector3.right * speedMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = meatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, 90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }

            else if (meatEatersList[i].GetComponent<MeatEaterInfo>().dir == 2 && meatEatersList[i].transform.localPosition.z > -(GetComponent<GameVariables>().width) + 5)
            {
                meatEatersList[i].transform.Translate(Vector3.zero);
                meatEatersList[i].transform.Translate(-Vector3.forward * speedMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = meatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, 180, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }

            else if (meatEatersList[i].GetComponent<MeatEaterInfo>().dir == 3 && meatEatersList[i].transform.localPosition.z < (GetComponent<GameVariables>().width) - 5)
            {
                meatEatersList[i].transform.Translate(Vector3.zero);
                meatEatersList[i].transform.Translate(Vector3.forward * speedMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = meatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, 0, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;

            }
        }

        for (int i = 0; i < babyMeatEatersList.Count; i++)
        {
            // Move the meat eater.
            if (babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dir == 0 && babyMeatEatersList[i].transform.localPosition.x > -(GetComponent<GameVariables>().width) + 5)
            {
                babyMeatEatersList[i].transform.Translate(Vector3.zero);
                babyMeatEatersList[i].transform.Translate((-Vector3.right) * speedBabyMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = babyMeatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, -90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }
            else if (babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dir == 1 && babyMeatEatersList[i].transform.localPosition.x < (GetComponent<GameVariables>().width) - 5)
            {
                babyMeatEatersList[i].transform.Translate(Vector3.zero);
                babyMeatEatersList[i].transform.Translate((Vector3.right) * speedBabyMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = babyMeatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, 90, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }
            else if (babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dir == 2 && babyMeatEatersList[i].transform.localPosition.z > -(GetComponent<GameVariables>().width) + 5)
            {
                babyMeatEatersList[i].transform.Translate(Vector3.zero);
                babyMeatEatersList[i].transform.Translate((-Vector3.forward) * speedBabyMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = babyMeatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, 180, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }
            else if (babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().dir == 3 && babyMeatEatersList[i].transform.localPosition.z < (GetComponent<GameVariables>().width) - 5)
            {
                babyMeatEatersList[i].transform.Translate(Vector3.zero);
                babyMeatEatersList[i].transform.Translate((Vector3.forward) * speedBabyMeatEater);

                // Rotate the meat eater into the correct direction.
                Transform eater = babyMeatEatersList[i].GetComponentInChildren<Transform>().Find("meatEater4");
                Vector3 rotationVector = new Vector3(0, 0, 0);
                Quaternion rotation = Quaternion.Euler(rotationVector);
                eater.rotation = rotation;
            }

        }
    }

    public void KillUnfedMeatEaters()
    {
        for (int i = meatEatersList.Count - 1; i >= 0; i--)
        {
            if (meatEatersList[i].GetComponent<MeatEaterInfo>().hasEaten == false)
            {
                Destroy(meatEatersList[i]);
                meatEatersList.RemoveAt(i);
                numberOfMeatEaters -= 1;
            }
        }

        for (int i = babyMeatEatersList.Count - 1; i >= 0; i--)
        {
            if (babyMeatEatersList[i].GetComponent<BabyMeatEaterInfo>().hasEaten == false)
            {
                Destroy(babyMeatEatersList[i]);
                babyMeatEatersList.RemoveAt(i);
            }
        }
    }

    public void KillAllMeatEaters()
    {
        numberOfMeatEaters = 0;
        for (int i = 0; i < meatEatersList.Count; i++)
        {
            numberOfMeatEaters += 1;
            Destroy(meatEatersList[i]);
        }
        meatEatersList.Clear();

        for (int i = 0; i < babyMeatEatersList.Count; i++)
        {
            numberOfMeatEaters += 1;
            Destroy(babyMeatEatersList[i]);
        }
        babyMeatEatersList.Clear();
    }
}
