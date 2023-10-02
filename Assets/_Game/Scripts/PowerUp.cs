using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static PowerUp instance;
    public bool isInvincible, isBooster;
    public float speed = 20f;

    private void Awake() {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            if(isInvincible)
            {
                Player.instance.isInvincible = true;
                Debug.Log("Ban dag Bat Tu 5s");
            }

            if(isBooster)
            {
                Player.instance.speed = speed;
                Debug.Log("speed = 20f");
            }
            Destroy(gameObject);
        } 
        
    }

}
