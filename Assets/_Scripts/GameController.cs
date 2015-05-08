using UnityEngine;
using System.Collections;
using System;

// need this to create binary files that can't be opened easily as plain text
// much better than playerprefs which is in plain text
using System.Runtime.Serialization.Formatters.Binary; 

// stands for input output
using System.IO;
// example: File.Open()

// note this supposedly works on everything EXCEPT web.

public class GameController : MonoBehaviour
{

    public static GameController gameController;

    public PlayerData playerData;
    //public float health;
    //public float experience;
    //public float wisdom;

    void Awake ()
    {
        // make singleton

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

    // Use this for initialization
    void Start ()
    {
        // examples of static singleton usage on another script
        GameController.gameController.Save();
        //GameController.gameController.Load();

    }

    

    // Update is called once per frame
    void Update ()
    {

    }

    public void Save ()
    {
        // make a thingy
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        

        // ready the file
        FileStream fileStream = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        // print(Application.persistentDataPath); // to see where it goes
        
        // create a compact class that only contains exactly what we want to save
        //PlayerData playerData = new PlayerData(health, experience, wisdom);
        PlayerData playerData = this.playerData;

        // tell the thingy to serialze the data into the file stream
        binaryFormatter.Serialize(fileStream, playerData);

        // close the file
        fileStream.Close();
    }

    public void Load ()
    {
        // first check if the file exists
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            // ready the thingy
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // ready the file
            FileStream fileStream = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            // FileMode.Create seems to overwrite
            // FileMode.Open throws an error if doesn't exist
            // FileMode.CreateNew throws an error if exists
            // Filemode.OpenOrCreate says it appends but seems to overwrite also

            // https://msdn.microsoft.com/en-us/library/system.io.filemode(v=vs.110).aspx

            // deserialize the data to a PlayerData object
            // don't forget to cast it from a general object
            PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);

            //health = playerData.health;
            //experience = playerData.experience;
            //wisdom = playerData.wisdom;

            this.playerData = playerData;

        }
        else
        {
            Debug.Log("404 - File not found.");

            // do nothing?

            // skip and create new?
        }
    }
}

// serializing makes conversion to binary easier/quicker

// can be structs, class, and serializable types
// Dictionaries can be serialized by making a child of ScriptableObject
// I don't know if others work

// [System.Serializable]
[Serializable]
public struct PlayerData 
{
    public float health;
    public float experience;
    public float wisdom;

    public PlayerData (float health, float experience, float wisdom)
    {
        this.health = health;
        this.experience = experience;
        this.wisdom = wisdom;
    }
    //public PlayerData ()
    //{
    //    this.health = 0;
    //    this.experience = 0;
    //    this.wisdom = 0;
    //}
}
