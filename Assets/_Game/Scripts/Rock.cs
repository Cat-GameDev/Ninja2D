using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject da;
    public float spawnRate = 2f;
    private float timer = 0;
    private void Update() 
    {
        if(timer < spawnRate)
        {
            timer = timer+Time.deltaTime;
        } else {
            Instantiate(da, transform.position, Quaternion.identity);
            timer = 0;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Character>().OnHit(20f);
        } 
        Destroy(gameObject);
        
    }
}
