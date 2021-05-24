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
	// increase damage
	private int increaseDamage = 10;
	private int increaseDamageCap = 4;
	private int increaseDamageTracker = 0;
	// Crit Chance
	private int critChance = 10;
	private int critChanceCap = 4;
	private int critChanceTracker = 0;
	// Crit Damage
	private int critDamage = 10;
	private int critDamageCap = 4;
	private int critDamageTracker = 0;

	private int points;
	public int Points { get { return points; }}

	private Vector3 position;
	private GameObject collectCatObject;

	private EnemyMovement bossCatScript;
	private bool win = false;
	public bool Win { get { return win; }}

	private int timer = 0;
	private int maxTimer = 5;
	private List<GameObject> commonCatList;

	[Header("Configuration")]
	public Spawner spawnScript;
	public ItemScript itemScript;




    // Start is called before the first frame update
    void Start()
    {
        points = initialPoints;
        InvokeRepeating("increaseTimer", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
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

	            if (hit.collider.gameObject.tag != "Boss")
	            {

	            	// Define the position that the collected cat spirte has to spawn
	            	position = hit.collider.gameObject.transform.position;

	            	// Deactive cat model then add points
	                hit.collider.gameObject.SetActive(false);
	                points++;

	                // Reduce the number of spawnned cats in spawner
	                if (hit.collider.gameObject.tag == "CommonCat")
	                {
	                	spawnScript.CommonCatSpawnned -= 1;
	                } else if (hit.collider.gameObject.tag == "UniqueCat")
	                {
	                	spawnScript.UniqueCatSpawnned -= 1;
	                } else if (hit.collider.gameObject.tag == "BoxCat")
	                {
	                	spawnScript.BoxCatSpawnned -= 1;
	                	// Item foundation, at the moment only 3 item with 3 different basic stats are implemented
	                	// Make sure there is error checking, do not over add values or select capped values
	                	bool looper = true;
	                	while (looper)
	                	{
		                	int rand = Random.Range(0, 3);
		                	if (rand == 0 && increaseDamageTracker != increaseDamageCap)
		                	{
		                		increaseDamage += itemScript.increaseDmg;
		                		increaseDamageTracker += 1;
		                		looper = false;
		                	}
		                	else if (rand == 1 && critChanceTracker != critChanceCap)
		                	{
		                		critChance += itemScript.critChance;
		                		critChanceTracker += 1;
		                		looper = false;
		                	}
		                	else if (rand == 2 && critDamageTracker != critDamageCap)
		                	{
		                		critDamage += itemScript.critDmg;
		                		critDamageTracker += 1;
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
	            	bossCatScript = hit.collider.gameObject.GetComponent<EnemyMovement>();


	            	Debug.Log("This is increase damage: " + increaseDamage);
	            	Debug.Log("This is increase crit chance " + critChance);
	            	Debug.Log("This is increase crit dmg : " + critDamage);

	            	int totalDamage = baseDamage + baseDamage*increaseDamage/100;
	            	Debug.Log("This is total damage: " + totalDamage);

	            	int rand = Random.Range(0, 100);
	            	Debug.Log("This is rand : " + rand);
	            	
	            	if (rand < critChance)
	            	{
	            		totalDamage = totalDamage + totalDamage*critDamage/100;
	            		Debug.Log("Total damage when crit: " + totalDamage);
	            	}
	            	Debug.Log("Boss Before getting damaged health: " + bossCatScript.BossCatHP);
	            	bossCatScript.BossCatHP -= totalDamage;
	            	Debug.Log("Boss remaining health: " + bossCatScript.BossCatHP);

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
        		// Perform idle feature
				commonCatList = ObjectPoolingManager.Instance.GetCommonCatList();   
				foreach (GameObject cc in commonCatList)
				{
					if(cc.activeInHierarchy && cc.transform.position.x < 8f && cc.transform.position.x > -8f)
					{
						cc.SetActive(false);
						points++;
						spawnScript.CommonCatSpawnned -= 1;
						// timer change to 4, so that idle feature will automatically collec cats every 1 second
						timer = 4;

						// Obtain position for collected cat model to spawn at the right location
						position = cc.transform.position;

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
    }

}
