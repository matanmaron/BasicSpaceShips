using MaronByteStudio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] InputActionReference inputActionQuit;
    internal bool mute = false;
    public bool gameOn = true;

    private void OnEnable()
    {
        inputActionQuit.action.started += ResetKey;
    }

    private void OnDisable()
    {
        inputActionQuit.action.started -= ResetKey;
    }

    private void ResetKey(InputAction.CallbackContext context)
    {
        Reset();
    }

    public void onMute()
    {
        mute = !mute;
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }
}