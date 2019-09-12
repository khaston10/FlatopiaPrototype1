using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform camera;
    Vector3 position;
    Vector3 rotationVector;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(0f, GameVariables.width, GameVariables.width * 2);
        Vector3 rotationVector = new Vector3(0, 90, 0);
        camera.localPosition = position;
        Quaternion rotation = Quaternion.Euler(rotationVector);
        camera.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
