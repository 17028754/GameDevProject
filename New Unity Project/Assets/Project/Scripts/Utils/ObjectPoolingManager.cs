using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{

	private static ObjectPoolingManager instance;
	public static ObjectPoolingManager Instance { get { return instance; }}

	[Header("Collected Cat Model")]
	public GameObject collectCatPrefab;
	public int amountCCP = 5;
	private List<GameObject> collectCats;

	[Header("Common Cat Model")]
	public GameObject commonCatPrefab;
	public int amountCommonCat = 10;
	private List<GameObject> commonCats;

	[Header("Unique Cat Model")]
	public GameObject uniqueCatPrefab;
	public int amountUniqueCat = 3;
	private List<GameObject> uniqueCats;

	[Header("Box Cat Model")]
	public GameObject boxCatPrefab;
	public int amountBoxCat = 3;
	private List<GameObject> boxCats;

	[Header("Boss Cat Model")]
	public GameObject bossCatPrefab;
	private int amountBossCat = 1;
	private List<GameObject> bossCat;

    // Use this for initialization
    void Awake ()
    {
        instance = this;

        // Preload CCP (Collected Cat Prefab/Model)
        collectCats = new List<GameObject>(amountCCP);

        for (int i = 0; i<amountCCP; i++)
        {
        	GameObject prefabInstance = Instantiate(collectCatPrefab);
        	prefabInstance.transform.SetParent(transform);
        	prefabInstance.SetActive(false);

        	collectCats.Add(prefabInstance);
        }

        // Preload common cats
        commonCats = new List<GameObject>(amountCommonCat);

        for (int i = 0; i<amountCommonCat; i++)
        {
        	GameObject prefabInstance = Instantiate(commonCatPrefab);
        	prefabInstance.transform.SetParent(transform);
        	prefabInstance.SetActive(false);

        	commonCats.Add(prefabInstance);
        }

        // Preload unique cats
        uniqueCats = new List<GameObject>(amountUniqueCat);

        for (int i = 0; i<amountUniqueCat; i++)
        {
        	GameObject prefabInstance = Instantiate(uniqueCatPrefab);
        	prefabInstance.transform.SetParent(transform);
        	prefabInstance.SetActive(false);

        	uniqueCats.Add(prefabInstance);
        }

        // Preload box cats
        boxCats = new List<GameObject>(amountBoxCat);

        for (int i = 0; i<amountUniqueCat; i++)
        {
        	GameObject prefabInstance = Instantiate(boxCatPrefab);
        	prefabInstance.transform.SetParent(transform);
        	prefabInstance.SetActive(false);

        	boxCats.Add(prefabInstance);
        }

        // Preload boss cat
        bossCat = new List<GameObject>(amountBossCat);

        for (int i = 0; i<amountBossCat; i++)
        {
        	GameObject prefabInstance = Instantiate(bossCatPrefab);
        	prefabInstance.transform.SetParent(transform);
        	prefabInstance.SetActive(false);

        	bossCat.Add(prefabInstance);
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

    public GameObject GetCommonCat()
    {
    	foreach (GameObject cc in commonCats)
    	{
    		if (!cc.activeInHierarchy)
    		{
    			cc.SetActive(true);
    			return cc;
    		}
    	}

    	GameObject prefabInstance = Instantiate(commonCatPrefab);
    	prefabInstance.transform.SetParent(transform);
    	commonCats.Add(prefabInstance);

    	return prefabInstance;
    }

    public GameObject GetUniqueCat()
    {
    	foreach (GameObject uc in uniqueCats)
    	{
    		if (!uc.activeInHierarchy)
    		{
    			uc.SetActive(true);
    			return uc;
    		}
    	}

    	GameObject prefabInstance = Instantiate(uniqueCatPrefab);
    	prefabInstance.transform.SetParent(transform);
    	commonCats.Add(prefabInstance);
    	return prefabInstance;
    }

    public GameObject GetBoxCat()
    {
    	foreach (GameObject bc in uniqueCats)
    	{
    		if (!bc.activeInHierarchy)
    		{
    			bc.SetActive(true);
    			return bc;
    		}
    	}

    	GameObject prefabInstance = Instantiate(boxCatPrefab);
    	prefabInstance.transform.SetParent(transform);
    	boxCats.Add(prefabInstance);
    	return prefabInstance;
    }

    public GameObject GetBossCat()
    {
    	foreach (GameObject bossC in bossCat)
    	{
    		if (!bossC.activeInHierarchy)
    		{
    			bossC.SetActive(true);
    			return bossC;
    		}
    	}

    	return null;
    }
}
