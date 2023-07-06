using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class Gamemanager : MonoBehaviour
{
    public CountdownTimer countdownTimer;
    private bool isGameOver = false;
    private bool hasStarted = false;
    public Gameover gameOver;
    public Animator transition;
    public float transitionTime = 1f;

    public Button playButton;
    public Button modeLevelButton;
    public Button levelSelectButton;
    public Button replayButton;

    public List<Button> buttonsToHide;
    public List<Image> imagesToHide;

    private float gameOverDelay = 1f;
    private float gameOverTimer = 0f;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        modeLevelButton.onClick.AddListener(LoadModeLevelScene);
        levelSelectButton.onClick.AddListener(LoadLevelSelectScene);
        replayButton.onClick.AddListener(ReplayGame);

        countdownTimer.OnTimerFinished += HandleTimerFinished;
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (gameOverTimer >= gameOverDelay)
            {
                gameOver.ShowGameOver();
            }
            else
            {
                gameOverTimer += Time.deltaTime;
            }
        }
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
        if (isGameOver)
        {
            RestartGame();
        }
        else if (!hasStarted)
        {
            HideButtons();
            HideImages();

            countdownTimer.StartTimer();
            hasStarted = true;
        }
    }

    public void ReplayGame()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        ShowButtons();
        ShowImages();

        gameOver.HideGameOver();
        countdownTimer.ResetTimer();

        hasStarted = false;
        isGameOver = false;
        gameOverTimer = 0f;
    }

    private void HandleTimerFinished()
    {
        if (hasStarted && !isGameOver)
        {
            isGameOver = true;
        }
    }

    public void GameOver()
    {
        gameOver.ShowGameOver();
        countdownTimer.StopTimer();
    }

    private void HideButtons()
    {
        foreach (var button in buttonsToHide)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void ShowButtons()
    {
        foreach (var button in buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void HideImages()
    {
        foreach (var image in imagesToHide)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void ShowImages()
    {
        foreach (var image in imagesToHide)
        {
            image.gameObject.SetActive(true);
        }
    }
}
