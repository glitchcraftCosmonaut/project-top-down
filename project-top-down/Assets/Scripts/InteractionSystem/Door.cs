using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractionSystem
{
    public string sceneName;
    [SerializeField] private string newScenePassword;
    public override void Interact()
    {
        Player.instance.scenePassword = newScenePassword;
        // SceneManager.LoadScene(sceneName);
        FindObjectOfType<SceneFader>().FadeTo(sceneName);
    }
}
