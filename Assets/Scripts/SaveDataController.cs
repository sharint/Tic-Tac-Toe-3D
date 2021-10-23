using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveDataController
{
    private const string saveSettingsDataPath = "SaveSettingsData.dat";
    private const string saveEndGameDataPath = "SaveEndGameData.dat";
    private const string saveStartGameDataPath = "SaveStartGameData.dat";

    public enum SaveDataTypes {None, Settings, StartGame, EndGame}

    public float volumeValue = 100;

    public int playerScore = 0;
    public string winner = "Отсутствует";
    public string time = "Отсутствует";

    public string playerName = "Отсутствует";
    public string enemyName = "Отсутствует";
    public int levelID = 0;
    public int widthField = 3;
    public int heightField = 3;
    public int streakLength = 3;


    public void Save(SaveDataTypes saveDataTypes)
    {
        switch (saveDataTypes)
        {
            case SaveDataTypes.Settings:
                SaveSettingsData();
                break;
            case SaveDataTypes.EndGame:
                SaveEndGameData();
                break;
            case SaveDataTypes.StartGame:
                SaveStartGameData();
                break;
            default:
                Debug.LogError("Error, Unknown save data parameter");
                break;
        }
    }

    public void Load(SaveDataTypes saveData)
    {
        switch (saveData)
        {
            case SaveDataTypes.Settings:
                LoadSettingsData();
                break;
            case SaveDataTypes.EndGame:
                LoadEndGameData();
                break;
            case SaveDataTypes.StartGame:
                LoadStartGameData();
                break;
            default:
                Debug.LogError("Error, Unknown save data parameter");
                break;
        }
    }

    private void SaveSettingsData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveSettingsDataPath);
        SaveSettingsData saveData = new SaveSettingsData();

        saveData.volumeValue = volumeValue;

        bf.Serialize(file, saveData);
        file.Close();
    }

    private void SaveEndGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveEndGameDataPath);
        SaveEndGameData saveEndGameData = new SaveEndGameData();

        saveEndGameData.winner = winner;
        saveEndGameData.time = time;
        saveEndGameData.playerScore = playerScore;

        bf.Serialize(file, saveEndGameData);
        file.Close();
    }

    private void SaveStartGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveStartGameDataPath);
        SaveStartGameData saveStartGameData = new SaveStartGameData();

        saveStartGameData.playerName = playerName;
        saveStartGameData.enemyName = enemyName;
        saveStartGameData.levelID = levelID;
        saveStartGameData.widthField = widthField;
        saveStartGameData.heightField = heightField;
        saveStartGameData.streakLength = streakLength;

        bf.Serialize(file, saveStartGameData);
        file.Close();
    }

    private void LoadSettingsData()
    {
        if (File.Exists(Application.persistentDataPath
          + saveSettingsDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveSettingsDataPath, FileMode.Open);
            SaveSettingsData saveData = (SaveSettingsData)bf.Deserialize(file);
            file.Close();

            volumeValue = saveData.volumeValue;
        }
        else
            Debug.LogError("There is no save data!");
    }

    private void LoadEndGameData()
    {
        if (File.Exists(Application.persistentDataPath
          + saveSettingsDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveEndGameDataPath, FileMode.Open);
            SaveEndGameData saveEndGameData = (SaveEndGameData)bf.Deserialize(file);
            file.Close();

            winner = saveEndGameData.winner;
            time = saveEndGameData.time;
            playerScore = saveEndGameData.playerScore;
        }
        else
            Debug.LogError("There is no save data!");
    }

    private void LoadStartGameData()
    {
        if (File.Exists(Application.persistentDataPath
          + saveSettingsDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveStartGameDataPath, FileMode.Open);
            SaveStartGameData saveStartGameData = (SaveStartGameData)bf.Deserialize(file);
            file.Close();

            playerName = saveStartGameData.playerName;
            enemyName = saveStartGameData.enemyName;
            levelID = saveStartGameData.levelID;
            widthField = saveStartGameData.widthField;
            heightField = saveStartGameData.heightField;
            streakLength = saveStartGameData.streakLength;
        }
        else
            Debug.LogError("There is no save data!");
    }

}

[Serializable]
public class SaveSettingsData
{
    public float volumeValue;
}

[Serializable]
public class SaveEndGameData
{
    public string winner;
    public string time;
    public int playerScore;
}

[Serializable]
public class SaveStartGameData
{
    public string playerName;
    public string enemyName;
    public int levelID;
    public int widthField;
    public int heightField;
    public int streakLength;
}

    
