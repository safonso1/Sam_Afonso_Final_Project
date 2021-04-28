using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitScript : MonoBehaviour
{


    //Game initialization
    void Start()
    {

        //Placeholder

    }

    //Every frame
    void Update()
    {

        //Placeholder

    }

    private void OnTriggerEnter(Collider other)
    {

        //If the enemy reaches a limit
        if (other.tag == "Enemy")
        {

            //The player loses a point for the score which is stored in the game manager script attached to the game manager object
            GameObject.Find("GameManager").GetComponent<GameManager>().score--;

        }

        //Destroy both enemies and spells when they hit the wall
        Destroy(other.gameObject);

    }

}
