using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

	[Header("Gameplay")]
	public int initialPoints = 0;
	public Transform targetCodex;
	// Player Stats
	public int baseDamage = 1;

	// Items - start

	// Items coding format:
	// private int increaseDamage = 10; <--- the item's current effect
	// public int IncreaseDamage { set { increaseDamage = value; }} <--- implement setter for upgrade button to change the item's current effect.
	// private int increaseDamageCap = 16; <--- A cap to limit what is the max amount the player can get for this item, and make sure the player will not get it once its capped
	// private int increaseDamageTracker = 0; <---  A tracker to keep track of the count that the player currently has for this item
	// public int IncreaseDamageTracker { get { return increaseDamageTracker; }} <--- implementer a getter for the upgrade button to validate the upgrade

	// Cat House (Increase Idle Rate in multiplication)
	private int idleRateM = 1;
	public int IdleRateM { get { return idleRateM; } set { idleRateM = value; }}
	private int idleRateMCap = 16;
	private int idleRateMTracker = 0;
	public int IdleRateMTracker { get { return idleRateMTracker; }}
	
	// Cat Food (Increase Idle Rate points in addition)
	private int idleRateA = 1;
	public int IdleRateA { get {return idleRateA; } set { idleRateA = value; }}
	private int idleRateACap = 16;
	private int idleRateATracker = 0;
	public int IdleRateATracker { get { return idleRateATracker; }}

	// Human Gloves (Increase manual collection points)
	private int manualCollect = 1;
	public int ManualCollect { get {return manualCollect; } set { manualCollect = value; }}
	private int manualCollectCap = 16;
	private int manualCollectTracker = 0;
	public int ManualCollectTracker { get { return manualCollectTracker; }}

	// increase damage
	private int increaseDamage = 10;
	public int IncreaseDamage { get {return increaseDamage; } set { increaseDamage = value; }}
	private int increaseDamageCap = 16;
	private int increaseDamageTracker = 0;
	public int IncreaseDamageTracker { get { return increaseDamageTracker; }}

	// Crit Damage
	private int critDamage = 10;
	public int CritDamage { get { return critDamage; } set { critDamage = value; }}
	private int critDamageCap = 16;
	private int critDamageTracker = 0;
	public int CritDamageTracker { get { return critDamageTracker; }}

	// Crit Chance
	private int critChance = 10;
	public int CritChance { get {return critChance; } set { critChance = value; }}
	private int critChanceCap = 16;
	private int critChanceTracker = 0;
	public int CritChanceTracker { get { return critChanceTracker; }}

	// Items - end

	private int points;
	public int Points { get { return points; } set { points = value; }}

	private Vector3 position;
	private GameObject collectCatObject;

	private EnemyMovement bossCatScript;
	public EnemyMovement BossCatScript { get { return bossCatScript; }}

	private bool win = false;
	public bool Win { get { return win; }}

	private int timer = 0;
	private int maxTimer = 5;
	private List<GameObject> commonCatList;

	[Header("Configuration")]
	public Spawner spawnScript;
	public ItemScript itemScript;

	//Visual effects
	public GameObject effects;

	//Damage Numbers
	public bool clicked = false;

	//Boolean to make boss spawn when player is active
	private bool canSpawn = false;
	public bool CanSpawn { get { return canSpawn; }}

	// To pass the idle rate (Points per second) for GameManager to display on UI
	private int idlePointsGained = 1;
	public int IdlePointsGained { get { return idlePointsGained; }}

	private int manualPointsGained = 0;


	// Start is called before the first frame update
	void Start()
    {
        points = initialPoints;
        InvokeRepeating("increaseTimer", 1f, 1f);

	}

    // Update is called once per frame
    void FixedUpdate()
    {
    	// Check if button is pressed	
       if (Input.GetMouseButtonDown(0))
        {

        	// Obtain current mouse position in Vector 3D
            Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Convert current mouse position to Vector 2D for 2D game
            Vector2 click2D = new Vector2(clickpos.x, clickpos.y);

            // Use Raycasthit2D to help determine if the object clicked has a collider
            // NOTE: Need to make sure the raycast hits a cat model instead of just a collider
            RaycastHit2D hit = Physics2D.Raycast(click2D, Vector2.zero);
            if (hit.collider != null)
            {
            	// Only make this run once
            	if (!spawnScript.BossCatSpawnned)
            	{
            		// When player plays, make boss spawnable if the points is valid
	            	canSpawn = true;
            	}

				//Damage pop up
				
				if (hit.collider.gameObject.tag != "Boss" && hit.collider.gameObject.tag != "Walls")
				{

					// Define the position that the collected cat spirte has to spawn
					position = hit.collider.gameObject.transform.position;
					clicked = true;


					// Effects
					Instantiate(effects, transform.position, Quaternion.identity);

					// Deactive cat model then add points
					hit.collider.gameObject.SetActive(false);
					//points++; // Use a gained points variable instead of adding on the points directly

					// Reduce the number of spawnned cats in spawner
					// Add the points according to the cat types
					if (hit.collider.gameObject.tag == "CommonCat")
					{
						int gained_points = 1;
						manualPointsGained = gained_points + gained_points * manualCollect / 100;
						points += manualPointsGained;
						spawnScript.CommonCatSpawnned -= 1;
					}
					else if (hit.collider.gameObject.tag == "UniqueCat")
					{
						int gained_points = 10;
						manualPointsGained = gained_points + gained_points * manualCollect / 100;
						points += manualPointsGained;
						spawnScript.UniqueCatSpawnned -= 1;
					}
					else if (hit.collider.gameObject.tag == "BoxCat")
					{
						int gained_points = 1;
						manualPointsGained = gained_points + gained_points * manualCollect / 100;
						points += manualPointsGained;
						spawnScript.BoxCatSpawnned -= 1;
						// Make sure there is error checking, do not over add values or select capped values
						// Capturing box cat doesn't increase the stat value directly, upgrading does <--- Take note
						bool looper = true;
						while (looper)
						{
							int rand = Random.Range(0, 6);
							// Idle Rate Multiplicative
							if (rand == 0 && idleRateMTracker != idleRateMCap)
							{
								idleRateMTracker += 1;
								looper = false;
							}
							// Idle Rate Addition
							else if (rand == 1 && idleRateATracker != idleRateACap)
							{
								idleRateATracker += 1;
								looper = false;
							}
							// Manual Collect
							else if (rand == 2 && manualCollectTracker != manualCollectCap)
							{
								manualCollectTracker += 1;
								looper = false;
							}
							// Increase Damage
							else if (rand == 3 && increaseDamageTracker != increaseDamageCap)
							{
								//increaseDamage += itemScript.increaseDmg;
								increaseDamageTracker += 1;
								looper = false;
							}
							// Crit Damage
							else if (rand == 4 && critDamageTracker != critDamageCap)
							{
								//critDamage += itemScript.critDmg;
								critDamageTracker += 1;
								looper = false;
							}
							// Crit Chance
							else if (rand == 5 && critChanceTracker != critChanceCap)
							{
								//critChance += itemScript.critChance;
								critChanceTracker += 1;
								looper = false;
							}
							else
							{
								looper = false;
							}
						}
					}




					// Then obtain collected cat model from PoolingManager, and spawn it at the clicked cat model's location
					collectCatObject = ObjectPoolingManager.Instance.GetCCP();
					collectCatObject.transform.position = position;

					// Perform the smooth animation to move the collected cat model to the codex
					StartCoroutine(moveObject());

					// // Deactive collected cat model after it has reached the codex
					StartCoroutine(deactivateObject());
				}
				// if the player clicked on "boss" tag object, deal damage accordingly
				else if (hit.collider.gameObject.tag == "Boss")
				{
					clicked = true;
					bossCatScript = hit.collider.gameObject.GetComponent<EnemyMovement>();


					// Debug.Log("This is increase damage: " + increaseDamage);
					// Debug.Log("This is increase crit chance " + critChance);
					// Debug.Log("This is increase crit dmg : " + critDamage);

					int totalDamage = baseDamage + baseDamage * increaseDamage / 100;
					// Debug.Log("This is total damage: " + totalDamage);

					int rand = Random.Range(0, 100);
					// Debug.Log("This is rand : " + rand);

					if (rand < critChance)
					{
						totalDamage = totalDamage + totalDamage * critDamage / 100;
						// Debug.Log("Total damage when crit: " + totalDamage);
					}
					// Debug.Log("Boss Before getting damaged health: " + bossCatScript.BossCatHP);
					bossCatScript.BossCatHP -= totalDamage;
					// Debug.Log("Boss remaining health: " + bossCatScript.BossCatHP);

					if (bossCatScript.BossCatHP <= 0)
					{
						hit.collider.gameObject.SetActive(false);
						win = true;
					}
				}

				timer = 0;
			}





		}
				

        // Check how long is user idle (button not pressed)
        if(!Input.GetMouseButtonDown(0))
        {

        	// Once button is not pressed for more than 5 seconds, perform idle feature
        	if (timer > maxTimer)
        	{
        		// Only do this once 
        		if (!spawnScript.BossCatSpawnned)
        		{
        			// Make boss unspwanable when idle feature is active
        			canSpawn = false;
        		}

        		// Perform idle feature
				commonCatList = ObjectPoolingManager.Instance.GetCommonCatList();   
				foreach (GameObject cc in commonCatList)
				{
					if(cc.activeInHierarchy && cc.transform.position.x < 8f && cc.transform.position.x > -8f)
					{
						cc.SetActive(false);
						//points++;
						int gained_points = 1;
						idlePointsGained = gained_points + gained_points*IdleRateM/100 + IdleRateA;
						points += idlePointsGained;
						spawnScript.CommonCatSpawnned -= 1;
						// timer change to 4, so that idle feature will automatically collect one cat every 1 second
						timer = 4;

						// Obtain position for collected cat model to spawn at the right location
						position = cc.transform.position;
						Instantiate(effects, transform.position, Quaternion.identity);
						spawnScript.effectsSpawned -= 1;

		                // Then obtain collected cat model from PoolingManager, and spawn it at the clicked cat model's location
		                collectCatObject = ObjectPoolingManager.Instance.GetCCP();
		                collectCatObject.transform.position = position;

		                // Perform the smooth animation to move the collected cat model to the codex
		                StartCoroutine(moveObject());

		                // // Deactive collected cat model after it has reached the codex
		                StartCoroutine(deactivateObject());

		                break;
					}
				}


        	}
        }
    }

    // Move the collected cat model to the codex in a small movement, use StartCoroutine to make the smooth animation/transition
    public IEnumerator moveObject()
    {
    	float totalMovementTime = 1f;
    	float currentMovementTime = 0f;

    	while (collectCatObject.transform.position != targetCodex.transform.position)
    	{
    		currentMovementTime += Time.deltaTime;
    		collectCatObject.transform.position = Vector3.Lerp(collectCatObject.transform.position, targetCodex.transform.position, currentMovementTime/totalMovementTime);
    		yield return null;
    	}
    }

    // Deactivate collect cat model 
    public IEnumerator deactivateObject()
    {

    	// Wait for half a second, then deactive the collected cat model
    	yield return new WaitForSeconds(0.1f);
    	collectCatObject.SetActive(false);
    	
    }

    // Increase timer every 1 second
    public void increaseTimer()
    {
    	timer += 1;
    	// Debug.Log (IdleRateM);
    }

}
