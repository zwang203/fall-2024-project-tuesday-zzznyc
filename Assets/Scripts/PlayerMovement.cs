
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
    Vector2 playerpos;
    Vector2 reticlepos;
    Vector2 direction;
    [SerializeField] float speed = 10f;
    [SerializeField] float regGrav = 1.0f;
    [SerializeField] float wallGrav = 0.5f;
    [SerializeField] int numJumps = 5;

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
        if (Input.GetKeyUp("r"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        UpdateStepsText();
    }

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
    private void UpdateStepsText()
    {
        stepsText.text = "Steps Remaining: " + currentJumps.ToString();
    }
}
