using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    // public GameObject door;
    public string entrancePassword;
    private void Start() 
    {
        if(Player.instance.scenePassword == entrancePassword)
        {
            Player.instance.transform.position = transform.position;
            Debug.Log("Enter!");
        }
        else
        {
            Debug.LogWarning("Wrong Password!");
        }
    }
}
