using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelloader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public Button playButton;
    public Button modeLevelButton;
    public Button levelSelectButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        modeLevelButton.onClick.AddListener(LoadModeLevelScene);
        levelSelectButton.onClick.AddListener(LoadLevelSelectScene);
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

    public void PlayGame()
    {

        modeLevelButton.gameObject.SetActive(false);
        levelSelectButton.gameObject.SetActive(false);

    }
}
