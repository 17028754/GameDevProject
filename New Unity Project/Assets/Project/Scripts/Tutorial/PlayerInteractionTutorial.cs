using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteractionTutorial : MonoBehaviour
{

    // UI for idle progression when player closes the app
    [Header("UI")]
    public GameObject tutorial_1;
    public Text tutorial_1_t;

    private int counter = 0;

    // Talk about thanking and greatful at the end of the game

    // When game start
    void Start()
    {
        tutorial_1_t.text = "Welcome to Cu-To!";
 
    }

    // Next screen button
    public void nextTutorial()
    {   

        if (counter == 0)
        {
        tutorial_1_t.text = "Click on the cat that looks like the orange colour cat at the left bottom to collect points.";
        }

        else if (counter == 1)
        {
        tutorial_1_t.text = "Click on the cat that looks like the box cat at the center bottom to collect points and items that can impact your progression.";
        }

        else if (counter == 2)
        {
        tutorial_1_t.text = "Click on the cat that looks like the blue cat at the right bottom to collect more points, but be warned they move faster.";
        }
        else if (counter == 3)
        {
        tutorial_1_t.text = "You can see how many points you have collected at the top.";
        }
        else if (counter == 4)
        {
        tutorial_1_t.text = "You can see how many points you can collect per second while you are IDLE at the left top.";
        }
        else if (counter == 5)
        {
        tutorial_1_t.text = "The top red colour health bar is the progression bar of the current stage," +
        "\nwhen it is completely filled, a mysterious creature will start moving on the screen," +
        "\nonce that happens, the health bar at the top will now reflect the mysterious creature's current hp.";
        }
        else if (counter == 6)
        {
        tutorial_1_t.text = "The menu icon on the top right is the upgrade screen." +
        "\nit will display a list of items that you can collect and upgrade, as well as how can these items affect your progression in game" +
        "\nthe points you collect can be used as a currency to upgrade these items.";
        }
        else if (counter == 7)
        {
        tutorial_1_t.text = "Now that you have an idea on how the game works and what to do, let's get started!";
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }

        counter++;

    }
}
