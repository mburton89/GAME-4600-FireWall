using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public static SceneMover Instance;

    void Awake()
    {
        Instance = this;
    }

    private IEnumerator GoToSceneCo(int index)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }

    public void GoToHome()
    {
        StartCoroutine(GoToSceneCo(0));
    }

    public void GoToLevel1()
    {
        StartCoroutine(GoToSceneCo(1));
    }

    public void GoToLevel2()
    {
        StartCoroutine(GoToSceneCo(2));
    }

    public void RestartScene()
    {
        StartCoroutine(GoToSceneCo(SceneManager.GetActiveScene().buildIndex));
    }
}
