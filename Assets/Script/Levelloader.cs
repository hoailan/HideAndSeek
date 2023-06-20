using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelloader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public Button levelButton;
    public Button modeLevelButton;
    public Button levelSelectButton;

    private void Start()
    {
        levelButton.onClick.AddListener(LoadGameplayScene);
        modeLevelButton.onClick.AddListener(LoadModeLevelScene);
        levelSelectButton.onClick.AddListener(LoadLevelSelectScene);
    }

    public void LoadGameplayScene()
    {
        StartCoroutine(LoadSceneByName("Gameplay"));
    }

    public void LoadModeLevelScene()
    {
        StartCoroutine(LoadSceneByName("Modelevel"));
    }

    public void LoadLevelSelectScene()
    {
        StartCoroutine(LoadSceneByName("LevelSelect"));
    }

    IEnumerator LoadSceneByName(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
