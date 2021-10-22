using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveDataController
{
    private const string saveDataPath = "SavedDataGame.dat";


    public float volumeValue = -1;
    public int playerScore = -1;

    public SaveDataController() { }

    public SaveDataController(float volumeValue)
    {
        this.volumeValue = volumeValue;
    }

    public SaveDataController(int playerScore)
    {
        this.playerScore = playerScore;
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveDataPath);
        SaveData saveData = new SaveData();
        if (volumeValue!= -1)
        {
            saveData.volumeValue = volumeValue;
        }
        if (playerScore != -1)
        {
            saveData.playerScore = playerScore;
        }
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + saveDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveDataPath, FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            volumeValue = saveData.volumeValue;
            playerScore = saveData.playerScore;
        }
        else
            Debug.LogError("There is no save data!");
    }
}


[Serializable]
public class SaveData
{
    public float volumeValue;
    public int playerScore;
}


    
