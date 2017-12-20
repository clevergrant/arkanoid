using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private bool ballIsActive;
    private Vector2 ballPosition;
    private Vector2 ballInitialForce;

    public float termVelocity = 15f;
    public float bounceForce = 400f;

    // GameObject
    public GameObject playerObject;

    //rigid body
    private Rigidbody2D rb;

    //make sure it's not stuck going straight
    public float lastx;

    //check first hit
    bool hasbeenhit = false;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        ballInitialForce = new Vector2(500.0f, 1000.0f);
        lastx = ballPosition.x;

        // set to inactive
        ballIsActive = false;

        // ball position
        ballPosition = transform.position;

        // rigid body
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballIsActive)
        {
            //check if it's going straight up or down
            if (ballPosition.x == lastx && hasbeenhit)
            {
                if (rb.velocity.x <= 0)
                {
                    rb.AddForce(new Vector2(-5, rb.velocity.y));
                }
                else
                {
                    rb.AddForce(new Vector2(5, rb.velocity.y));
                }
            }

            //make sure it's not moving too fast
            if (rb.velocity.y < -termVelocity)
                rb.velocity = new Vector2(rb.velocity.x, -termVelocity);
            if (rb.velocity.y > termVelocity)
                rb.velocity = new Vector2(rb.velocity.x, termVelocity);
            if (rb.velocity.x < -termVelocity)
                rb.velocity = new Vector2(-termVelocity, rb.velocity.y);
            if (rb.velocity.x > termVelocity)
                rb.velocity = new Vector2(termVelocity, rb.velocity.y);

            if (transform.position.y < -12)
            {
                ballIsActive = false;
                hasbeenhit = false;
                rb.velocity = new Vector2(0f, 0f);
                ballPosition = new Vector2(0f, 0f);
                transform.position = ballPosition;

                playerObject.SendMessage("TakeLife");
            }
        }
        else
        {
            //if (playerObject != null)
            //{
            //    // get and use the player position
            //    ballPosition.x = playerObject.transform.position.x;

            //    // apply player X position to the ball
            //    transform.position = ballPosition;
            //}

            // check for user input
            if (Input.GetMouseButtonDown(0))
            {
                // set force
                rb.velocity = new Vector2(0f, 0f);
                rb.position = new Vector2(0f, 0f);
                transform.position = ballPosition;

                // set ball active
                ballIsActive = true;
            }
            else
            {
                ballPosition = new Vector2(0f, 0f);
                transform.position = ballPosition;
            }
        }
        lastx = ballPosition.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(new Vector2(rb.velocity.x, bounceForce));
            if (!hasbeenhit) hasbeenhit = true;
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            rb.AddForce(new Vector2(-100f, rb.velocity.y));
        }
        else if (collision.gameObject.tag == "LeftWall")
        {
            rb.AddForce(new Vector2(100f, rb.velocity.y));
        }
    }
}
