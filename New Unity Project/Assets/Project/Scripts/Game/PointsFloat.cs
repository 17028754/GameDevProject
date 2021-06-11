using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsFloat : MonoBehaviour
{
    public GameObject floatingPoints;
    private PlayerInteraction c;
    public bool pointsFloated = false;


    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<PlayerInteraction>().GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void Floatpoints(Vector3 y)
    {
        if (c.clicked)
        {

            Instantiate(floatingPoints, y, Quaternion.identity);
            c.clicked = false;
            pointsFloated = true;

        }
    }
}
