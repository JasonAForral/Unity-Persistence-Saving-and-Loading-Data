using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

// this is the condensed version of the same thing

public class GameController2 : MonoBehaviour
{

    public static GameController2 gameController;
    private string saveFileName = "/playerData2.dat";

    protected PlayerData playerData;

    void Awake ()
    {
        if (null == gameController)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }
        else if (this != gameController)
        {
            Destroy(gameObject);
        }

    }

    void Start ()
    {
        //GameController2.gameController.Save();
        GameController2.gameController.Load();
    }

    public void Save ()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + saveFileName);
        PlayerData playerData = this.playerData;
        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public void Load ()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + saveFileName, FileMode.Open);
            PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            this.playerData = playerData;

            
        }
        else
        {
            Debug.Log("404 - File not found: "+saveFileName);
        }

    }
}

// [System.Serializable]
//[Serializable]
//public struct PlayerData
//{
//    public float health;
//    public float experience;
//    public float wisdom;

//    public PlayerData (float health, float experience, float wisdom)
//    {
//        this.health = health;
//        this.experience = experience;
//        this.wisdom = wisdom;
//    }
//}