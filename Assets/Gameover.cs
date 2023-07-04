using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        HideGameOver();
    }

    public void ShowGameOver()
    {
        if (!gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void HideGameOver()
    {
        if (gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void ReplayGame()
    {

        Debug.Log("Replay game");
    }


}
