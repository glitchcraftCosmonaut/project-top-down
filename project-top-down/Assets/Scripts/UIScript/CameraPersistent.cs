using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPersistent : MonoBehaviour
{
    private static CameraPersistent instance;
    private void Awake() 
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
