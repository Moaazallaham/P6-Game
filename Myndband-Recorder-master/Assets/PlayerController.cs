using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private DisplayData dataInfo;
    public GameObject dataInfoObj;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public bool IsBlinked;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;
    private Rigidbody2D rb;
    void Start()
    {
        dataInfo = dataInfoObj.GetComponent<DisplayData>();

        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        MyndbandManager.UpdateBlinkEvent += blinkDetected;

    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius, whatIsGround);  
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight==false && moveInput >0)
            {
            Flip();

        }else if (facingRight == true && moveInput < 0)
        {
            Flip();
        } 
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

       
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
       //  if(IsBlinked && extraJumps >0)
        {
           // Debug.Log("test");
            //if (GameObject.Find ("MyndController").GetComponent < "MyndBandManager" >().OnBlinkDetected);
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            IsBlinked = false;


        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded== true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        
    }
    void blinkDetected(int value)
    {
        IsBlinked = true;
        Debug.Log("test");
        
    }
}
