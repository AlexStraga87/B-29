using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonAction : MonoBehaviour
{

    public void LoadScene(string nameScene)
    {
        StartCoroutine(LoadSceneCoroutine(nameScene));
    }

    public void ResetSaveFile()
    {
        SaveSystem.Instance.ResetSaveData();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void PassedButtonClick()
    {
        PlayerData data = SaveSystem.Instance.GetPlayerData();
        if (data.CurrentLevel == 10)
        {
            ResetSaveFile();
            LoadScene("FinalScene");
        }
        else
        {
            LoadScene("LevelScene");
        }
    }

    private IEnumerator LoadSceneCoroutine(string nameScene)
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        SceneManager.LoadScene(nameScene);
    }


}
