using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    [SerializeField]
    float speed = 4;
    [SerializeField]
    float jump = 4;
    Rigidbody2D rb2d;
    [SerializeField]
    float gravity = 4;
    [SerializeField]
    GameObject bulletPrefab;
    Collider2D playerC;
    Collider2D groundC;
    bool onGround;
    Vector3 placeholder;
    bool wallTouchLeft = false;
    bool wallTouchRight = false;
    public float look = 1;
    
    [SerializeField]
    Vector3 wallJumpLeft = new Vector3 (4, 4, 0);
    [SerializeField]
    Vector3 wallJumpRight = new Vector3(-4, 4, 0);
    int jumpCount = 2;
    bool wallJumpDown = true;
    public bool wallJumpLefting = false;
    bool wallJumpRighting = false;
    float timer = 0;

    private void Awake()
    {
        GameManager.Instance.MyCharacter = this;
    }


    // Use this for initialization
    void Start()
    {
        playerC = GetComponent<BoxCollider2D>();
        groundC = GameObject.Find("Ground").GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        wallJumpLefting = JumpTimer();


        RaycastHit2D landright = Physics2D.Raycast(transform.position - new Vector3(-.04f, .05f, 0), new Vector2(0, velocity[1]), Mathf.Abs(velocity[1]));
        RaycastHit2D landleft = Physics2D.Raycast(transform.position - new Vector3(.04f, .05f, 0), new Vector2(0, velocity[1]), Mathf.Abs(velocity[1]));
        // landing
        if (landleft.collider ? landleft.collider.tag == "Ground" : false || landright.collider ? landright.collider.tag == "Ground" : false)
        {
         
                placeholder = transform.position;
                if (landleft.collider)
                {
                    placeholder[1] -= landleft.distance;

                }
                else if (landright.collider)
                    placeholder[1] -= landright.distance;
                    

                
                   
                transform.position = placeholder;
                velocity[1] = 0;
                jumpCount = 2;

                Debug.Log("on ground");
                onGround = true;
            
             
        }

        else
        {
            Debug.Log("not on ground");
            velocity[1] -= gravity * Time.deltaTime;
            onGround = false;
        }





        //hitting wall with left side
        RaycastHit2D wallHitLeftBottom = Physics2D.Raycast(transform.position - new Vector3(.05f, -.04f, 0), new Vector2(-1, 0), -(velocity[0]));
       
        RaycastHit2D wallHitLeftTop = Physics2D.Raycast(transform.position - new Vector3(.05f, .04f, 0), new Vector2(-1, 0), -(velocity[0]));
        
        
        if (wallHitLeftTop.collider ? wallHitLeftTop.collider.tag == "Ground" : false || wallHitLeftBottom.collider ? wallHitLeftBottom.collider.tag == "Ground" : false)
        {
            
            placeholder = transform.position;

            if (wallHitLeftBottom.collider)
            {
                placeholder[0] -= wallHitLeftBottom.distance;

            }
            else if (wallHitLeftTop.collider)
                placeholder[0] -= wallHitLeftTop.distance;


            transform.position = placeholder;
            velocity[0] = 0;
            wallTouchLeft = true;

            Debug.Log("touching wall");

        }
        else
        {
            wallTouchLeft = false;
        }



        //hitting wall with right side
        RaycastHit2D wallHitRightBottom = Physics2D.Raycast(transform.position + new Vector3(.05f, -.04f, 0), new Vector2(1, 0), ( velocity[0]));
        RaycastHit2D wallHitRightTop = Physics2D.Raycast(transform.position + new Vector3(.05f, .04f, 0), new Vector2(1, 0), (velocity[0]));
        if (wallHitRightTop.collider ? wallHitRightTop.collider.tag == "Ground" : false || wallHitRightBottom.collider ? wallHitRightBottom.collider.tag == "Ground" : false)
        {

            placeholder = transform.position;

            if (wallHitRightBottom.collider)
            {
                placeholder[0] += wallHitRightBottom.distance;

            }
            else if (wallHitRightTop.collider)
                placeholder[0] += wallHitRightTop.distance;


            transform.position = placeholder;
            velocity[0] = 0;
            wallTouchRight = true;

            Debug.Log("touching wall");

        }
        else
        {
            wallTouchRight = false;
        }

        //enemy collision
        RaycastHit2D enemyHitRightTop = Physics2D.Raycast(transform.position + new Vector3(.05f, .04f, 0), new Vector2(1, 0), velocity[0]);
        if (enemyHitRightTop.collider)
        {
            if(enemyHitRightTop.collider.tag == "Enemy")
            Destroy(gameObject);
        }

        //walljumps
        if (wallTouchLeft && Input.GetKeyDown(KeyCode.W) && !onGround && wallJumpDown)
        {
            wallJumpLefting = true; 

            
            
            
           
            SoundManager.Instance.PlayOneShot(SoundEffect.wallJump);
        }
        if (wallTouchRight && Input.GetKeyDown(KeyCode.W) && !onGround && wallJumpDown)
        {
            SoundManager.Instance.PlayOneShot(SoundEffect.wallJump);
           

            velocity = wallJumpRight;

             
            

        }
        if (wallTouchRight && Input.GetKey(KeyCode.A))
        {
            moveleft();
        }
        if (wallTouchLeft && Input.GetKey(KeyCode.D))
        {
            moveright();
        }
        if (!wallTouchLeft)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveleft();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {

        }
        if (!wallTouchRight)
        {
            if (Input.GetKey(KeyCode.D))
            {
                moveright();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpUp();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            landright = Physics2D.Raycast(transform.position - new Vector3(-.04f, .05f, 0), new Vector2(0, velocity[1]), -velocity[1]);
            landleft = Physics2D.Raycast(transform.position - new Vector3(.04f, .05f, 0), new Vector2(0, velocity[1]), -velocity[1]);
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            velocity[0] = 0;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.A))
        {
            velocity[0] = 0;
            moveleft();
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        {
            velocity[0] = 0;
            moveright();
        }
        if (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            velocity[0] = 0;
            moveright();
        }
        if (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            velocity[0] = 0;
            moveleft();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (wallJumpLefting)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                wallJumpLefting = false;
                timer = 0;

            }
        }



        velocity[0] = Mathf.Clamp(velocity[0], -.05f, .05f);
        velocity[1] = Mathf.Clamp(velocity[1], -.25f, .25f);
        
        transform.position += velocity;
        Debug.Log(velocity[0]);
        


    }

    void moveright()
    {
        velocity += speed * Vector3.right * Time.deltaTime;
        look = 1;
    }
    void moveleft()
    {
        velocity += speed * Vector3.left * Time.deltaTime;
        look = -1;
    }
    void jumpUp()
    {
        if (jumpCount > 0)
        {
            velocity[1] = jump * Time.deltaTime;
            
            if (!wallTouchLeft && !wallTouchRight)
            {                
                jumpCount--;
                SoundManager.Instance.PlayOneShot(SoundEffect.Jump);

            }
            
        }        
        
       
    }
    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullets>().look2 = look;
    }
    bool JumpTimer()
    {
        
        timer += Time.deltaTime;
        if (timer < 0)
            return true;
        else
            return false;
    }


}