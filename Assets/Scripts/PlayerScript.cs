using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public int Speed;
    public Rigidbody2D rb;
    public Transform spawned;
    private int health = 3;
    private bool dead;
	public ParticleSystem boom;

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
		Instantiate(boom, transform.position, boom.transform.rotation);
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            return;
        }
        if (!dead)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 tempVect = new Vector3(h, v, 0);
            tempVect = tempVect.normalized * Speed * Time.deltaTime;
            rb.MovePosition(rb.transform.position + tempVect);

            //shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(spawned, transform.position, transform.rotation);
            }
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
