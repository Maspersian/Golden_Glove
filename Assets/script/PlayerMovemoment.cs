using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovemoment : MonoBehaviour
{
    Rigidbody2D rb;
    public int clikCounter;
    public bool doubleJump;
    public float horizon;
    // public Button jumpBt;
    public int btCount;

    [SerializeField] float btSpeed;
    [SerializeField] bool isgrounded;
    [SerializeField] int jumpPower;
    [SerializeField] Transform groundCheck;
    //[SerializeField] Transform poss;
    [SerializeField] Vector2 pos;
    [SerializeField] LayerMask groundLayer;
   // [SerializeField] int horizonSpeed;
    [SerializeField] int speed;
    [SerializeField] Animator player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jump1Sfx;
    [SerializeField] AudioClip jump2Sfx;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // jumpBt.onClick.AddListener(JumpBtPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
        Jump();
        horizon = Input.GetAxis("Horizontal");
       
        if (rb.velocity.y < 0)
        {
            player.SetBool("Jump", false);
            player.SetBool("DoubleJump", false);
        }
        
    }
    private void Awake()
    {
         doubleJump = false;
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Jump()
    {
        //isgrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.07f, 0.7f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        isgrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
       
        if (Input.GetButtonDown("Jump"))
        {
           if (isgrounded)
           {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);

                player.SetBool("Jump", true);
                audioSource.PlayOneShot(jump1Sfx);
              
                doubleJump = true;
                //JumpBtPressed();
           }
           else if (doubleJump)
           {
                if (Input.GetButtonDown("Jump") && doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    doubleJump = false;

                    player.SetBool("DoubleJump", true);
                    player.SetBool("Jump", false);
                    audioSource.PlayOneShot(jump2Sfx);
                }
           }

        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y * 0.5f);
        }

    }
    
   
    void Movement()
    {
        rb.velocity = new Vector2(horizon * speed * Time.deltaTime , rb.velocity.y);
       // rb.AddForce(ForceMode.Force)
        if (horizon > 0f)
        {
           GetComponent<SpriteRenderer>().flipX = false;
            player.SetBool("Run",true);
        }
        else if(horizon < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            player.SetBool("Run", true);
        }
        else
        {
            player.SetBool("idle", true);
            player.SetBool("Run",false);

        }
    }
    public  void JumpBtPressed()
    {
        if (isgrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 1f);

            player.SetBool("Jump", true);
            audioSource.PlayOneShot(jump1Sfx);

            doubleJump = true;
            btCount++;
        }
        else if (doubleJump)
        {
            if (btCount == 1 && doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * 1f);
                doubleJump = false;

                player.SetBool("DoubleJump", true);
                player.SetBool("Jump", false);
                audioSource.PlayOneShot(jump2Sfx);
                btCount = 0;

            }
        }

        if (btCount > 1)
        {
            btCount = 0;
        }

         
    }
    public void LeftBtPressed()
    {
        pos = transform.position;
        pos.x -= btSpeed;
        transform.position = pos;
        Debug.Log("Left");
    }
    public void RightBtPresed()
    {


        Debug.Log("Right");

    }
}
