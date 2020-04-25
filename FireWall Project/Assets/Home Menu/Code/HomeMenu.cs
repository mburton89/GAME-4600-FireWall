using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitCreditsButton;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _creditsMenu;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(HandleStartPressed);
        _exitButton.onClick.AddListener(HandleExitPressed);
        _creditsButton.onClick.AddListener(HandleCreditsPressed);
        _exitCreditsButton.onClick.AddListener(CloseCreditsMenu);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(HandleStartPressed);
        _exitButton.onClick.RemoveListener(HandleExitPressed);
        _creditsButton.onClick.RemoveListener(HandleCreditsPressed);
        _exitCreditsButton.onClick.RemoveListener(CloseCreditsMenu);
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
        _loadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
