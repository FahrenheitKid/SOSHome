using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraNormal : MonoBehaviour
{
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
    }
}
