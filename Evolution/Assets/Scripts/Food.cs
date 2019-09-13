using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Food : MonoBehaviour
{
    public Transform prefab;
    List<Vector3> positions = new List<Vector3>();
    List<Transform> foodList = new List<Transform>();

    public void SetUp()
    {
        // Fill the position list with available positions.
        for (int i = -(GetComponent<GameVariables>().width) + 2; i < (GetComponent<GameVariables>().width) - 2; i++)
        {
            for (int j = -(GetComponent<GameVariables>().width) + 2; j < (GetComponent<GameVariables>().width) - 2; j++)
            positions.Add(new Vector3(i, 1f, j));
        }

        // Place food objects above plane.
        PlaceFood();
    }

    public void UpdateData()
    {
        positions.Clear();

        // Fill the position list with available positions.
        for (int i = -(GetComponent<GameVariables>().width) + 2; i < (GetComponent<GameVariables>().width) - 2; i++)
        {
            for (int j = -(GetComponent<GameVariables>().width) + 2; j < (GetComponent<GameVariables>().width) - 2; j++)
                positions.Add(new Vector3(i, 1f, j));
        }

    }

    public void PlaceFood()
    {
        for (int i = 0; i < GetComponent<GameVariables>().foodCount; i++)
        {
            // Pick a random position.
            int pos = Random.Range(0, positions.Count);
            
            // Create a food object and place it in a random location.
            Transform t = Instantiate(prefab);
            t.localPosition = positions[pos];

            // Delete the location for the position array.
            positions.RemoveAt(pos);
            foodList.Add(t);
        }
    }

    public void DeleteFood()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            if (foodList[i] != null)
            {
                Destroy(foodList[i].gameObject);
            }
        }
        foodList.Clear();
    }
}

