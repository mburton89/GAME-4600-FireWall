using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitCreditsButton;
    [SerializeField] private Button _lvl1Button;
    [SerializeField] private Button _lvl2Button;
    [SerializeField] private Button _backButton;

    [SerializeField] private GameObject _levelSelectMenu;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _creditsMenu;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(HandleStartPressed);
        _exitButton.onClick.AddListener(HandleExitPressed);
        _creditsButton.onClick.AddListener(HandleCreditsPressed);
        _exitCreditsButton.onClick.AddListener(CloseCreditsMenu);
        _lvl1Button.onClick.AddListener(HandleLevel1Pressed);
        _lvl2Button.onClick.AddListener(HandleLevel2Pressed);
        _backButton.onClick.AddListener(HandleBackPressed);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(HandleStartPressed);
        _exitButton.onClick.RemoveListener(HandleExitPressed);
        _creditsButton.onClick.RemoveListener(HandleCreditsPressed);
        _exitCreditsButton.onClick.RemoveListener(CloseCreditsMenu);
        _lvl1Button.onClick.RemoveListener(HandleLevel1Pressed);
        _lvl2Button.onClick.RemoveListener(HandleLevel2Pressed);
        _backButton.onClick.RemoveListener(HandleBackPressed);
    }

    void Update()
    {
        if (_creditsMenu.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditsMenu();
        }
    }

    void HandleStartPressed()
    {
        _levelSelectMenu.SetActive(true);
    }

    void HandleExitPressed()
    {
        Application.Quit();
    }

    void HandleCreditsPressed()
    {
        _creditsMenu.gameObject.SetActive(true);
    }

    void CloseCreditsMenu()
    {
        _creditsMenu.gameObject.SetActive(false);
    }

    void HandleLevel1Pressed()
    {
        SceneManager.LoadScene(1);
    }

    void HandleLevel2Pressed()
    {
        SceneManager.LoadScene(2);
    }

    void HandleBackPressed()
    {
        _levelSelectMenu.SetActive(false);
    }
}
