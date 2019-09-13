using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGroundPlane : MonoBehaviour
{
    public List<Transform> grassPrefabs;
    public List<Transform> rockPrefabs;
    public Transform groundPrefab;
    public Transform clearPlanePrefab;
    public List<Vector3> positions;
    int grassToPlace = 100;
    int rocksToPlace = 100;
    public List<Transform> walls;
    public List<Transform> grass;
    public List<Transform> rocks;

    public void CreateGround()
    {
        // Place and scale ground plane.
        Transform ground = Instantiate(groundPrefab);
        ground.localPosition = Vector3.zero;
        Vector3 scaleGround = new Vector3(GetComponent<GameVariables>().width * 2, 1f, GetComponent<GameVariables>().width * 2);
        ground.localScale = scaleGround;

        // Populate the list of positions.
        for (int i = (-GetComponent<GameVariables>().width); i < GetComponent<GameVariables>().width; i++)
        {
            for (int j = (-GetComponent<GameVariables>().width); j < GetComponent<GameVariables>().width; j++)
            {
                Vector3 pos = new Vector3(i, 0f, j);
                positions.Add(pos);
            }
        }

        // Create grass prefabs and place them on the ground plane.
        for (int i = 0; i < grassToPlace; i++)
        {
            // Pick random prfab from list.
            int randNumForPrefab = Random.Range(0, grassPrefabs.Count);
            Transform t = Instantiate(grassPrefabs[randNumForPrefab]);

            // Pick a position to place prefab.
            int randNumForPosition = Random.Range(0, positions.Count);
            Vector3 prefabPos = positions[randNumForPosition];

            // Place prefab at position.
            t.localPosition = prefabPos;
            grass.Add(t);


            // Remove position from the list of available.
            positions.RemoveAt(randNumForPosition);

        }

        // Create rock prefabs and place them on the ground plane.
        for (int i = 0; i < rocksToPlace; i++)
        {
            // Pick random prefab from list.
            int randNumForPrefab = Random.Range(0, rockPrefabs.Count);
            Transform t = Instantiate(rockPrefabs[randNumForPrefab]);

            // Pick a position to place prefab.
            int randNumForPosition = Random.Range(0, positions.Count);
            Vector3 prefabPos = positions[randNumForPosition];

            // Pick a rotation.
            int randNumForRotation = Random.Range(0, 360);
            Vector3 vectorForRotation = new Vector3(0f, randNumForRotation, 0f);

            // Pick a scale.
            float randNumForScale = Random.Range(0.1f, 1f);


            // Place prefab at position.
            t.localPosition = prefabPos;
            rocks.Add(t);

            // Rotate prefab.
            t.Rotate(vectorForRotation);

            // Set scale for prefab.
            t.localScale = Vector3.one * randNumForScale;


            // Remove position from the list of available.
            positions.RemoveAt(randNumForPosition);

        }

    }

    public void CreateClearPlanes()
    {
        // Place and scale ground plane for the -z side.
        Transform negativeZ = Instantiate(clearPlanePrefab);
        Vector3 negativeZPos = new Vector3(0f, 0f, -GetComponent<GameVariables>().width);
        negativeZ.localPosition = negativeZPos;
        Vector3 scaleNegativeZ = new Vector3(GetComponent<GameVariables>().width * 2, 10f, 1f);
        negativeZ.localScale = scaleNegativeZ;
        negativeZ.gameObject.GetComponent<Renderer>().enabled = false;
        walls.Add(negativeZ);

        // Place and scale ground plane for the z side.
        Transform positiveZ = Instantiate(clearPlanePrefab);
        Vector3 positiveZPos = new Vector3(0f, 0f, GetComponent<GameVariables>().width);
        positiveZ.localPosition = positiveZPos;
        Vector3 scalePositiveZ = new Vector3(GetComponent<GameVariables>().width * 2, 10f, 1f);
        positiveZ.localScale = scalePositiveZ;
        positiveZ.gameObject.GetComponent<Renderer>().enabled = false;
        walls.Add(positiveZ);

        // Place and scale ground plane for the -x side.
        Transform negativeX = Instantiate(clearPlanePrefab);
        Vector3 negativeXPos = new Vector3(-GetComponent<GameVariables>().width, 0f, 0f);
        negativeX.localPosition = negativeXPos;
        Vector3 scaleNegativeX = new Vector3(1f, 10f, GetComponent<GameVariables>().width * 2);
        negativeX.localScale = scaleNegativeX;
        negativeX.gameObject.GetComponent<Renderer>().enabled = false;
        walls.Add(negativeX);

        // Place and scale ground plane for the x side.
        Transform positiveX = Instantiate(clearPlanePrefab);
        Vector3 positiveXPos = new Vector3(GetComponent<GameVariables>().width, 0f, 0f);
        positiveX.localPosition = positiveXPos;
        Vector3 scalePositiveX = new Vector3(1f, 10f, GetComponent<GameVariables>().width * 2);
        positiveX.localScale = scalePositiveX;
        positiveX.gameObject.GetComponent<Renderer>().enabled = false;
        walls.Add(positiveX);
    }

    public void DeleteWalls()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            Destroy(walls[i].gameObject);
            
        }
        walls.Clear();

        for(int j = 0; j < grass.Count; j++)
        {
            Destroy(grass[j].gameObject);

        }
        grass.Clear();

        for(int k = 0; k < rocks.Count; k++)
        {
            Destroy(rocks[k].gameObject);

        }
        rocks.Clear();
    }
}
