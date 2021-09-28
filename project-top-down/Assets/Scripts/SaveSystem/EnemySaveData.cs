using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class EnemySaveData
{
    public EnemyData MyEnemyData {get; set;}
    // public ItemData MyItemdData {get; set;}
    // public CollectibleItemData MyCollectibleItemData {get; set;}

}

[System.Serializable]
public class EnemyData
{
    // public int MyLevel {get; set;}

    public float MyX {get; set;}
    public float MyY {get; set;}
    public bool MyDead {get; set;}
    

    public EnemyData(Vector2 position, bool isDead)
    {
        // this.MyLevel = level;
        this.MyX = position.x;
        this.MyY = position.y;
        this.MyDead = isDead;
    }
}