using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerSaveData
{
    public PlayerData MyPlayerData {get; set;}
    // public ItemData MyItemdData {get; set;}
    // public CollectibleItemData MyCollectibleItemData {get; set;}

}

[System.Serializable]
public class PlayerData
{
    // public int MyLevel {get; set;}

    public float MyX {get; set;}
    public float MyY {get; set;}
    private int currentSceneIndex;
    

    public PlayerData(Vector2 position)
    {
        // this.MyLevel = level;
        this.MyX = position.x;
        this.MyY = position.y;
    }
}

// [System.Serializable]
// public class ItemData
// {
//     public List<Item> Items { get; set; } = new List<Item>();
    
//     public ItemDatabase database;
//     public bool hasLight;
//     public ItemData(List<Item> items)
//     {
//         database = Inventory.instance.database;
//         this.Items = new List<Item>(items);
//         foreach(Item item in items)
//         {
//             Inventory.instance.AddItem(item.itemName);
//         }
//     }
// }

// [System.Serializable]
// public class CollectibleItemData
// {
//     public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();

//     public CollectibleItemData(HashSet<string> collectedItem)
//     {
//         this.CollectedItems = new HashSet<string>(collectedItem);
//     }
// }

