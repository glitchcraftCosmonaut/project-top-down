using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDestroyer : MonoBehaviour
{
    private void Awake() 
    {
        if(CameraController.instance != null)
        {
            Destroy(CameraController.instance.gameObject);
        }
        if(GameManager.instance.gameObject != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
        if(EventSystem.instance.gameObject != null)
        {
            Destroy(EventSystem.instance.gameObject);
        }
        
        
    }
}
