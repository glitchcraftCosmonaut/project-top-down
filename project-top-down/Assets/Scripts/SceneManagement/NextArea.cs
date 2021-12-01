using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextArea : MonoBehaviour
{
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        SceneManager.LoadScene("Credits");
        Destroy(Player.instance.gameObject);
        Destroy(CameraController.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(EventSystem.instance.gameObject);
        isGameOver = false;
        GameManager.instance.isPaused = false;
        GameManager.instance.isGameOver = false;
    }
}
