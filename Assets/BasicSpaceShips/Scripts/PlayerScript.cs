using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject[] hearts = null;
    [SerializeField] bl_Joystick Joystick;
    [SerializeField] InputActionReference inputActionMove;
    [SerializeField] InputActionReference inputActionShoot;
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
        Vector2 move = inputActionMove.action.ReadValue<Vector2>();
        float jh = Joystick.Horizontal;
        float jv = Joystick.Vertical;

        Debug.Log($"{jh+move.x},{jv+move.y}");
        Vector3 tempVect = new Vector3(jh + move.x, jv + move.y, 0);
        tempVect = tempVect.normalized * Speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);
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
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    public void ScoreUp()
    {
        score++;
        _score.text = $"score: {score} ({_name})";
        if (score > highscore)
        {
            highscore = score;
            SaveFile();
        }
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

    private void OnEnable()
    {
        inputActionShoot.action.started += ShootKey;
    }

    private void OnDisable()
    {
        inputActionShoot.action.started -= ShootKey;
    }

    private void ShootKey(InputAction.CallbackContext context)
    {
        Shoot();
    }
}
