using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	[Header("Game")]
	public PlayerInteraction player;

	[Header("UI")]
	public Text pointsText;
	public Image VictoryScreen;
	public Text victoryScreen;

    // Start is called before the first frame update
    void Start()
    {
    	victoryScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points: " + player.Points;

        if (player.Win)
        {
        	victoryScreen.gameObject.SetActive(true);
        	// gameObject.GetComponent<Spawner>().enabled = false;
        }
    }
}
