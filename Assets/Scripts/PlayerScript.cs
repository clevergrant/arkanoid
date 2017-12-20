using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float playerVelocity;
    private Vector3 playerPosition;
    public float boundary;

    Animator anim;

    private int playerLives;
    private int playerScore;

	// Use this for initialization
	void Start () {
        //get the initial position of the game object
        Cursor.visible = false;
        playerPosition = gameObject.transform.position;
        anim = GetComponent<Animator>();

        playerLives = 3;
        playerScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //horizontal movement
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.y = -9f;
        
        //boundaries
        if (pos.x < -boundary)
        {
            pos.x = -boundary;
        }
        if (pos.x > boundary)
        {
            pos.x = boundary;
        }

        gameObject.transform.position = pos;

        //leave game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        WinLose();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            anim.SetTrigger("Paddle_n_Hit");
        }
    }

    void addPoints(int points)
    {
        playerScore += points;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Live's: " + playerLives + "  Score: " + playerScore);
    }

    void TakeLife()
    {
        playerLives--;
    }

    void WinLose()
    {
        if (playerLives == 0)
        {
            SceneManager.LoadScene("Level1");
        }

        if ((GameObject.FindGameObjectsWithTag("Block")).Length == 0)
        {
            Application.Quit();
        }
    }
}
