using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform trnsfr;
    public Vector3 cameraPos;

    void Update()
    {
        this.transform.position = trnsfr.position + cameraPos;
    }
}
