using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float scale = 0.1f;
    
    void Update()
    {
        var qxAngle = Mathf.Sin(Time.time * speed) * scale;
        var qzAngle = Mathf.Cos(Time.time * speed) * scale;
        var qx = Quaternion.AngleAxis(qxAngle, Vector3.right);
        var qz = Quaternion.AngleAxis(qzAngle, Vector3.forward);
        transform.localRotation = qx * (qz * transform.localRotation);
    }
}
