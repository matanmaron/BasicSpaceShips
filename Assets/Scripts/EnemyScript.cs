using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public ParticleSystem boom;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Dead", 1);
    }

    private void Update()
    {
        rb.velocity = Vector2.right * -Speed * Time.deltaTime * Random.Range(1f,1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Laser"))
        {
            var _player = GameObject.Find("Player");
            var _script = _player.GetComponent<PlayerScript>();
            _script.ScoreUp();
            Destroy(transform.gameObject);
            Instantiate(boom, transform.position,boom.transform.rotation);
        }
    }

    void Dead()
    {
        if (transform.position.x < -10)
        {
            Destroy(transform.gameObject);
        }
        Invoke("Dead", 1);
    }
}
