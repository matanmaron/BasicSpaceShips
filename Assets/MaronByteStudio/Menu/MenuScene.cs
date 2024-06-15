using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MaronByteStudio.Menu
{
    public class MenuScene : MonoBehaviour
    {
        [SerializeField] Button StartButton;
        [SerializeField] Button AboutButton;
        [SerializeField] Button QuitButton;
        [SerializeField] GameObject MenuPanel;
        [SerializeField] GameObject AboutPanel;
        [SerializeField] string StartScene;
        [SerializeField] GameObject FirstAboutMenuButton;

        private void Start()
        {
            SetPanel(Panels.Menu);
            StartButton.onClick.AddListener(OnStartGame);
            AboutButton.onClick.AddListener(OnOpenAbout);
            QuitButton.onClick.AddListener(OnQuitGame);
            HideQuitIfNeeded();
        }

        private void SetPanel(Panels current)
        {
            MenuPanel.SetActive(false);
            AboutPanel.SetActive(false);
            switch (current)
            {
                case
                Panels.Menu:
                    MenuPanel.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(StartButton.gameObject);
                    break;
                case
                Panels.About:
                    AboutPanel.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(FirstAboutMenuButton);
                    break;
                default: Debug.LogError("Panel not supported"); break;
            }
        }

        private void HideQuitIfNeeded()
        {
            if (Application.platform == RuntimePlatform.Android ||
             Application.platform == RuntimePlatform.IPhonePlayer ||
             Application.platform == RuntimePlatform.WebGLPlayer)
            {
                QuitButton.gameObject.SetActive(false);
            }
        }

        private void OnQuitGame()
        {
            Application.Quit();
        }

        private void OnOpenAbout()
        {
            SetPanel(Panels.About);
        }

        private void OnStartGame()
        {
            SceneManager.LoadScene(StartScene);
        }

        internal void OnBackButton()
        {
            SetPanel(Panels.Menu);
        }

        private void OnDestroy()
        {
            StartButton.onClick.RemoveAllListeners();
            AboutButton.onClick.RemoveAllListeners();
            QuitButton.onClick.RemoveAllListeners();
        }

        private enum Panels
        {
            Menu,
            About
        }
    }
}