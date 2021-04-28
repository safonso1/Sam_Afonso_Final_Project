using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Reference to the enemy game object
    public GameObject enemy;


    //Integer variable that stores the score of the game
    public int score = 0;


    //Float variable that stores the time before the next spawn
    private float spawnRate = 2.0f;
    //Float variables that store the current amount of enemy spawns that have happened during the game
    private float spawns = 0.0f;
    private float spawnsProgress = 0.0f;

    //Float variables that store the increase of spawnrate and how many spawns it takes before it adds more difficulty as well as a cap to difficulty
    //NOTE: Not used yet
    private float difSpeed = -0.25f;
    private float difIteration = 10.0f;
    private float difLimit = 50.0f;


    //Vector variables that will store where an enemy could potential spawn, one for each side of the screen
    private Vector3 leftSpawnPos;
    private Vector3 rightSpawnPos;
    private Vector3 topSpawnPos;
    private Vector3 botSpawnPos;
    //Integer variable that stores the rotation the enemy will spawn with (used to manage what direction the enemy moves)
    private int rotation = 0;
    //List that will store the four spawn position vectors defined above
    private Vector3[] SpawnPos = new Vector3[4];
    //NOTE: Eventually I will put limits to the range an enemy will spawn as proper variables instead of hardcoded in setupSpawnVector

    //Float variable that stores the speed at which the enemy moves
    public float speed = 5.0f;

    //Game Initializations
    void Start()
    {

        //NOTE: This is temporary in order to test spawning enemies before UI with proper game start is done
        StartCoroutine(SpawnEnemy());

    }

    //Every frame
    void Update()
    {

        //Placeholder

    }

    //Coroutine called to spawn enemies around the screen
    IEnumerator SpawnEnemy()
    {

        //NOTE: Temporarily infinite until proper game start
        while (true)
        {

            //Wait for however much time the spawn rate currently is
            yield return new WaitForSeconds(setSpawnRate());

            //Store which vector in the spawn position array will be picked to instantiate with using the setupSpawnVector method
            int picked = setupSpawnVector();

            //Setup the rotation depending on what was picked to be the side the enemy spawned on
            setupEnemyRotation(picked);

            //Spawn the enemy with established position and rotation
            Instantiate(enemy, SpawnPos[picked], Quaternion.Euler(0, rotation, 0));

        }

    }

    //Sets up potential vectors for where an enemy will spawn and then picks one
    int setupSpawnVector()
    {

        //Set up the four vectors with random x or z values depending on which side it is, for example the left option has a random z
        leftSpawnPos = new Vector3(-12.0f, 0.0f, Random.Range(-4.0f, 4.0f));
        rightSpawnPos = new Vector3(12.0f, 0.0f, Random.Range(-4.0f, 4.0f));
        topSpawnPos = new Vector3(Random.Range(-7.0f, 7.0f), 0.0f, 6.0f);
        botSpawnPos = new Vector3(Random.Range(-7.0f, 7.0f), 0.0f, -6.0f);

        //Store the established vectors into the potential spawn position array
        SpawnPos[0] = leftSpawnPos;
        SpawnPos[1] = rightSpawnPos;
        SpawnPos[2] = topSpawnPos;
        SpawnPos[3] = botSpawnPos;

        //Store which vector in the spawn position array will be picked to instantiate with
        int picked = Random.Range(0, SpawnPos.Length);

        //Send the decision to the spawn enemy coroutine
        return picked;

    }

    //Changes the rotation variable for the enemy that spawns according to which side it was picked to spawn at
    void setupEnemyRotation(int picked)
    {

        //If the left side is picked
        if (picked == 0)
        {

            //Have the enemy faced right
            rotation = 90;

        }
        //Else if the right side is picked
        else if (picked == 1)
        {

            //Have the enemy faced left
            rotation = -90;

        }
        //Else if the top side is picked
        else if (picked == 2)
        {

            //Have the enemy face down
            rotation = 180;

        }
        //Else it is the bot side
        else
        {

            //So don't change rotation
            rotation = 0;

        }

    }

    //Changes the spawn rate of enemies over time as the game progresses
    float setSpawnRate()
    {

        //If the total enemies spawned is not past the limit
        if (spawns <= difLimit)
        {

            //If the total amount of enemies spawned has gone up by the next iteration for difficulty
            if (spawns == spawnsProgress + difIteration)
            {

                //Increase the spawnRate slightly
                spawnRate += difSpeed;
                //Increase enemy speed
                speed += 2;

                //Set it up so the next time the spawnRate increases is the next proper difficulty iteration
                spawnsProgress = spawns;

            }

            //Regardless keep track of total spawns
            spawns++;

        }

        Debug.Log(spawnRate);

        //Send the current spawnrate to the spawn enemy coroutine
        return spawnRate;

    }

}
