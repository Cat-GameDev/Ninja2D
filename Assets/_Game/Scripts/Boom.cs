using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private  Vector3 LaunchOffset;
    [SerializeField] private  float speed = 4f;
    
    Rigidbody2D rb;
    Vector3 lastVelocity;
    public Vector2 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.right + Vector3.up;
        GetComponentInParent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        transform.Translate(LaunchOffset);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }





}

