using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] GameObject[] hearts = null;
    [SerializeField] bl_Joystick Joystick;

    private bool dead;
    private int score = 0;
    public int Speed;
    public Rigidbody2D rb;
    public Transform spawned;
    public int health = 3;
    public int highscore;
    public string highname;

    public string _name;
    public ParticleSystem boom;

    public Text _score;
    public InputField _inputField;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public Vector3 ToPosition;

    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        _name = System.Environment.UserName;
        LoadFile();
        _score.text = $"high score: {highscore} ({highname})";
        dead = false;

    }

    private void Player_OnTouch(bool data)
    {
        if (data)
        {
            Shoot();
        }
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
            HitMe();
        }

    }

    void ShipDamage()
    {
        GetComponent<Animator>().Play("ShipDamage");
    }

    private void HitMe()
    {
        health--;
        if (health == 0)
        {
            KillMe();
            return;
        }
        ShipDamage();
        hearts[health].SetActive(false);
    }

    public void KillMe()
    {
        if (dead)
        {
            return;
        }
        Debug.Log("you dead !");
        dead = true;
        Instantiate(boom, transform.position, boom.transform.rotation);
        Destroy(gameObject, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOn)
        {
            return;
        }
        if (!dead)
        {
            HandleKeys();
        }
    }

    private void HandleKeys()
    {
        float h = Input.GetAxis(horizontalAxis);
        float v = Input.GetAxis(verticalAxis);
        float jh = Joystick.Horizontal;
        float jv = Joystick.Vertical;

        //Debug.Log($"{jh+h},{jv+v}");
        Vector3 tempVect = new Vector3(jh+h, jv+v, 0);
        tempVect = tempVect.normalized * Speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);

        //shoot

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(ShootTimer());
            Instantiate(spawned, transform.position, transform.rotation);
        }
    }

    IEnumerator ShootTimer()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    public void ScoreUp()
    {
        score++;
        _score.text = $"score: {score} ({_name})";
        if (score > highscore)
        {
            highscore = score;
        }
        SaveFile();
    }

    public void SaveFile()
    {
        PlayerPrefs.SetString("_name", _name);
        PlayerPrefs.SetInt("score", score);
    }

    public void LoadFile()
    {
        highscore = PlayerPrefs.GetInt("score", 0);
        highname = PlayerPrefs.GetString("_name", string.Empty);
    }
}