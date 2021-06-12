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

    [Header("Effects")]
    public GameObject effectPrefab;
    public int amountOfEffects = 1;
    private List<GameObject> effects;

    [Header("Damage")]
    public GameObject damagePrefab;
    public int amountOfDamage = 1;
    private List<GameObject> damage;

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

        //effects
        effects = new List<GameObject>(amountOfEffects);

        for (int i = 0; i < amountOfEffects; i++)
        {
            GameObject prefabInstance = Instantiate(effectPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            effects.Add(prefabInstance);
        }

        //damage
        damage = new List<GameObject>(amountOfDamage);

        for (int i = 0; i < amountOfDamage; i++)
        {
            GameObject prefabInstance = Instantiate(damagePrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            damage.Add(prefabInstance);
        }
    }



    // Functions used by other script that wants to access the initialised objects

    // Access Collected Cat Model 
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

    // Access Common Cat
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

    // Access Unique Cat
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

    	return null;
    }

    // Access Box Cat
    public GameObject GetBoxCat()
    {
    	foreach (GameObject bc in boxCats)
    	{
    		if (!bc.activeInHierarchy)
    		{
    			bc.SetActive(true);
    			return bc;
    		}
    	}

    	return null;
    }

    // Access Boss Cat
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

    public GameObject GetEffects()
    {
        foreach (GameObject effect in effects)
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                return effect;
            }
        }

        return null;
    }
    public GameObject GetDamage()
    {
        foreach (GameObject damage in damage)
        {
            if (!damage.activeInHierarchy)
            {
                damage.SetActive(true);
                return damage;
            }
        }

        return null;
    }

    // Access common cats that have been activated (for idle feature)
    public List<GameObject> GetCommonCatList()
    {
    	return commonCats;
    }
}
