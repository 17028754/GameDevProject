using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

	[Header("Gameplay")]
	public int initialPoints = 0;
	public Transform targetCodex;
	public int baseDamage = 1;

	private int points;
	public int Points { get { return points; }}

	private Vector3 position;
	private GameObject collectCatObject;
	public Spawner spawnScript;

	private EnemyMovement bossCatScript;
	private bool win = false;
	public bool Win { get { return win; }}

	private int timer = 0;
	private int maxTimer = 5;
	private List<GameObject> commonCatList;




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

	            	bossCatScript.BossCatHP -= baseDamage;

	            	if (bossCatScript.BossCatHP == 0)
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
					if(cc.activeInHierarchy)
					{
						cc.SetActive(false);
						points++;
						spawnScript.CommonCatSpawnned -= 1;
						// timer change to 4, so that idle feature will automatically collec cats every 1 second
						timer = 4;
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
