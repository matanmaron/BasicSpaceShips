using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float SideSpeed;
    public Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d.velocity = new Vector2(Random.Range(0,5), Random.Range(0,5));
        Invoke("Dead", 1);
    }

    void Dead()
    {
        if (transform.position.y < -20)
        {
            Destroy(transform.gameObject);
        }
        Invoke("Dead", 1);
    }

    public void PlayExplosion()
    {
        GetComponent<AudioSource>().Play();
    }
}
