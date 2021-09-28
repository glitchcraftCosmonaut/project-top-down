using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    public EnemySaveData data {get; set;} = new EnemySaveData();
    public List<Enemy> enemies;
    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        
        GameEvents.LoadInitiated += Load;
        Load();
    }

    private void SaveEnemy(EnemySaveData data)
    {
        data.MyEnemyData= new EnemyData(Enemy.MyInstance.transform.position, Enemy.MyInstance.isDead);
    }
    private void LoadEnemy(EnemySaveData data)
    {
        Enemy.MyInstance.transform.position = new Vector2(data.MyEnemyData.MyX, data.MyEnemyData.MyY);
        Enemy.MyInstance.isDead = data.MyEnemyData.MyDead;
    }
    
    void Save()
    {
        SaveEnemy(data);
        SaveLoad.Save(data, "EnemyData");
    }
    public void Load()
    {
        if (SaveLoad.SaveExists("EnemyData"))
        {
            data = SaveLoad.Load<EnemySaveData>("EnemyData");
            LoadEnemy(data);
        }
        
    }
}
