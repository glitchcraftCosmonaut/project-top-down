using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePoint : InteractionSystem
{
    public Image gameMenuImage;
    public Image pauseMenuImage;
    public bool onSavePoint = false;
    public string sceneName;
    public Game game;
    public static SavePoint instance;
    public static SavePoint MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SavePoint>();
            }
            return instance;
        }
    }

    private void Start() 
    {
        game = FindObjectOfType<Game>();
    }

    // private void Update()
    // {
        
    //     if(Input.GetKeyDown(KeyCode.Escape) && onSavePoint == true)
    //     {
    //         if(GameManager.instance.isPaused)
    //         {
    //             Resume();
    //             onSavePoint = false;
    //             // game.gameMenuImage.gameObject.SetActive(false);
    //             // game.Resume();
    //         }
    //         else
    //         {
    //             gameMenuImage.gameObject.SetActive(true);
    //             Time.timeScale = 0.0f;
    //             GameManager.instance.isPaused = true;
    //             onSavePoint = true;
    //         }
    //     }
    // }       
    public override void Interact()
    {
        // if(GameManager.instance.isPaused)//If we press the escape AND the game is PAUSE
        //     {
        //         //We want to resume the game ASAP
        //         Resume();
        //     }
        //     else
        //     {
        //         //We want to pause the game
        //         //Debug.Log("PAUSE");
        //         Pause();
        //     }
        GameEvents.OnSaveInitiated();
        Player.MyInstance.GetComponentInChildren<HealthBar>().hp = 1000;
        
    }
    // public void Resume()
    // {
    //     //Debug.Log("RESUME");
    //     gameMenuImage.gameObject.SetActive(false);
    //     Time.timeScale = 1.0f;
    //     onSavePoint = false;
    //     GameManager.instance.isPaused = false;
    // }
    // public void Pause()
    // {
    //     gameMenuImage.gameObject.SetActive(true);
    //     Time.timeScale = 0.0f;
    //     onSavePoint = true;
    //     GameManager.instance.isPaused = true;
    // }
    // public void Save()
    // {
    //     GameEvents.OnSaveInitiated();
    // }
    // public void Quit()
    // {
    //     SceneManager.LoadScene("MainMenu");
    //     Destroy(Player.instance.gameObject);
    //     Destroy(CameraController.instance.gameObject);
    //     Destroy(GameManager.instance.gameObject);
    //     Destroy(EventSystem.instance.gameObject);
    //     FindObjectOfType<SceneFader>().FadeTo(sceneName);
    //     GameManager.instance.isPaused = false;
    // }
}
