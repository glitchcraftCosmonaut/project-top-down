using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class CameraSaveData
{
    public CameraData MyCameraData {get; set;}
    // public ItemData MyItemdData {get; set;}
    // public CollectibleItemData MyCollectibleItemData {get; set;}

}

[System.Serializable]
public class CameraData
{
    // public int MyLevel {get; set;}

    public float MyX {get; set;}
    public float MyY {get; set;}
    private int currentSceneIndex;
    

    public CameraData(Vector3 position)
    {
        // this.MyLevel = level;
        this.MyX = position.x;
        this.MyY = position.y;
    }
}
