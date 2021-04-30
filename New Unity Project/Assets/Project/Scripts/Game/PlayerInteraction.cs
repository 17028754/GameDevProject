using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

	[Header("Gameplay")]
	public int initialPoints = 0;
	public Transform targetCodex;

	private int points;
	public int Points { get { return points; }}

	private Vector3 position;
	private GameObject collectCatObject;


    // Start is called before the first frame update
    void Start()
    {
        points = initialPoints;
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

            	// Define the position that the collected cat spirte has to spawn
            	position = hit.collider.gameObject.transform.position;

            	// Destroy cat model (nid to deactivate instead) then add points
                Destroy(hit.collider.gameObject);
                points++;

                // Then obtain collected cat model from PoolingManager, and spawn it at the clicked cat model's location
                collectCatObject = ObjectPoolingManager.Instance.GetCCP();
                collectCatObject.transform.position = position;

                // Perform the smooth animation to move the collected cat model to the codex
                StartCoroutine(moveObject());

                // // Deactive collected cat model after it has reached the codex
                StartCoroutine(deactivateObject());
            }
        }
    }

    // Move the collected cat model to the codex in a small movement, use StartCoroutine to make the smooth animation/transition
    public IEnumerator moveObject()
    {
    	float totalMovementTime = 1f;
    	float currentMovementTime = 0f;
    	while (Vector3.Distance(collectCatObject.transform.position, targetCodex.transform.position) > 0)
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
    	yield return new WaitForSeconds(0.5f);
    	collectCatObject.SetActive(false);

    	
    }
}
