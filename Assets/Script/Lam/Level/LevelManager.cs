using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get => instance; }

    public List<GameObject> levelPrefab;
    public static int level;
    public bool clear = false;

    private void Awake()
    {
        Player.OnPlayerFinish += saveLevel;
        instance = this;
    }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        loadLevel();

        if(level >= levelPrefab.Count)
        {
            level = 0;
        }
        Instantiate(levelPrefab[level]);
        Debug.Log(level);
    }

    private void Update()
    {
        // temp clear level
        if (clear)
        {
            clearLevel();
        }
    }

    public void loadLevel()
    {
        level = PlayerPrefs.GetInt("level");
    }

    public void saveLevel()
    {
        //next to temp
        Gamemanager.Instance.LoadTempScene(); // change LoadTempScene at Game Manager

        PlayerPrefs.SetInt("level",level + 1);
    }

    public void clearLevel()
    {
        clear = false;
        level = 0;
    }

    public int getLevel()
    {
        return level;
    }
}
