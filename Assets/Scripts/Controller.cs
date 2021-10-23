using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public string playerName;
    public string enemyName;
    public int levelID;
    public int widthField;
    public int heightField;
    public int streakLength;


    private void Start()
    {
        SaveDataController saveDataController = new SaveDataController();
        saveDataController.playerName = playerName;
        saveDataController.enemyName = enemyName;
        saveDataController.levelID = levelID;
        saveDataController.widthField = widthField;
        saveDataController.heightField = heightField;
        saveDataController.streakLength = streakLength;
        saveDataController.Save(SaveDataController.SaveDataTypes.StartGame);
    }

    public void Load(int loadSceneID)
    {
        LoadingController.Load(loadSceneID);

    }

  
}
