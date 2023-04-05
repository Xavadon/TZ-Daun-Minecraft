using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _lookAt;
    [SerializeField] private float _offsetY = 5f;
    [SerializeField] private float _offsetZ = 6f;
    [SerializeField] private float _rotationX = 38f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(_rotationX, 0, 0);
    }

    private void Update()
    {
        transform.position = _lookAt.position + Vector3.up * _offsetY + Vector3.back * _offsetZ;
    }
}
