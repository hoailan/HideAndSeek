using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Gamemanager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public Button playButton;
    public Button modeLevelButton;
    public Button levelSelectButton;
    public Button replayButton;

    public List<Button> buttonsToHide;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        modeLevelButton.onClick.AddListener(LoadModeLevelScene);
        levelSelectButton.onClick.AddListener(LoadLevelSelectScene);
        replayButton.onClick.AddListener(ReplayGame);
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
        // Ẩn các button cần ẩn
        foreach (var button in buttonsToHide)
        {
            button.gameObject.SetActive(false);
        }

        // Thực hiện các hành động khác để bắt đầu game
        // ...
    }

    public void ReplayGame()
    {
        // Hiển thị lại các button đã ẩn
        foreach (var button in buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }

        // Thực hiện các hành động khác để chơi lại game
        // ...
    }
}
