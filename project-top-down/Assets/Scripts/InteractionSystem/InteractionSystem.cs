using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractionSystem : MonoBehaviour
{ 
    public Button MyButton{get; private set;}

    // private void Start() 
    // {
    //     MyButton = GetComponent<Button>();
    //     MyButton.onClick.AddListener(OnClick);    
    // }
    
    private void Reset() 
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OpenInteractableIcon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().CloseInteractableIcon();
        }
    }
}


