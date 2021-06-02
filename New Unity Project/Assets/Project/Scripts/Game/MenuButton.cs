using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

	[Header("UI")]
	public Image menu;
	public GameObject tutorialScreen;
	public GameObject invalidUpgradeScreen;
	public Text invalidUpgradeText;
	public Text catHouseText;
	private int catHouseDupReq = 2;
	private int catHousePointsReq = 10;

	[Header("ReferencePlayer")]
	public PlayerInteraction player;

	// Start is called before the first frame update
    void Start()
    {
    	menu.gameObject.SetActive(false);
    	tutorialScreen.SetActive(false);
    	invalidUpgradeScreen.SetActive(false);
    }


	public void ShowMenu()
	{
		// Set the menu screen (same scene) to true, and display the approrpriate items on the screen
		menu.gameObject.SetActive(true);

		// Display the correct values on the item's effect in the menu screen
		// Cat House (Item)
		if (player.IdleRate == 1)
		{
			catHouseText.text = "Current: 1cat/3seconds";
		}
		else if (player.IdleRate == 2)
		{
			catHouseText.text = "Current: 2cat/3seconds";
		}
		else if (player.IdleRate == 3)
		{
			catHouseText.text = "Current: 3cat/3seconds";
		}
	}

	// Need Testing
	public void UpgradeCatHouse()
	{
		if (player.IdleRateTracker >= catHouseDupReq && player.Points >= catHousePointsReq)
		{
			player.IdleRate = player.IdleRate + 1;
			catHousePointsReq *= 2;
			catHousePointsReq *= 2;
			player.Points = player.Points - catHousePointsReq;
		}
		else
		{
			invalidUpgradeScreen.SetActive(true);
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + catHousePointsReq + 
			"\nCurrent cat house count: " + player.IdleRateTracker + 
			"\nCat house count needed: " + catHouseDupReq;
		}
	}

	// Close Invalid Upgrade Screen button
	public void CloseInvalidUpgradeScreen()
	{
		invalidUpgradeScreen.SetActive(false);
	}

	public void ResumeGame()
	{
		menu.gameObject.SetActive(false);
	}

	public void TutorialButton()
	{
		tutorialScreen.SetActive(true);
	}

	public void CloseTutorialScreen()
	{
		tutorialScreen.SetActive(false);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
