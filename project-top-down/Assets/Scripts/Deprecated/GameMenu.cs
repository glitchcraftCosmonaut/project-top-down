using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameMenu : MonoBehaviour
{
    public Image gameMenuImage;
    public Image restartMenuImage;
    // public Image saveMenuImage;
    // private bool isGameEnded = false;
    private int sceneToContinue;

    // public Toggle BGMToggle;
    private AudioSource BGMSource;

    private Player player;

    //EP48 
    public Text hintText;
    public Bat batGameObject;//THIS gameobject will be instantiate when we have already kill the enemy but need to load the game later
    public Witch witchGameObject;

    private void Start()
    {
        gameMenuImage.gameObject.SetActive(false);
        // restartMenuImage.gameObject.SetActive(false);

        BGMSource = GetComponent<AudioSource>();

        player = FindObjectOfType<Player>();//GET ACCESS OF THE PLAYERMOVEMENT component
        // hintText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.instance.isPaused)//If we press the escape AND the game is PAUSE
            {
                //We want to resume the game ASAP
                Resume();
            }
            else
            {
                //We want to pause the game
                Pause();
            }
        }
        // if(player.isDead)
        // {
        //     GameOver();
        // }

        // BGMManager();
    }

    public void Resume()
    {
        //Debug.Log("RESUME");
        gameMenuImage.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }

    private void Pause()
    {
        //Debug.Log("PAUSE");
        gameMenuImage.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
    }
    public void Restart()
    {
        // if(File.Exists(Application.dataPath + "/savedata.Text"))
        // {
        //     // Time.timeScale = 1.0f;
        //     GameManager.instance.isPaused = false;
            
        //     LoadByJson();
        // }
        // else
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //     GameManager.instance.isPaused = false;
        // }
        restartMenuImage.gameObject.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // player.isDead = false;
        Time.timeScale = 1.0f;
        GameManager.instance.isGameOver = false;
    }
    public void GameOver()
    {
        restartMenuImage.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.instance.isGameOver = true;
    }

    //MARKER THIS FUNCTION WILL BE TRIGGER ON THE TOGGLE EVENT LISTENER
    // public void BGMToggleButton()
    // {
    //     if(BGMToggle.isOn)
    //     {
    //         //OPEN THE BGM
    //         PlayerPrefs.SetInt("BGM", 1);//1 means open and 0 means close.(CUSTOMIZED)
    //         //Debug.Log(PlayerPrefs.GetInt("BGM"));
    //     }
    //     else
    //     {
    //         //CLOSE THE BGM
    //         PlayerPrefs.SetInt("BGM", 0);
    //         //Debug.Log(PlayerPrefs.GetInt("BGM"));
    //     }
    // }

    // private void BGMManager()
    // {
    //     if(PlayerPrefs.GetInt("BGM") == 1)
    //     {
    //         BGMToggle.isOn = true;
    //         BGMSource.enabled = true;//PLAY THE SOURCE
    //     }
    //     else if(PlayerPrefs.GetInt("BGM") == 0)
    //     {
    //         BGMToggle.isOn = false;
    //         BGMSource.enabled = false;
    //     }
    // }

    public void NewGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    //MARKER THIS TWO FUNCTIONS WILL BE TRIGGERED ON THE BUTTON EVENT LISTENER
    public void SaveButton()
    {
        //SaveByPlayerPrefs();
        // SaveBySerialization();
        SaveByJson();
    }

    public void LoadButton()
    {
        
        //LoadByPlayerPrefs();
        // LoadByDeSerialization();
        // if(GameManager.instance.isPaused)
        // { 
        //     LoadByJson();
        //     Resume();
        // }
        // if(GameManager.instance.isGameOver)
        // {
        //     LoadByJson();
        // }
        LoadByJson();
        Resume();
        
    }
    public void Quit()
    {
        Application.Quit();
    }

    //MARKER SAVE AND LOAD BY SERIALIZATION
    private Save createSaveGameObject()
    {
        Save save = new Save();

        // save.coinsNum = GameManager.instance.coins;
        // save.diamondsNum = GameManager.instance.diamonds;

        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;
        save.saveCurrenSceneIndex = player.currentSceneIndex;

        //MARKER Enemy position
        foreach(Bat bat in GameManager.instance.bats)
        {
            save.batIsDead.Add(bat.isDead);
            save.enemyPositionX.Add(bat.batPositionX);
            save.enemyPositionY.Add(bat.batPositionY);
        }

        foreach(Witch witch in GameManager.instance.witchs)
        {
            save.witchIsDead.Add(witch.isDead);
            save.enemyPositionX.Add(witch.witchPositionX);
            save.enemyPositionY.Add(witch.witchPositionY);
        }

        return save;
    }
    public void SaveByJson()
    {
        Save save = createSaveGameObject();

        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/savedata.Text");
        sw.Write(JsonString);

        sw.Close();

        
        Debug.Log("Saved");

        
    }
    public void LoadByJson()
    {
        if(File.Exists(Application.dataPath + "/savedata.Text"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/savedata.Text");
            string JsonString = sr.ReadToEnd();
            sr.Close();

            Save save = JsonUtility.FromJson<Save>(JsonString);

            // GameManager.instance.coins = save.coinsNum;

            // GameManager.instance.diamonds = save.diamondsNum;

            player.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);
            // player.currentSceneIndex = save.saveCurrenSceneIndex;
            // if(player.currentSceneIndex != 0)
            // {
            //     SceneManager.LoadScene(player.currentSceneIndex);
            // }
            // else
            // {
            //     return;
            // }

            //MARKER Enemy position
            for(int i = 0; i < save.batIsDead.Count; i++)
            {
                if (GameManager.instance.bats[i] == null)
                {
                    if(!save.batIsDead[i])//jika musuh ini mati, tapi hidup saat sebelum di save
                    {
                        float batPosX = save.enemyPositionX[i];
                        float batPosY = save.enemyPositionY[i];
                        Bat newBat = Instantiate(batGameObject, new Vector2(batPosX, batPosY), Quaternion.identity);
                        GameManager.instance.bats[i] = newBat;//mengisi posisi dimana elemen musuh sama dengan null
                    }
                }
                else
                {
                    float batPosX = save.enemyPositionX[i];
                    float batPosY = save.enemyPositionY[i];
                    GameManager.instance.bats[i].transform.position = new Vector2(batPosX, batPosY); 
                }
            }
            for(int i = 0; i < save.witchIsDead.Count; i++)
            {
                if (GameManager.instance.witchs[i] == null)
                {
                    if(!save.witchIsDead[i])//jika musuh ini mati, tapi hidup saat sebelum di save
                    {
                        float witchPosX = save.enemyPositionX[i];
                        float witchPosY = save.enemyPositionY[i];
                        Witch newWitch = Instantiate(witchGameObject, new Vector2(witchPosX, witchPosY), Quaternion.identity);
                        GameManager.instance.witchs[i] = newWitch;//mengisi posisi dimana elemen musuh sama dengan null
                    }
                }
                else
                {
                    float witchPosX = save.enemyPositionX[i];
                    float witchPosY = save.enemyPositionY[i];
                    GameManager.instance.witchs[i].transform.position = new Vector2(witchPosX, witchPosY);
                }
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //MARKER THIS FUNCTION WILL BE TRIGGERED WHEN YOU PRESS THE SAVE OR LOAD BUTTON
    IEnumerator DisplayHintCo(string _message)
    {
        Debug.Log("TTTTT");
        hintText.gameObject.SetActive(true);
        hintText.text = _message;
        yield return new WaitForSeconds(2);
        hintText.gameObject.SetActive(false);
    }

}
