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
    private bool dead;
    private int score = 0;
    private bool joystick;
    public int Speed;
    public Rigidbody2D rb;
    public Transform spawned;

    public int highscore;
    public string highname;

    public string _name;
    public ParticleSystem boom;

    public Text _score;
    public Text _highscore;
    public InputField _inputField;
    public GameObject _inputpanel;
    public GameObject _joystick;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public Vector3 ToPosition;

    // Start is called before the first frame update
    void Start()
    {
        joystick = false;
        _joystick.SetActive(false);
        LoadFile();
        _highscore.text = highname + " " + highscore.ToString();

        score = 0;

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
        if (collision.gameObject.name.Contains("Enemy"))
        {
            //GetComponent<AudioSource>().Play();
            KillMe();
        }

    }

    void ShipDamage()
    {
        GetComponent<Animator>().Play("ShipDamage");
    }

    private void KillMe()
    {
        if (score > highscore)
        {
            _inputpanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            KillMeFinale();
        }
    }

    public void InputOK()
    {
        _name = _inputField.text;
        if (_inputField.text.Length > 8)
        {
            _name = _inputField.text.Substring(0, 8);
        }
        SaveFile();
        _inputpanel.SetActive(false);
        Time.timeScale = 1;
        KillMeFinale();
    }

    public void KillMeFinale()
    {
        dead = true;
        Destroy(transform.gameObject);
        Instantiate(boom, transform.position, boom.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!joystick)
            {
                joystick = true;
                _joystick.SetActive(true);
            }
        }
        if (!dead)
        {

            HandleKeys();

            OutOfScreen();
        }
    }

    private void HandleKeys()
    {
        float h = SimpleInput.GetAxis(horizontalAxis);
        float v = SimpleInput.GetAxis(verticalAxis);

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * Speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);

		//shoot

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    private void OutOfScreen()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        Vector3 speed = rb.velocity;
        if (pos.x == 0 || pos.x == 1)
            speed.x = 0;
        if (pos.y == 0 || pos.y == 1)
            speed.y = 0;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
        rb.velocity = speed;
    }

        public void Shoot()
    {
        Instantiate(spawned, transform.position, transform.rotation);
    }


    public void ScoreUp()
    {
        score++;
        _score.text = score.ToString();
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