using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int Speed;
    public Rigidbody2D rb;
    public Transform spawned;
    private int health = 3;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Meteor"))
        {
            KillMe();
            return;
        }
        if (collision.gameObject.name.Contains("Enemy"))
        {
            health--;
            GetComponent<Animator>().Play("ShipDamage");
        }
        if (health==0)
        {
            KillMe();
        }
    }

    private void KillMe()
    {
        dead = true;
        Destroy(transform.gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!dead)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 tempVect = new Vector3(h, v, 0);
            tempVect = tempVect.normalized * Speed * Time.deltaTime;
            rb.MovePosition(rb.transform.position + tempVect);
            /*
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                rb.velocity = Vector2.right * Speed * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                rb.velocity = Vector2.right * -Speed * Time.deltaTime;
            }
            else
            {
                rb.velocity = Vector2.right * 0;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                rb.velocity = Vector2.up * Speed * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                rb.velocity = Vector2.down * Speed * Time.deltaTime;
            }
            else
            {
                rb.velocity = Vector2.up * 0;
            }
            //rb.velocity = Vector2.right * Speed * Time.deltaTime;
            */
            //shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(spawned, transform.position, transform.rotation);
            }
        }
    }
}
