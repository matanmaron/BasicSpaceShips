using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemySpawner : MonoBehaviour
{
    public Transform spawned;
    bool create;
    private float min = 3f;
    private float max = 8f;
    public static int Speed = 200;

    // Start is called before the first frame update
    void Start()
    {
        create = false;
        StartCoroutine(UpDifficulty());
    }

    // Update is called once per frame
    void Update()
    {
        if (!create)
        {
            create = true;
            StartCoroutine(CreateEnemy(Random.Range(min, max)));
        }
    }

    IEnumerator CreateEnemy(float time)
    {
        transform.position = new Vector2(10, Random.Range(-4f, 4f));
        Instantiate(spawned, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        create = false;
    }

    IEnumerator UpDifficulty()
    {
        yield return new WaitForSeconds(20);//20
        Debug.Log("def1");
        min = 1;
        max = 5;
        yield return new WaitForSeconds(60);//40
        Debug.Log("def2");
        min = 0.5f;
        max = 3;
        yield return new WaitForSeconds(300);//300
        Debug.Log("def3");
        min = 0.1f;
        max = 1;
        Speed = 500;
        Debug.Log("speed 500");
    }
}
