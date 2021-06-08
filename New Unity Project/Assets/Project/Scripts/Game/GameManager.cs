using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{

	[Header("Game")]
	public PlayerInteraction player;
	public Spawner spawnScript;


    [Header("UI")]
	public Text pointsText;
	public Image VictoryScreen;
	public Text victoryScreen;
	public Text idleRateText;
	public BossSpawnBar bossSpawnBar;

    // Start is called before the first frame update
    void Start()
    {
    	victoryScreen.gameObject.SetActive(false);
    	bossSpawnBar.SetMaxBar(spawnScript.BossSpawnCriteria);
    }

    // Update is called once per frame
    void Update()
    {
    	// Idle rate need to be adjusted, and actual idle rate in gameplay need to be adjusted accordingly
    	// Maximum value (don't know how to make it expand to right instead of both sides)
    	// Minimum starting value
    	// idleRateText.text = "IdleRate: 10points/second";
    	idleRateText.text = "IdleRate: " + player.IdlePointsGained + "points/second";

    	// Change spawn bar to reflect boss cat's current hp after boss cat is spawnned
    	// Note: Boss spawn criteria (Points, must be the same as boss's max hp)
    	if (!spawnScript.BossCatSpawnned)
    	{
    		bossSpawnBar.SetSpawnBar(player.Points);
    	}
    	else
    	{
    		bossSpawnBar.SetSpawnBar(player.BossCatScript.BossCatHP);
    	}

    	pointsText.text = "Points: " + player.Points;


        if (player.Win)
        {
        	victoryScreen.gameObject.SetActive(true);
        	// gameObject.GetComponent<Spawner>().enabled = false;
        }
    }
    
    
}
