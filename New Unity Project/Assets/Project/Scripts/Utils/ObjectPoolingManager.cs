using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{

	private static ObjectPoolingManager instance;
	public static ObjectPoolingManager Instance { get { return instance; }}

	public GameObject collectCatPrefab;
	public int amountCCP = 5;

	private List<GameObject> collectCats;

    // Use this for initialization
    void Awake ()
    {
        instance = this;

        // Preload bullets
        collectCats = new List<GameObject>(amountCCP);

        for (int i = 0; i<amountCCP; i++)
        {
        	GameObject prefabInstance = Instantiate(collectCatPrefab);
        	prefabInstance.transform.SetParent(transform);
        	prefabInstance.SetActive(false);

        	collectCats.Add(prefabInstance);
        }
    }

    public GameObject GetCCP()
    {
    	foreach (GameObject ccp in collectCats)
    	{
    		if (!ccp.activeInHierarchy)
    		{
    			ccp.SetActive(true);
    			return ccp;
    		}
    	}

	    GameObject prefabInstance = Instantiate(collectCatPrefab);
    	prefabInstance.transform.SetParent(transform);
    	collectCats.Add(prefabInstance);

    	return prefabInstance;
    }
}
