using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected CombatText combatTextPrefab;

    private string currentAnimName;
    private float hp;
    public bool IsDead => hp <=0;
    public bool isInvincible = true;


    private void Start() 
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        heathBar.OnInit(100, transform);
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangAnim("die");
        Invoke(nameof(OnDespawn),2f);
    }

    protected void ChangAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if(isInvincible) 
        {
            Invoke(nameof(CountDown), 5f);
            return;
        }
        if(!IsDead)
        {
            hp -= damage;

            if(IsDead)
            {
                hp = 0;
                OnDeath();
            }
            heathBar.SetNewHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    private void CountDown()
    {
        isInvincible = false;
        Debug.Log("Ban HET bat tu");
    }

}
