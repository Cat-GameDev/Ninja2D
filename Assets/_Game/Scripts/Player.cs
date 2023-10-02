using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    public float speed = 10f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    public static Player instance;
    private bool isGrounded = true;
    private bool isAttack = false;
    private bool isJumping = false;
    private int coin = 0;
    private float horizontal;
    
    Vector3 savePoint;

    public Boom LaunchableBoomPrefab;


    private void Awake() 
    {
        instance = this;
    }

    void Update() {
        isGrounded = CheckGround();

        horizontal = Input.GetAxisRaw("Horizontal");

        if(IsDead) return;
        
        if(isAttack) {
            rb.velocity = Vector2.zero;
            return;
        }

        if(isGrounded) 
        {
            if(isJumping) 
            {
                return;
            }

            //jump 
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if(Mathf.Abs(horizontal) > 0.1f)
            {
                ChangAnim("run");
            }

            if(Input.GetKeyDown(KeyCode.C)&& isGrounded)
            {
                Attack();
            }

            if(Input.GetKeyDown(KeyCode.V)&& isGrounded)
            {
                Throw();
            }

            // if(Input.GetKeyDown(KeyCode.S)&& isGrounded)
            // {
            //     Attack2();
            // }
            
        }

        //check falling
        if(!isGrounded && rb.velocity.y <0)
        {
            Falling();
        }

        //Run
        if(Mathf.Abs(horizontal) > 0.1f)
        {
            Run();
        }
        else if (isGrounded && !isAttack)
        {
            Idel();
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;

        transform.position = savePoint;
        ChangAnim("idle");
        DeActiveAttack();

        SavePoint();
        UIManager.instance.SetCoin(coin);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }


    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position+Vector3.down*1.2f ,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, groundLayer);
        return hit.collider != null;
    }

    public void Attack()
    {
        ChangAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAnim), 0.5f);
        ActionAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Attack2()
    {
        Instantiate(LaunchableBoomPrefab, throwPoint.position, transform.rotation);
    }

    public void Throw()
    {
        ChangAnim("throw");
        //isThrow = true;
        Invoke(nameof(ResetAnim), 0.5f);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    private void ResetAnim()
    {
        isAttack = false;
        //isThrow = false;
        ChangAnim("idle");
    }

    public void Jump()
    {
        isJumping = true;
        ChangAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void Falling()
    {
        ChangAnim("fall");
        isJumping = false;
    }
    
    private void Run()
    {
        ChangAnim("run");
        if(speed == PowerUp.instance.speed)
        {
            Invoke(nameof(SpeedDecrease), 5f);
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180,0));
    }

    private void Idel()
    {
        ChangAnim("idle");
        rb.velocity = Vector2.zero;
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    private void ActionAttack()
    {
        attackArea.SetActive(true);

    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);

    }

    public void SetMove(float horizontal)
    {   
        this.horizontal = horizontal;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Coin")
        {
            coin++;
            UIManager.instance.SetCoin(coin);
            Destroy(other.gameObject);
        }
        if(other.tag == "Death Zone")
        {
            ChangAnim("die");
            Invoke(nameof(OnInit), 1f);
        }
    }

    private void SpeedDecrease()
    {
        speed = 10f;
        Debug.Log("speed == 10");
    }


}
