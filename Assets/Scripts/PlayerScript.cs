using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int Speed;
    public Rigidbody2D rb;
    public Transform spawned;
    private int health = 3;
    private bool dead;
    private int score = 0;
    private bool godmod;
	public ParticleSystem boom;
    public Text _life;
    public Text _score;

	// Start is called before the first frame update
	void Start()
    {
        godmod = false;
        score = 0;
        health = 3;
        _life.text = health.ToString();
        _score.text = score.ToString();
        dead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Meteor"))
        {
            KillMe();
            return;
        }
        if (collision.gameObject.name.Contains("Enemy") && !godmod)
        {
            GetComponent<AudioSource>().Play();
            godmod = true;
            Invoke("NoGod", 2);
            health--;
            ShipDamage();
            Invoke("ShipDamage", 0.5f);
            Invoke("ShipDamage", 1f);
            Invoke("ShipDamage", 1.5f);
        }
        if (health==0)
        {
            KillMe();
        }
        _life.text = health.ToString();

    }

    void ShipDamage()
    {
        GetComponent<Animator>().Play("ShipDamage");
    }

    void NoGod()
    {
        godmod = false;
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

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
