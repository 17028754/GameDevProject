using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    	// Idle rate need to be tied with a script that gives it the right value to display
    	// Maximum value (don't know how to make it expand to right instead of both sides)
    	idleRateText.text = "IdleRate: 1.22Mpoints/minute";
    	// Minimum starting value
    	// idleRateText.text = "IdleRate: 10points/second";

    	bossSpawnBar.SetSpawnBar(player.Points);
        pointsText.text = "Points: " + player.Points;

        if (player.Win)
        {
        	victoryScreen.gameObject.SetActive(true);
        	// gameObject.GetComponent<Spawner>().enabled = false;
        }
    }
}
