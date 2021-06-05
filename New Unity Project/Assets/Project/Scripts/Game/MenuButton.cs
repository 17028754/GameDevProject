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

	// Items' text field - start
	// Cat House
	public Text catHouseText;
	private int catHouseDupReq = 2;
	private int catHousePointsReq = 10;

	// Cat Food
	public Text catFoodText;
	private int catFoodDupReq = 2;
	private int catFoodPointsReq = 10;

	// Human Gloves
	public Text humanGlovesText;
	private int humanGlovesDupReq = 2;
	private int humanGlovesPointsReq = 10;

	// Cat Toy
	public Text catToyText;
	private int catToyDupReq = 2;
	private int catToyPointsReq = 10;

	// Cat Clothes
	public Text catClothesText;
	private int catClothesDupReq = 2;
	private int catClothesPointsReq = 10;

	// Cat Shoes
	public Text catShoesText;
	private int catShoesDupReq = 2;
	private int catShoesPointsReq = 10;

	// Items' text field - end

	[Header("ReferencePlayer")]
	public PlayerInteraction player;

	// Start is called before the first frame update
    void Start()
    {
    	menu.gameObject.SetActive(false);
    	tutorialScreen.SetActive(false);
    	invalidUpgradeScreen.SetActive(false);
    }

  //   void Update()
  //   {
  //   	invalidUpgradeText.text = "Unable to upgrade..." + 
		// "\nCurrent points: " + player.Points + 
		// "\nPoints needed: " + catHousePointsReq + 
		// "\nCurrent cat house count: " + player.IdleRateMTracker + 
		// "\nCat house count needed: " + catHouseDupReq;

  //   }


	public void ShowMenu()
	{
		// Set the menu screen (same scene) to true, and display the approrpriate items on the screen
		menu.gameObject.SetActive(true);

		// Display the correct values on the item's effect in the menu screen - start
		// Cat House 
		catHouseText.text = "Current: +" + player.IdleRateM + "%";

		// Cat Food
		catFoodText.text = "Current: +" + player.IdleRateA + " points";

		// Human Gloves
		humanGlovesText.text = "Current: +" + player.ManualCollect + "%";

		// Cat Toy
		catToyText.text = "Current: +" + player.IncreaseDamage + "%";

		// Cat Clothes
		catClothesText.text = "Current: +" + player.CritDamage + "%";

		// Cat Shoes
		catShoesText.text = "Current: +" + player.CritChance + "%";

		// Display the correct values on the item's effect in the menu screen - end

	}

	// Bind to the item's respective upgrade button - start
	// Need to fine tune (Adjust the values accordingly)
	public void UpgradeCatHouse()
	{
		if (player.IdleRateMTracker >= catHouseDupReq && player.Points >= catHousePointsReq)
		{
			// Debug.Log("Before upgrade: ");
			// Debug.Log("DupReq: " + catHouseDupReq);
			// Debug.Log("PointsReq: " + catHousePointsReq);
			player.IdleRateM = player.IdleRateM + 1;
			player.Points = player.Points - catHousePointsReq;
			catHouseDupReq *= 2;
			catHousePointsReq *= 2;
			// Debug.Log("After upgrade: ");
			// Debug.Log("DupReq: " + catHouseDupReq);
			// Debug.Log("PointsReq: " + catHousePointsReq);
		}
		else
		{
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + catHousePointsReq + 
			"\nCurrent cat house count: " + player.IdleRateMTracker + 
			"\nCat house count needed: " + catHouseDupReq;
			invalidUpgradeScreen.SetActive(true);
		}
	}

	// Need Testing
	public void UpgradeCatFood()
	{
		if (player.IdleRateATracker >= catFoodDupReq && player.Points >= catFoodPointsReq)
		{
			player.IdleRateA = player.IdleRateA + 1;
			player.Points = player.Points - catFoodPointsReq;
			catFoodDupReq *= 2;
			catFoodPointsReq *= 2;
		}
		else
		{
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + catFoodPointsReq + 
			"\nCurrent cat food count: " + player.IdleRateATracker + 
			"\nCat food count needed: " + catFoodDupReq;
			invalidUpgradeScreen.SetActive(true);
		}
	}

	// Need Testing
	public void UpgradeHumanGloves()
	{
		if (player.ManualCollectTracker >= humanGlovesDupReq && player.Points >= humanGlovesPointsReq)
		{
			player.ManualCollect = player.ManualCollect + 1;
			player.Points = player.Points - humanGlovesPointsReq;
			humanGlovesDupReq *= 2;
			humanGlovesPointsReq *= 2;
		}
		else
		{
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + humanGlovesPointsReq + 
			"\nCurrent human gloves count: " + player.ManualCollectTracker + 
			"\nHuman gloves count needed: " + humanGlovesDupReq;
			invalidUpgradeScreen.SetActive(true);
		}
	}

	// Need Testing
	public void UpgradeCatToy()
	{
		if (player.IncreaseDamageTracker >= catToyDupReq && player.Points >= catToyPointsReq)
		{
			player.IncreaseDamage = player.IncreaseDamage + 1;
			player.Points = player.Points - catToyPointsReq;
			catToyDupReq *= 2;
			catToyPointsReq *= 2;
		}
		else
		{
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + catToyPointsReq + 
			"\nCurrent cat toy count: " + player.IncreaseDamageTracker + 
			"\nCat toy count needed: " + catToyDupReq;
			invalidUpgradeScreen.SetActive(true);
		}
	}

	// Need Testing
	public void UpgradeCatClothes()
	{
		if (player.CritDamageTracker >= catClothesDupReq && player.Points >= catClothesPointsReq)
		{
			player.CritDamage = player.CritDamage + 1;
			player.Points = player.Points - catClothesPointsReq;
			catClothesDupReq *= 2;
			catClothesPointsReq *= 2;
		}
		else
		{
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + catClothesPointsReq + 
			"\nCurrent cat clothes count: " + player.CritDamageTracker + 
			"\nCat clothes count needed: " + catClothesDupReq;
			invalidUpgradeScreen.SetActive(true);
		}
	}

	// Need testing
	public void UpgradeCatShoes()
	{
		if (player.CritChanceTracker >= catShoesDupReq && player.Points >= catShoesPointsReq)
		{
			player.CritChance = player.CritChance + 1;
			player.Points = player.Points - catShoesPointsReq;
			catShoesDupReq *= 2;
			catShoesPointsReq *= 2;
		}
		else
		{
			invalidUpgradeText.text = "Unable to upgrade..." + 
			"\nCurrent points: " + player.Points + 
			"\nPoints needed: " + catShoesPointsReq + 
			"\nCurrent cat shoes count: " + player.CritChanceTracker + 
			"\nCat shoes count needed: " + catShoesDupReq;
			invalidUpgradeScreen.SetActive(true);
		}
	}


	// Bind to the item's respective upgrade button - end

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
