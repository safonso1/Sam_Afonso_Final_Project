using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    //Float variable that stores the speed at which the projectile moves
    private float speed = 15.0f;



    //Game initialization
    void Start()
    {

        //Placeholder

    }

    // Update is called once per frame
    void Update()
    {

        //Move the projectile forward by the speed set
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
