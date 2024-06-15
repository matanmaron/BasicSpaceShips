using UnityEngine;
using UnityEngine.InputSystem;

public class AudioButton : MonoBehaviour
{
    [SerializeField] GameObject muteImg = null;
    [SerializeField] InputActionReference inputActionMute;
    bool mute = false;

    void Start()
    {
        if (GameManager.Instance.mute)
        {
            OnMute();
        }
    }

    private void OnEnable()
    {
        inputActionMute.action.started += OnMute;
    }

    private void OnMute(InputAction.CallbackContext context)
    {
        OnMute();
    }

    private void OnDisable()
    {
        inputActionMute.action.started -= OnMute;
    }

    public void OnMute()
    {
        mute = !mute;
        muteImg.SetActive(mute);
    }
}