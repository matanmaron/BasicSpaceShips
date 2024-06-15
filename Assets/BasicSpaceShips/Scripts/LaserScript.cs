using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int Speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Destroy(transform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * Speed * Time.deltaTime;
    }


}
