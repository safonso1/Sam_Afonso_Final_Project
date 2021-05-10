using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Stores the spell prefab
    public GameObject spell;


    //Float variables that store a range anywhere between -1 and 1 to move player on the x (horizontal) and y (vertical) axis
    private float horizontalInput;
    private float verticalInput;

    //Float variable that stores the speed at which the player moves
    private float speed = 5.0f;

    //Float variables used to create a delay for when the user is allowed to cast the spell
    private float fireRate = 0.5f;
    private float nextFire = 0.0f;

    private GameManager gameManagerScript;



    //Game Initializations
    void Start()
    {

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    //Every frame
    void Update()
    {
        if (gameManagerScript.gameNotOver)
        {
            //Manage player movement for this frame
            playerMovement();

            //Check if player wants to cast a spell, if they do then manage that
            castSpell();
        }
    }

    //Manages the player movement, meaning both turning and limiting how far the player can go
    void playerMovement()
    {

        //Get the user input on W, A, S, D keys and store in horzontalInput (A and D) or veritcalInput (W and S)
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Make the player character move dependent on their horizontal and vertical inputs
        //NOTE: Due to Space.World they move according to the global x and y axis and are constant
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed, Space.World);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed, Space.World);


        //Creates a ray starting from the camera to the direction of the mouse position (specifically a vector point)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Used to store information when the ray hits something
        RaycastHit hit;

        //If the ray hits something
        if (Physics.Raycast(ray, out hit))
        {

            //Have the cube look at where the ray hit
            transform.LookAt(hit.point);

            //Only rotate along the y axis to look at the object
           // transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

        }

        //Make sure to stop the player when they move to far in one direction
        limitPlayerMovement();

    }

    //Limits the distance the player can go
    void limitPlayerMovement()
    {

        //If the player moves to far to the left
        if (transform.position.x < -7.0f)
        {

            //fix their position to stop them from going any further
            transform.position = new Vector3(-7.0f, transform.position.y, transform.position.z);

        }
        //Else if the player moves to far to the right
        else if (transform.position.x > 7.0f)
        {

            //fix their position to stop them from going any further
            transform.position = new Vector3(7.0f, transform.position.y, transform.position.z);

        }
        //If the player moves to far up
        if (transform.position.z < -4.0f)
        {

            //fix their position to stop them from going any further
            transform.position = new Vector3(transform.position.x, transform.position.y, -4.0f);

        }
        //Else if the player moves to far down
        else if (transform.position.z > 4.0f)
        {

            //fix their position to stop them from going any further
            transform.position = new Vector3(transform.position.x, transform.position.y, 4.0f);

        }

    }

    //Manages the creation of a spell object to kill an enemy
    void castSpell()
    {

        //If the space bar is pressed within time limits
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {

            //Update the timer for the delay
            nextFire = Time.time + fireRate;

            //Vector 3 that stores where 1 unit in front of the player is
            Vector3 spellCastPos = gameObject.transform.position + gameObject.transform.forward * 1.0f;

            //Create the spell and put it in the position determined by the spellCastPos
            Instantiate(spell, spellCastPos, gameObject.transform.rotation);

        }

    }

}
