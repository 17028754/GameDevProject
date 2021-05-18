using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The length of the vector is square root of (x*x+y*y+z*z)


public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    //public float rayLength;
    public Transform contactChecker;
    public Vector2 p;
    public bool appear = false;
    public bool bossCat;
    public bool commonCat;
    public bool uniqueCat;
    public bool boxCat;
    //private bool moveLeft;
    //private bool moveRight;
    //private bool moveDown;
    //private bool moveUp;

    //public float speed;
    //public Vector3 normalized;
    //public Vector3 move;

    private Spawner spawnScript;

    private float MinY = -4.5f;
    private float MaxY = 0f;

    private float leftMinX = -12f;
    private float leftMaxX = -10f;

    private float rightMinX = 10f;
    private float rightMaxX = 12f;

    public int initialBossCatHP = 1000;
    private int bossCatHP = 0;
    public int BossCatHP { get { return bossCatHP; } set {bossCatHP = value;}}

    public string[] options = new string[] {"HorizontalDirection", "DigonalDirection"};


    // Start is called before the first frame update
    void Start()
    {
    	spawnScript = FindObjectOfType<GameManager>().GetComponent<Spawner>();	
        rb2d = GetComponent<Rigidbody2D>();
        p = transform.position;
        if (bossCat)
        {
        	bossCatHP = initialBossCatHP;
        }
    }

        // Update is called once per frame
    void Update()
    {
        //transform.Translate(move * speed * Time.deltaTime);

    }


    void OnEnable()
    {
    	rb2d = GetComponent<Rigidbody2D>();
        int rand = Random.Range(0, 2);
        Invoke(options[rand], 0);  	
    }

   
    void FixedUpdate()
    {
        if ((transform.position.x > 12) || (transform.position.x < -12))
        {
            if (bossCat == true)
            {
                if (commonCat == true)
                {
                    gameObject.SetActive(false);
                }

                gameObject.SetActive(false);
                appear = true;
            }

            else if (commonCat == true)
            {
                gameObject.SetActive(false);
                spawnScript.CommonCatSpawnned -= 1;

            }

            // At the moment, just make special cats bounce off a special wall instead of despawning it and respawning it
            // Need to fix animation (flip to the other side when bounced)
            // else if (uniqueCat == true)
            // {
            // 	gameObject.SetActive(false);
            // 	spawnScript.UniqueCatSpawnned -= 1;
            // }

            // else if (boxCat == true)
            // {
            // 	gameObject.SetActive(false);
            // 	spawnScript.BoxCatSpawnned -= 1;
            // }

        }

        //need to say after some time or change coordinate to be further
        //if((p.x == 480.0f) || (p.x == 420.5f))


        /*

         RayCastHit2D contactCheck = Physics2D.Raycast(contactChecker.position, Vector2.left, rayLength);
         Debug.DrawRay(contactChecker.position, Vector2.left * rayLength, Color.red);
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
        */

    }
    //Called once
    void HorizontalDirection()
    {
        //to the right
        if (p.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
            rb2d.AddForce(new Vector2(100, 0));
            //moveRight = true;
            //move = new Vector3(5, 0, 0);

        }
        //to the left
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rb2d.AddForce(new Vector2(-100, 0));
            //moveLeft = true;
            //move = -(new Vector3(5, 0, 0));

        }
    }
    void DigonalDirection()
    {

        if (p.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
            //moveLeft = true;
            //object at top left
            if (p.y > 0)
            {
                rb2d.AddForce(new Vector2(100, -45));
                //moveDown = true;
                //move = new Vector3(5, -5, 0);

            }
            //object at bottom left
            else
            {
                rb2d.AddForce(new Vector2(100, 45));
                //moveUp = true;
                //move = new Vector3(5, 5, 0);

            }
        }
        //object at top right
        else if (p.x > 0)
        {

            transform.eulerAngles = new Vector2(0, 0);
            //moveRight = true;
            //object at top right
            if (p.y > 0)
            {
                rb2d.AddForce(new Vector2(-100, -45));
                //moveDown = true;
                //move = -(new Vector3(-5, -5, 0));
            }

            //object at bottom right
            else
            {
                rb2d.AddForce(new Vector2(-100, 45));
                //moveUp = true;
                //move = -(new Vector3(-5, 5, 0));
            }
        }
         
    }


    // Randomly generate the position for next cat spawn
    public Vector3 spawnCatPosition(bool left)
    {
        float rand_Y = Random.Range(MinY, MaxY);
        float rand_X = 0f;

        if (left == true)
        {
            rand_X = Random.Range(leftMinX, leftMaxX);
        }else
        {
            rand_X = Random.Range(rightMinX, rightMaxX);
        }
        Vector3 position = new Vector3(rand_X, rand_Y, 0f);

        return position;
    }
    
}
