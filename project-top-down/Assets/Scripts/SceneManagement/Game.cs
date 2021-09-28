using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private PlayerSave playerSave;
    public Image gameMenuImage;
    public Toggle BGMToggle;
    private AudioSource BGMSource;

    private void Start() 
    {
        gameMenuImage.gameObject.SetActive(false);
        // restartMenuImage.gameObject.SetActive(false);

        BGMSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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

        BGMManager();
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
    private void BGMManager()
    {
        if(PlayerPrefs.GetInt("BGM") == 1)
        {
            BGMToggle.isOn = true;
            BGMSource.enabled = true;//PLAY THE SOURCE
        }
        else if(PlayerPrefs.GetInt("BGM") == 0)
        {
            BGMToggle.isOn = false;
            BGMSource.enabled = false;
        }
    }
    public void BGMToggleButton()
    {
        if(BGMToggle.isOn)
        {
            //OPEN THE BGM
            PlayerPrefs.SetInt("BGM", 1);//1 means open and 0 means close.(CUSTOMIZED)
            //Debug.Log(PlayerPrefs.GetInt("BGM"));
        }
        else
        {
            //CLOSE THE BGM
            PlayerPrefs.SetInt("BGM", 0);
            //Debug.Log(PlayerPrefs.GetInt("BGM"));
        }
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(Player.instance.gameObject);
        Destroy(CameraController.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(EventSystem.instance.gameObject);
        GameManager.instance.isPaused = false;
    }

    public void DeleteAllProgress()
    {
        SaveLoad.SeriouslyDeleteAllSaveFiles();
    }
}
