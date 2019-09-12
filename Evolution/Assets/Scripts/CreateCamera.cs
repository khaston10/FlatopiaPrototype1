using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCamera : MonoBehaviour
{
    public GameObject camera;

    public void CreateLocalCamera()
    {
        Instantiate(camera);
    }
}
