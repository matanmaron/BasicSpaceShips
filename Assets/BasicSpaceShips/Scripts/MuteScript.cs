using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MuteScript : MonoBehaviour
{
    bool mute = false;

    private void Start()
    {
        GameManager.Instance.mute = PlayerPrefs.GetInt("mute") == 1 ? true : false;
        if (GameManager.Instance.mute)
        {
            onMute();
        }
    }

    public void onMute()
    {
        mute = !mute;
        PlayerPrefs.SetInt("mute", mute == true ? 1 : 0);
        PlayerPrefs.Save();
        GetComponent<AudioSource>().mute = mute;
    }
}
