using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float height = 6f;
    [SerializeField] private float speed = 1f;

    public float Height => height;
    
    private void Update()
    {
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        var inputValue = new Vector3(v, 0f, -h) * speed;
        
        Rotate(inputValue);
        Move();
    }
    
    
    public void Rotate(Vector3 val)
    {
        Quaternion qz = Quaternion.AngleAxis(val.x, transform.right);
        Quaternion qx = Quaternion.AngleAxis(val.z, transform.forward);

        transform.rotation = qx * qz * transform.rotation;
    }

    private void Move()
    {
        var pos = transform.rotation * Vector3.up * height;
        transform.position = pos;
    }
}
