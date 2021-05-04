using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    //Float variable that stores the speed at which the enemy moves
    private float speed = 0.0f;



    //Game Initialization
    void Start()
    {

        //Placeholder

    }

    //Every frame
    void Update()
    {

        speed = GameObject.Find("GameManager").GetComponent<GameManager>().speed;

        //Move the enemy forward by the speed set
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    //Upon collision
    private void OnTriggerEnter(Collider other)
    {

        //If the enemy is hit by the players spell
        if (other.tag == "Fireball")
        {

            //The player gains a point for the score which is stored in the game manager script attached to the game manager object
            GameObject.Find("GameManager").GetComponent<GameManager>().score++;

            //Destroy the enemy
            Destroy(gameObject);

        }
    }

}