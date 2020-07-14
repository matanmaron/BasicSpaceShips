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
        //LoadFile();

        score = 0;

        _score.text = "score: "+score.ToString();
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
            if (Application.platform == RuntimePlatform.IPhonePlayer
                || Application.platform == RuntimePlatform.Android)
            {
                MobileMove();
            }
            else
            {
                HandleKeys();
            }
        }
    }

    private void HandleKeys()
    {
        float h = Input.GetAxis(horizontalAxis);
        float v = Input.GetAxis(verticalAxis);

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * Speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);

		//shoot

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }
    private void MobileMove()
    {
        float moveHorizontal = Input.acceleration.normalized.x; // left / right movement
        float moveVertical = -0.6f -Input.acceleration.normalized.z; //forward / backwards

        // main three directional movement control
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        if (movement != Vector3.zero)
        {
            rb.MovePosition(rb.transform.position + movement );
        }
        //shoot

        if (Input.touchCount > 0)
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
        _score.text = "score: " + score.ToString();
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(score, _name);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            //Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        highscore = data.score;
        highname = data.name;

       //Debug.Log(data.name);
       //Debug.Log(data.score);
    }
}

[System.Serializable]
public class GameData
{
    public int score;
    public string name;

    public GameData(int scoreInt, string nameStr)
    {
        score = scoreInt;
        name = nameStr;
    }
}