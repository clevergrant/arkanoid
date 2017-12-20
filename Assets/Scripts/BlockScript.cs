using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    public int hitsToKill;
    public int points;
    public int numberOfHits;

    // Use this for initialization
    void Start()
    {
        numberOfHits = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            numberOfHits++;

            if (numberOfHits == hitsToKill)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                player.SendMessage("addPoints", points);

                //destroy
                Destroy(this.gameObject);
            }
        }
    }
}
