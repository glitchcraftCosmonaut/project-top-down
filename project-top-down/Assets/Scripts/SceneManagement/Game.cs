using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private PlayerSave playerSave;
    private int sceneToContinue;
    public Image gameMenuImage;
    public Image restartMenuImage;
    // public Toggle BGMToggle;
    public string sceneName;
    public PlayerSaveData playerDatas;
    public CameraSaveData playerCams;
    private AudioSource BGMSource;
    public bool isGameOver;
    public SavePoint savePoint;
    public static Game instance;

    private void Start() 
    {
        savePoint = FindObjectOfType<SavePoint>();
        gameMenuImage.gameObject.SetActive(false);
        restartMenuImage.gameObject.SetActive(false);
        // restartMenuImage.gameObject.SetActive(false);

        BGMSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && savePoint.onSavePoint == false)
            {
                if(GameManager.instance.isPaused)
                {
                    Resume();
                    
                }
                else
                {
                    Pause();
                }
            }
            if(GameManager.instance.isGameOver == true)
            {
                GameOver();
            }
        }
        

        // BGMManager();
    }

    public void Resume()
    {
        //Debug.Log("RESUME");
        gameMenuImage.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }

    private void Pause()
    {
        //Debug.Log("PAUSE");
        gameMenuImage.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
    }

    public void Save()
    {
        GameEvents.OnSaveInitiated();
    }
    public void Load()
    {
        GameEvents.OnLoadInitiated();
        
        // playerSave.Load();
        // inventory.Load();
        // collectibleItemSet.Load()
        Debug.Log("Clicked");
    }
    public void Restart()
    {
        
        if(SaveLoad.SaveExists("PlayerData") && SaveLoad.SaveExists("CameraData") && SaveLoad.SaveExists("SavedScene"))
        {
            SaveLoad.Load<PlayerSaveData>("PlayerData");
            SaveLoad.Load<CameraSaveData>("CameraData");
            SceneManager.LoadScene("Scene 1");
            restartMenuImage.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            GameManager.instance.isGameOver = false;
        }
        else
        {
            SceneManager.LoadScene("Scene 1");
            restartMenuImage.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            GameManager.instance.isGameOver = false;
        }
  
    }
    // private void BGMManager()
    // {
    //     if(PlayerPrefs.GetInt("BGM") == 1)
    //     {
    //         BGMToggle.isOn = true;
    //         BGMSource.enabled = true;//PLAY THE SOURCE
    //     }
    //     else if(PlayerPrefs.GetInt("BGM") == 0)
    //     {
    //         BGMToggle.isOn = false;
    //         BGMSource.enabled = false;
    //     }
    // }
    // public void BGMToggleButton()
    // {
    //     if(BGMToggle.isOn)
    //     {
    //         //OPEN THE BGM
    //         PlayerPrefs.SetInt("BGM", 1);//1 means open and 0 means close.(CUSTOMIZED)
    //         //Debug.Log(PlayerPrefs.GetInt("BGM"));
    //     }
    //     else
    //     {
    //         //CLOSE THE BGM
    //         PlayerPrefs.SetInt("BGM", 0);
    //         //Debug.Log(PlayerPrefs.GetInt("BGM"));
    //     }
    // }
        public void GameOver()
        {
            restartMenuImage.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            Destroy(CameraController.instance.gameObject);
            Destroy(GameManager.instance.gameObject);
            Destroy(EventSystem.instance.gameObject);
            BGMSource.Stop();
            isGameOver = true;
            GameManager.instance.isGameOver = true;
        }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        // FindObjectOfType<SceneFader>().FadeTo(sceneName);
        Destroy(Player.instance.gameObject);
        Destroy(CameraController.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(EventSystem.instance.gameObject);
        isGameOver = false;
        GameManager.instance.isPaused = false;
        GameManager.instance.isGameOver = false;
    }

    public void DeleteAllProgress()
    {
        SaveLoad.SeriouslyDeleteAllSaveFiles();
    }
}
