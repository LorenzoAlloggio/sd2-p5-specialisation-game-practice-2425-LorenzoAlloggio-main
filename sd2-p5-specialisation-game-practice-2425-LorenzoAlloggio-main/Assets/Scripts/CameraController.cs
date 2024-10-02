using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public Transform CameraTarget;

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(CameraTarget.position.x, CameraTarget.position.y, -10f);
    }
}
