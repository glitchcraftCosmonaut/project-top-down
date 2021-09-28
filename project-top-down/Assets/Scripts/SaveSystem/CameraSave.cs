using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSave : MonoBehaviour
{
    public CameraSaveData data {get; set;} = new CameraSaveData();
    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        
        GameEvents.LoadInitiated += Load;
        Load();
    }

    private void SaveCamera(CameraSaveData data)
    {
        data.MyCameraData = new CameraData(CameraController.MyInstance.transform.position);
    }
    private void LoadCamera(CameraSaveData data)
    {
        CameraController.MyInstance.transform.position = new Vector2(data.MyCameraData.MyX, data.MyCameraData.MyY);
    }
    
    void Save()
    {
        SaveCamera(data);
        SaveLoad.Save(data, "CameraData");
    }
    public void Load()
    {
        if (SaveLoad.SaveExists("CameraData"))
        {
            data = SaveLoad.Load<CameraSaveData>("CameraData");
            LoadCamera(data);
        }
        
    }
}
