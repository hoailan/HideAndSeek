using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Backbutton : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(GoToMenu);
    }

    public void GoToMenu()
    {
        StartCoroutine(LoadLevel("Menu"));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
