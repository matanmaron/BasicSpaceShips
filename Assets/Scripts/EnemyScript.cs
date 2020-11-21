using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem boom;

    private void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(transform.gameObject);
        }
        rb.velocity = Vector2.right * -ScriptEnemySpawner.Speed * Time.deltaTime * Random.Range(1f, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Laser"))
        {
            var _player = GameObject.Find("Player");
            var _script = _player.GetComponent<PlayerScript>();
            _script.ScoreUp();
            Destroy(transform.gameObject);
            Instantiate(boom, transform.position, boom.transform.rotation);
        }
        if (collision.gameObject.name.Contains("Player"))
        {
            var _player = GameObject.Find("Player");
            Destroy(transform.gameObject);
            Instantiate(boom, transform.position, boom.transform.rotation);
        }
    }
}