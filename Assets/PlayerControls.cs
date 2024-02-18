using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velocity;
    float movementDi;
    float airTime;
    public float speed;
    public float jumpForce;
    public float jumpTimer;
    bool isGrounded;
    public float GroundHeight;
    public float collRadius;
    public LayerMask GroundTouchy;
    Vector2 sphereLocation;
    public GameObject currentCheckpoint;
    public GameObject wall;
    public float wallToCheckpointDistance;

    public float springPower;
    public bool onSpring = false;


    float muddy = 1;
    public float mudVar;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        sphereLocation = new Vector2(transform.position.x, transform.position.y - GroundHeight);
        isGrounded = Physics2D.OverlapCircle(sphereLocation,collRadius,GroundTouchy);
        print(isGrounded);

        

       if(Input.GetButtonDown("Jump") && isGrounded == true && airTime <= 0)
        {
            airTime = jumpTimer;
        }




        

    }




    private void FixedUpdate()
    {
        velocity.x = Input.GetAxisRaw("Horizontal") * speed * muddy;
       
        //print(airTime);
        if (airTime > 0)
        {


            airTime -= Time.deltaTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        

        if (!onSpring)
        {
            rb.velocity = new Vector2(velocity.x, rb.velocity.y);
        }
        else
        {
            Invoke("onSpringOff", 0.2f);
        }

        if(muddy < 1)
        {
            muddy += Time.deltaTime* .5f;


        }
        else if (muddy > 1)
        {
            muddy = 1;

        }



        rb.velocity = new Vector2(velocity.x,rb.velocity.y);
    
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sphereLocation, collRadius);

    }


    void onSpringOff()
    {
        onSpring = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        switch(col.gameObject.tag)
        {
            case "spring":
                onSpring = true;
                print("spring");
                rb.AddForce(new Vector2(-springPower, 0.3f), ForceMode2D.Impulse);
                break;

            case "wall":
                wall.transform.position = currentCheckpoint.transform.position - new Vector3(wallToCheckpointDistance, 0, 0);
                transform.position = currentCheckpoint.transform.position;
                break;

            case "levelEnd":
               // SceneManager.LoadScene("winScene");
                break;

            case "Mud":
                muddy = mudVar;
                break;
            case "checkpoint":
                currentCheckpoint = col.gameObject;
                break;



        }





    }





}
