using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemySpawner : MonoBehaviour
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
            StartCoroutine(CreateEnemy(Random.Range(0.5f, 3f)));
        }
    }

    IEnumerator CreateEnemy(float time)
    {
        transform.position = new Vector2(10, Random.Range(-4f, 4f));
        Instantiate(spawned, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        create = false;
    }
}
