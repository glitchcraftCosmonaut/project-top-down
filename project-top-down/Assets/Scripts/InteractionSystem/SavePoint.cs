using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePoint : InteractionSystem
{
    public Image gameMenuImage;
    public override void Interact()
    {
        //Debug.Log("PAUSE");
        gameMenuImage.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
    }
    public void Resume()
    {
        //Debug.Log("RESUME");
        gameMenuImage.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }
    public void Save()
    {
        GameEvents.OnSaveInitiated();
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(Player.instance.gameObject);
        Destroy(CameraController.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(EventSystem.instance.gameObject);
        GameManager.instance.isPaused = false;
    }
}
