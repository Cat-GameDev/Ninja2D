using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    private bool isAttack = false;
    private bool isRight = true;
    private IState currentState;
    private Character target;
    public Character Target => target;

    private void Update() 
    {
        if(isAttack) {
            return;
        }
        if(currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
        Destroy(heathBar.gameObject);
    }

    
    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        if(IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if(Target != null)
        {
            ChangeState(new PatrolState());
        } else ChangeState(new IdleState());
    }

    public void Moving()
    {
        ChangAnim("run");
        rb.velocity = transform.right * moveSpeed; 
    }

    public void StopMoving()
    {
        ChangAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangAnim("attack");
        Invoke(nameof(ResetAnim), 0.5f);
        ActionAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public bool IsTargetInRange()
    {   
        if(target != null && Vector2.Distance(Target.transform.position, transform.position ) <= attackRange)
            return  true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up*180);
    }

    private void ActionAttack()
    {
        attackArea.SetActive(true);
        
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
       
    }

    
    private void ResetAnim()
    {
        isAttack = false;
        ChangAnim("idle");
    }


}
