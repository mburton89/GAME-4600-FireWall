using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    [SerializeField] private KeyCode _pauseKey;
    [SerializeField] private GameObject _container;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    private bool _isPaused;

    [SerializeField] private UISpriteLooper[] _uiSpriteLoopers;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            if (_isPaused)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(ResumeGame);
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(ResumeGame);
        _restartButton.onClick.RemoveListener(RestartGame);
    }

    public void Open()
    {
        _isPaused = true;
        _container.SetActive(true);
        //Time.timeScale = 0; //TODO: Stop all rigid bodies and game world stuff

        foreach (UISpriteLooper uiSpriteLooper in _uiSpriteLoopers)
        {
            uiSpriteLooper.Play();
        }
    }

    public void Close()
    {
        _isPaused = false;
        _container.SetActive(false);
        //Time.timeScale = 1; //TODO: Re-enable all rigid bodies and game world stuff

        foreach (UISpriteLooper uiSpriteLooper in _uiSpriteLoopers)
        {
            uiSpriteLooper.Stop();
        }
    }

    void ResumeGame()
    {
        Close();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
