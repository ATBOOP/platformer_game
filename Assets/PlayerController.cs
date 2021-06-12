using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

     [SerializeField]
    float fJumpVelocity = 5;
    
    private Rigidbody2D rb;
    Rigidbody2D rigid;

    private bool facingRight = true;

    
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    private int extraJumps;
    public int extraJumpsValue;
    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    
    void Start(){
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce; 
        }

        if(Input.GetKey(KeyCode.Space)){
            rb.velocity = Vector2.up * jumpForce;
            fJumpPressedRemember -= Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if ((fJumpPressedRemember > 0) && isGrounded)
        {
            fJumpPressedRemember = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, fJumpVelocity);
        }

        }

        if(Input.GetKey(KeyCode.Space) && isJumping == true){
            if(jumpTimeCounter > 0){
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
        }
    }
    if(Input.GetKeyUp(KeyCode.Space)){
        isJumping = false;
    }

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);   
        
        if(facingRight == false && moveInput > 0){
            Flip();
        } else if(facingRight == true && moveInput > 0){
            Flip();
        }
    }

    void Update(){
        
        if(isGrounded == true){
            extraJumps = extraJumpsValue;
        
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        }    
    }

    void Flip(){
        
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1; 
        transform.localScale = Scaler; 

    }
}
