using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class righting : MonoBehaviour
{
    Rigidbody2D platform;
    public float speed = 3;
    public float timer = 30;
    bool touchedPlat = false;
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchedPlat = true;
        }
    }
    void FixedUpdate()
    {
        if (touchedPlat)
        {
            platform.AddForce(new Vector2(speed, 0));
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
