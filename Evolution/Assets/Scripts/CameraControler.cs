using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform camera;
    Vector3 position;
    Vector3 rotationVector;
    Transform t;

    // Start is called before the first frame update
    public void SetUp()
    {
        t = Instantiate(camera);
        Vector3 position = new Vector3(0f, GetComponent<GameVariables>().width, GetComponent<GameVariables>().width);
        Vector3 rotationVector = new Vector3(0, 90, 0);
        t.localPosition = position;
        Quaternion rotation = Quaternion.Euler(rotationVector);
        t.rotation = rotation;
    }

    public void UpdateCameraPosition()
    {
        Vector3 position = new Vector3(0f, GetComponent<GameVariables>().width, GetComponent<GameVariables>().width * 2);
        Vector3 rotationVector = new Vector3(0, 90, 0);
        t.localPosition = position;
        Quaternion rotation = Quaternion.Euler(rotationVector);
        t.rotation = rotation;
    }


}
