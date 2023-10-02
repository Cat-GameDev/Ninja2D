using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaremaFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed;
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, offset + target.position, Time.fixedDeltaTime*speed);
    }
}
