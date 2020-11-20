using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] float Speed;
    float resetpos = 17.75f;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
        if (transform.position.x<=-resetpos)
        {
            transform.SetPositionAndRotation(new Vector3(0, transform.position.y, transform.position.z), transform.rotation);
        }
    }
}