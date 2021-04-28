using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The length of the vector is square root of (x*x+y*y+z*z)


public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Transform contactChecker;
    public Vector2 p;
    private bool moveLeft;
    private bool moveRight;
    private bool moveDown;
    private bool moveUp;
    
    //public float speed;
    //public Vector3 normalized;
    //public Vector3 move;


    public string[] options = new string[] {"HorizontalDirection", "DigonalDirection"};


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        p = transform.position;
        int rand = Random.Range(0, 2);
        Invoke(options[rand],0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(move * speed * Time.deltaTime);
    }

    //Consistent calling just for check walls
    /*
    void FixedUpdate()
    {

        RayCastHit2D contactCheck = Physics2D.Raycast(contactChecker.position, Vector2.left, rayLength);
        if(contactCheck == true)
        {
            if(moveLeft == true && moveDown == true)
            {
                transform.eulerAngles = new Vector2(0, 315);
            }
            else if (moveLeft == true && moveUp == true)
            {
                transform.eulerAngles = new Vector2(0, 225);
            }
            else if (moveRight == true && moveDown == true)
            {
                transform.eulerAngles = new Vector2(0, 45);
            }
            if(moveRight == true && moveUp == true)
            {
                transform.eulerAngles = new Vector2(0, 135);
            }

        }
    }
    */


    //Called once
    void HorizontalDirection()
    {
        //to the right
        if (p.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
            rb2d.AddForce(new Vector2(100, 0));
            moveRight = true;
            //move = new Vector3(5, 0, 0);

        }
        //to the left
        else

        {
            transform.eulerAngles = new Vector2(0, 0);
            rb2d.AddForce(new Vector2(-100, 0));
            moveLeft = true;
            //move = -(new Vector3(5, 0, 0));

        }
    }
    void DigonalDirection()
    {

        if (p.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
            moveLeft = true;
            //object at top left
            if (p.y > 0)
            {
                rb2d.AddForce(new Vector2(100, -45));
                moveDown = true;
                //move = new Vector3(5, -5, 0);

            }
            //object at bottom left
            else
            {
                rb2d.AddForce(new Vector2(100, 45));
                moveUp = true;
                //move = new Vector3(5, 5, 0);

            }
        }
        //object at top right
        else if (p.x > 0)
        {

            transform.eulerAngles = new Vector2(0, 0);
            moveRight = true;
            //object at top right
            if (p.y > 0)
            {
                rb2d.AddForce(new Vector2(-100, -45));
                moveDown = true;
                //move = -(new Vector3(-5, -5, 0));
            }

            //object at bottom right
            else
            {
                rb2d.AddForce(new Vector2(-100, 45));
                moveUp = true;
                //move = -(new Vector3(-5, 5, 0));
            }
        }
         
    }
    
}
