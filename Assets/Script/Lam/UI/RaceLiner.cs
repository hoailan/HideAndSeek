using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RaceLiner : MonoBehaviour
{
    public bool isStart;
    private GameObject level, nextLevel, Pos, fill ;

    private float start,end, distance;
    private RectTransform rectTransform;


    void Start()
    {
        changeLevel();
        isStart = false;
        Gamemanager.OnStartGame += handleStartGame;
        
        Pos = GameObject.Find("Position");
        rectTransform = Pos.GetComponent<RectTransform>();
    }


    void Update()
    {
        changeFillAmount();
        if (isStart)
        {
            changePos();
        }
    }

    private void changeLevel()
    {
        // change level -> next
        int levelIndex = LevelManager.Instance.getLevel();

        level = GameObject.Find("txtLevel");
        level.GetComponent<TextMeshProUGUI>().text = "" + levelIndex;
        levelIndex += 1;
        nextLevel = GameObject.Find("txtLevelNext");
        nextLevel.GetComponent<TextMeshProUGUI>().text = "" + levelIndex;
    }

    private void changeFillAmount()
    {
        fill = GameObject.Find("FillAmount");
        fill.GetComponent<Image>().fillAmount = Player.Instance.percentage;
    }

    private void changePos()
    {
        // player percent
        // start/end position
        start = -333f;
        end = 340f;
        distance = Mathf.Abs(end - start);
        float x = start + distance * Player.Instance.percentage;

        // update
        rectTransform.anchoredPosition = new Vector2(x,rectTransform.anchoredPosition.y);
    }

    private void handleStartGame()
    {
        isStart = true;
    }
}
