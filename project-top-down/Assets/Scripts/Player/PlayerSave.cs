using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour
{ 
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    private int currentSceneIndex;
    
    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        
        GameEvents.LoadInitiated += Load;
        Load();
    }
    private void SavePlayer(PlayerSaveData data)
    {
        data.MyPlayerData = new PlayerData(Player.MyInstance.transform.position);
    }
    private void LoadPlayer(PlayerSaveData data)
    {
        Player.MyInstance.transform.position = new Vector2(data.MyPlayerData.MyX, data.MyPlayerData.MyY);
    }
    
    void Save()
    {
        SavePlayer(data);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SaveLoad.Save(currentSceneIndex,"SavedScene");
        SaveLoad.Save(data, "PlayerData");
    }
    public void Load()
    {
        if (SaveLoad.SaveExists("PlayerData"))
        {
            data = SaveLoad.Load<PlayerSaveData>("PlayerData");
            currentSceneIndex = SaveLoad.Load<int>("SavedScene");
            LoadPlayer(data);
        }
        
    }
}
