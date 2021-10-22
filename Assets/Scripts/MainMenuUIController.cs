using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUIController : MonoBehaviour
{
    public Text playerScoreText;

    private void Start()
    {
        SaveDataController saveDataController = new SaveDataController();
        saveDataController.LoadGame();
        playerScoreText.text = "Рейтинг: " + saveDataController.playerScore.ToString();
    }
}
