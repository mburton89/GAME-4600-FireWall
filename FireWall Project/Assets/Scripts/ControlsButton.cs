using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsButton : MonoBehaviour
{
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private Button _controlsButton;

    private void OnEnable()
    {
        _controlsButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        PauseMenu.Instance.Open();
    }
}
