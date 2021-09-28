using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    //list item picked up
    public List<GameObject> items = new List<GameObject>();
    //flag indicates that inventory is open or not
    public bool isOpen;
    [Header("UI Item Section")]
    //inventory system window
    public GameObject ui_window;
    public Image[] item_images;
    [Header("UI Item Description")]
    public GameObject ui_description_window;
    public Image description_image;
    public Text description_text;
    public bool hasLighter = false;

    private static InventorySystem instance;

    public static InventorySystem MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventorySystem>();
            }
            return instance;
        }
    }
    public List<GameObject> MyItem
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }
    void ToggleInventory()
    {
        isOpen = !isOpen;
        ui_window.SetActive(isOpen);
    }
    //add item to item lists
    public void PickUp(GameObject item)
    {
        items.Add(item);
        Update_UI();
    }
    //refresh element in inventopry window
    private void Update_UI()
    {
        HideAll();
        //for each item in "items" list
        //show it in respective slot in the "item_images"
        for(int i=0;i<items.Count;i++)
        {
            item_images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            item_images[i].gameObject.SetActive(true);
        }
    }
    //hide all the items ui images
    private void HideAll() { foreach (var i in item_images) {i.gameObject.SetActive(false); } }
    
    public void ShowDescription(int id)
    {
        //set the image
        description_image.sprite = item_images[id].sprite;
        //set the text
        description_text.text = items[id].name;
        //show the elements
        description_image.gameObject.SetActive(true);
        description_text.gameObject.SetActive(true);
    }
    public void HideDescription()
    {
        description_image.gameObject.SetActive(false);
        description_text.gameObject.SetActive(false);
    }
    

    
}

