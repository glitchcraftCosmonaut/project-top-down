using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private int sceneToContinue;
    public Image loadButton;
    private void Start() 
    {
        if(SaveLoad.SaveExists("SavedScene") && SaveLoad.SaveExists("PlayerData") && SaveLoad.SaveExists("CameraData"))
        {
            loadButton.gameObject.SetActive(true);
        }
        else
        {
            loadButton.gameObject.SetActive(false);
        }
    }
    public void NewGame()
    {
        if(SaveLoad.SaveExists("SavedScene") && SaveLoad.SaveExists("PlayerData") && SaveLoad.SaveExists("CameraData"))
        {
            SaveLoad.SeriouslyDeleteAllSaveFiles();
        }

        SceneManager.LoadScene("Scene 2");
        Time.timeScale = 1.0f;
    }
    public void LoadGame()
    {
        // SaveLoad.Load<PlayerSaveData>("PlayerData");
        // SaveLoad.Load<CameraSaveData>("CameraData");
        sceneToContinue = SaveLoad.Load<int>("SavedScene");
        if(sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);
            Time.timeScale = 1.0f;
        }
        else
        {
            return;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
