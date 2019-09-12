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
    int grassToPlace = GameVariables.width * 20;
    int rocksToPlace = GameVariables.width;

    public void CreateGround()
    {
        // Place and scale ground plane.
        Transform ground = Instantiate(groundPrefab);
        ground.localPosition = Vector3.zero;
        Vector3 scaleGround = new Vector3(GameVariables.width * 2, 1f, GameVariables.width * 2);
        ground.localScale = scaleGround;

        // Populate the list of positions.
        for (int i = (-GameVariables.width); i < GameVariables.width; i++)
        {
            for (int j = (-GameVariables.width); j < GameVariables.width; j++)
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


            // Remove position from the list of available.
            positions.RemoveAt(randNumForPosition);

        }

        // Create rock prefabs and place them on the ground plane.
        for (int i = 0; i < rocksToPlace; i++)
        {
            // Pick random prfab from list.
            int randNumForPrefab = Random.Range(0, rockPrefabs.Count);
            Transform t = Instantiate(rockPrefabs[randNumForPrefab]);

            // Pick a position to place prefab.
            int randNumForPosition = Random.Range(0, positions.Count);
            Vector3 prefabPos = positions[randNumForPosition];

            // Place prefab at position.
            t.localPosition = prefabPos;


            // Remove position from the list of available.
            positions.RemoveAt(randNumForPosition);

        }

        Debug.Log(positions.Count);
    }

    public void CreateClearPlanes()
    {
        // Place and scale ground plane for the -z side.
        Transform negativeZ = Instantiate(clearPlanePrefab);
        Vector3 negativeZPos = new Vector3(0f, 0f, -GameVariables.width);
        negativeZ.localPosition = negativeZPos;
        Vector3 scaleNegativeZ = new Vector3(GameVariables.width * 2, 10f, 1f);
        negativeZ.localScale = scaleNegativeZ;
        negativeZ.gameObject.GetComponent<Renderer>().enabled = false;

        // Place and scale ground plane for the z side.
        Transform positiveZ = Instantiate(clearPlanePrefab);
        Vector3 positiveZPos = new Vector3(0f, 0f, GameVariables.width);
        positiveZ.localPosition = positiveZPos;
        Vector3 scalePositiveZ = new Vector3(GameVariables.width * 2, 10f, 1f);
        positiveZ.localScale = scalePositiveZ;
        positiveZ.gameObject.GetComponent<Renderer>().enabled = false;

        // Place and scale ground plane for the -x side.
        Transform negativeX = Instantiate(clearPlanePrefab);
        Vector3 negativeXPos = new Vector3(-GameVariables.width, 0f, 0f);
        negativeX.localPosition = negativeXPos;
        Vector3 scaleNegativeX = new Vector3(1f, 10f, GameVariables.width * 2);
        negativeX.localScale = scaleNegativeX;
        negativeX.gameObject.GetComponent<Renderer>().enabled = false;

        // Place and scale ground plane for the x side.
        Transform positiveX = Instantiate(clearPlanePrefab);
        Vector3 positiveXPos = new Vector3(GameVariables.width, 0f, 0f);
        positiveX.localPosition = positiveXPos;
        Vector3 scalePositiveX = new Vector3(1f, 10f, GameVariables.width * 2);
        positiveX.localScale = scalePositiveX;
        positiveX.gameObject.GetComponent<Renderer>().enabled = false;
    }
}
