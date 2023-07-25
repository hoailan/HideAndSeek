using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class BtnBack : BaseButton
{
    protected override void onClick()
    {
        StartCoroutine(LoadSceneByName("Menu1"));
    }

    public IEnumerator LoadSceneByName(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
