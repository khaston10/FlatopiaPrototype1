using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Food : MonoBehaviour
{
    public Transform prefab;
    public int foodCount;
    List<Vector3> positions = new List<Vector3>();
    List<Transform> foodList = new List<Transform>();

    public void Awake()
    {
        //Debug.Log(positions.Length);

        for (int i = -(GameVariables.width) + 2; i < (GameVariables.width) - 2; i++)
        {
            for (int j = -(GameVariables.width) + 2; j < (GameVariables.width) - 2; j++)
            positions.Add(new Vector3(i, 1f, j));
        }

        // Place food objects above plane.
        PlaceFood();
    }

    public void PlaceFood()
    {
        for (int i = 0; i < foodCount; i++)
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

