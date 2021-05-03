using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	[Header("Game")]
	public PlayerInteraction player;
	public float uniqueBaseSpawn = 1f;
	private float uniqueSpawnRate = 0f;
	private int previousSpawnPointU = 0;
	public float boxBaseSpawn = 1f;
	private float boxSpawnRate = 0f;
	private int previousSpawnPointB = 0;

	private float MinY = 250f;
	private float MaxY = 256f;

	private float leftMinX = 440f;
	private float leftMaxX = 443f;

	private float rightMinX = 464f;
	private float rightMaxX = 467f;

	private bool spawnLeft = false;
	private bool spawnRight = false;

	private int commonCatSpawnned = 0; 
	private int uniqueCatSpawnned = 0;
	private int boxCatSpawnned = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnLeft = true;
        uniqueSpawnRate = uniqueBaseSpawn;
        boxSpawnRate = boxBaseSpawn;

        // Call spawn unique cat and box cat function, and see if can spawn unique cat every 1 second
        InvokeRepeating("spawnUniqueCat", 1.0f, 1.0f);
        InvokeRepeating("spawnBoxCat", 1.0f, 1.0f);
    }


    // Update is called once per frame
    void Update()
    {

    	// Check if there is enough common cat spawnned
    	if (commonCatSpawnned < 8)
    	{

    		// Wait for 1 to 2 seconds
			StartCoroutine(waitSeconds());

			// Make it either spawn left or right alternatively, spawn left first then right
	        if (spawnLeft == true)
	        {

	        	// Obtain position for this new cat that is going to be spawnned
	        	Vector3 position = spawnCatPosition(true);

	        	// Obtain the gameobject for common cat and defines it spawnning position
	        	GameObject commonCat = ObjectPoolingManager.Instance.GetCommonCat();
	        	commonCat.transform.position = position;

	        	// Set the spawnning behaviour to right region for next spawn
	        	spawnLeft = false;
	        	spawnRight = true;

	        	// Keep track of number of cats spawnned
	    		commonCatSpawnned++;

	        } else if (spawnRight == true)
	        {
	        	Vector3 position = spawnCatPosition(false);

	        	GameObject commonCat = ObjectPoolingManager.Instance.GetCommonCat();
	        	commonCat.transform.position = position;

	        	// Set the spawnning behaviour to left region for next spawn
	        	spawnRight = false;
	        	spawnLeft = true;
	    		commonCatSpawnned++;
	        }
    	}

    }

	// Spawn unique cat according to requirements
    public void spawnUniqueCat()
    {
    	if (uniqueCatSpawnned < 3)
    	{
	    	float uniqueSpawn = Random.Range(0f, 100f);
	    	if (uniqueSpawn < uniqueSpawnRate)
	    	{
	    		bool left = false;
	    		int rand_pos = Random.Range(0,2);
	    		if (rand_pos == 0)
	    		{
	    			left = true;
	    		}
	        	else if (rand_pos == 1)
	        	{
	        		left = false;
	        	}
	        	Vector3 position = spawnCatPosition(left);
	        	GameObject uniqueCat = ObjectPoolingManager.Instance.GetUniqueCat();
	        	uniqueCat.transform.position = position;

	        	Debug.Log(uniqueSpawn);
	        	Debug.Log(uniqueSpawnRate);

	        	uniqueSpawnRate = uniqueBaseSpawn;
	        	uniqueCatSpawnned += 1;
	    	}
	    	// If the player's current points are divisible by 10 with no remainder, meaning the player has acquired 10 points
	    	else if (player.Points != previousSpawnPointU && player.Points%10 == 0)
	    	{
	    		uniqueSpawnRate += 10;

	    		// store current player's points to make sure no more spawn rate addition will be performed for the same point
	    		previousSpawnPointU = player.Points;
	    	}

    	}
    }


    // Spawn box cat according to requirements
    public void spawnBoxCat()
    {
    	if (boxCatSpawnned < 3)
    	{
	    	float boxSpawn = (Random.Range(0f, 100f) * 100);
	    	if (boxSpawn < boxSpawnRate)
	    	{
	    		bool left = false;
	    		int rand_pos = Random.Range(0,2);
	    		if (rand_pos == 0)
	    		{
	    			left = true;
	    		}
	        	else if (rand_pos == 1)
	        	{
	        		left = false;
	        	}
	        	Vector3 position = spawnCatPosition(left);
	        	GameObject boxCat = ObjectPoolingManager.Instance.GetBoxCat();
	        	boxCat.transform.position = position;

	        	boxSpawnRate = boxBaseSpawn;
	        	boxCatSpawnned += 1;
	    	}
	    	// If the player's current points are divisible by 10 with no remainder, meaning the player has acquired 10 points
	    	else if (player.Points != previousSpawnPointB && player.Points%10 == 0)
	    	{
	    		boxSpawnRate += 10;

	    		// store current player's points to make sure no more spawn rate addition will be performed for the same point
	    		previousSpawnPointB = player.Points;
	    	}
    	}
    }

    // Function to wait for 1 to 2 seconds before spawnning next cat
    public IEnumerator waitSeconds()
    {

    	float timer = Random.Range(1f, 2f);
    	yield return new WaitForSeconds(timer);
    } 


    // Randomly generate the position for next cat spawn
 	public Vector3 spawnCatPosition(bool left)
   	{
   		float rand_Y = Random.Range(MinY, MaxY);
   		float rand_X = 0f;

   		if (left == true)
   		{
   			rand_X = Random.Range(leftMinX, leftMaxX);
   		}else
   		{
   			rand_X = Random.Range(rightMinX, rightMaxX);
   		}
   		Vector3 position = new Vector3(rand_X, rand_Y, 0f);

   		return position;
   	}



}