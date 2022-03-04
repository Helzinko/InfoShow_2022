using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;

    private Vector3 gameCenterPos = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(Grid.gridXLenght / 2, 0, Grid.gridZLenght / 2);
    }

    private void Update()
    {
        var inputRot = Input.GetAxis("Horizontal");

        if(inputRot != 0)
            transform.Rotate(0, rotationSpeed * -inputRot * Time.deltaTime, 0);
    }
}
