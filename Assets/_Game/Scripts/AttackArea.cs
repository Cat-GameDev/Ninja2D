using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player" || collider.tag == "Enemy")
        {
            collider.GetComponent<Character>().OnHit(30f);
            Debug.Log("attack");
        }
    }
}
