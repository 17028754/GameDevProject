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

    // Start is called before the first frame update
    void Start()
    {
    	VictoryScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points: " + player.Points;

        if (player.Win)
        {
        	VictoryScreen.gameObject.SetActive(true);
        }
    }
}
