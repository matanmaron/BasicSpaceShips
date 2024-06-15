using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMuteScript : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.Instance.mute)
        {
            GetComponent<AudioSource>().mute = true;
        }
    }
}
