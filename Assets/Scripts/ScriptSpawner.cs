using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpawner : MonoBehaviour
{
    public Transform spawned;
    bool create;
    // Start is called before the first frame update
    void Start()
    {
        create = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!create)
        {
            create = true;
            StartCoroutine(CreateMeteor(Random.Range(2f,10f)));
        }
    }

    IEnumerator CreateMeteor(float time)
    {
        int place = Random.Range(0, 10);
        if (place>5)
        { 
            Instantiate(spawned, transform.position + new Vector3(5, 0, 0), transform.rotation);
        }
        else
        {
            Instantiate(spawned, transform.position, transform.rotation);
        }
        yield return new WaitForSeconds(time);
        create = false;
    }
}
