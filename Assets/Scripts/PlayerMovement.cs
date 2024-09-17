using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject reticlecenter;
    //bool spacepressed = false;
    Rigidbody2D rb;
    Vector2 playerpos;
    Vector2 reticlepos;
    Vector2 direction;
    [SerializeField] float speed = 10f;
    [SerializeField] float regGrav = 1.0f;
    [SerializeField] float wallGrav = 0.5f;

    bool onFloor = false;
    //[SerializeField] float acceleration = 1f;
    //[SerializeField] float maxspeed = 300f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space") && onFloor)
        {
            playerpos = (Vector2)transform.position;
            reticlepos = (Vector2)reticle.transform.position;
            direction = reticlepos - playerpos;
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("Wall"))
        {
            onFloor = true;

        }
        if (col.gameObject.CompareTag("Wall"))
        {
            rb.gravityScale = wallGrav;

        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("Wall"))
        {
            onFloor = false;
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            rb.gravityScale = regGrav;
        }
    }
}
