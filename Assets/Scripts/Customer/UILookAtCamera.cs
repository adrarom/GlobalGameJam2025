using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    private Camera mainCamera;
    public Vector3 rotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform);
            transform.Rotate(rotationOffset);
        }
    }
}
