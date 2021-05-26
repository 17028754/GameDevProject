using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

	[Header("UI")]
	public Image menu;

	// Start is called before the first frame update
    void Start()
    {
    	menu.gameObject.SetActive(false);
    }


	public void ShowMenu()
	{
		//Set the menu screen (same scene) to true, and display the approrpriate items on the screen
		Debug.Log("Menu Button is pressed!");
		menu.gameObject.SetActive(true);
	}

	public void ResumeGame()
	{
		menu.gameObject.SetActive(false);
	}

	public void TutorialButton()
	{
		Debug.Log("Tutorial Button is pressed!");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
