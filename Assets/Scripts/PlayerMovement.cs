
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject reticlecenter;
    [SerializeField] TextMeshProUGUI stepsText;
    //bool spacepressed = false;

    Rigidbody2D rb;
    bool facingRight = true;

    bool wallSliding;
    [SerializeField] float wallSlidingSpeed = 2f;

    bool wallJumping;
    float wallJumpDirection;
    [SerializeField] float wallJumpTime = 0.2f;
    [SerializeField] int wallJumpCounter;
    [SerializeField] float wallJumpDuration = 0.4f;
    [SerializeField] Vector2 wallJumpPower = new Vector2(8f, 16f);

    Vector2 playerpos;
    Vector2 reticlepos;
    Vector2 direction;
    [SerializeField] float speed = 10f;
    [SerializeField] float regGrav = 1.0f;
    [SerializeField] float wallGrav = 0.5f;
    [SerializeField] int numJumps = 5;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    int currentJumps;
    

    bool onWall = false;
   
    //[SerializeField] float acceleration = 1f;
    //[SerializeField] float maxspeed = 300f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentJumps = numJumps;
        UpdateStepsText();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentJumps <= 0)
        {
            RestartGame();
        }
        if (Input.GetKeyDown("space") && (IsGrounded() || IsWalled()))
        {
            playerpos = (Vector2)transform.position;
            reticlepos = (Vector2)reticle.transform.position;
            direction = reticlepos - playerpos;
            rb.AddForce(direction.normalized * speed);
        }
        if(Input.GetKeyUp("space") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        /*
        if (Input.GetKeyUp("space") && currentJumps > 0 && onWall) 
        {
            playerpos = (Vector2)transform.position;
            reticlepos = (Vector2)reticle.transform.position;
            direction = reticlepos - playerpos;
            if (currentJumps < numJumps)
            {
                rb.velocity = Vector3.zero;
            }
            
            rb.AddForce(direction.normalized * speed);
            
        }
        */
        if (Input.GetKeyUp("r"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        WallSlide();
        Flip();
        UpdateStepsText();
    }

    void Flip()
    {
        if ((facingRight && reticle.transform.position.x-transform.position.x < 0) || (!facingRight && reticle.transform.position.x - transform.position.x >0))
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    void WallSlide()
    {
        if(IsWalled() && !IsGrounded())
        {
            wallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            wallSliding = false;
        }
    }
    
    void WallJump()
    {
        if (wallSliding)
        {
            wallJumping = false;

        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            currentJumps -= 1;
            onWall = true;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = wallGrav;
            }
        }
    }
   

    private void OnCollisionStay2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Floor"))
        {
            onWall = true;
            currentJumps = numJumps;
        }
        
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            rb.gravityScale = regGrav;
            onWall = true;
        }
        if (col.gameObject.CompareTag("Floor"))
        {
            currentJumps = numJumps;
            onWall = true;
        }
        onWall = false;
    }
    */

    private void UpdateStepsText()
    {
        stepsText.text = "Steps Remaining: " + currentJumps.ToString();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
