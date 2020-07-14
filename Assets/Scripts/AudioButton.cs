using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] GameObject muteImg = null;
    bool mute = false;

    void Start()
    {
        if (GameManager.Instance.mute)
        {
            onMute();
        }
    }

    public void onMute()
    {
        mute = !mute;
        muteImg.SetActive(mute);
    }
}