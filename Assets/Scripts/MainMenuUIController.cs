using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUIController : MonoBehaviour
{
    public Text playerScoreText;

    public Text previousGameDataText;

    private DialogBoxController dialogBoxController;

   

    private void Start()
    {
        dialogBoxController = gameObject.GetComponent<DialogBoxController>();

        SaveDataController saveDataController = new SaveDataController();

        saveDataController.Load(SaveDataController.SaveDataTypes.EndGame);

        string playerScore = saveDataController.playerScore.ToString();
        playerScoreText.text = "Рейтинг: " + playerScore;

        string winner = saveDataController.winner;
        string time = saveDataController.time;
        previousGameDataText.text = "Предыдущая игра - Победитель: " + winner + "\n" + "Время: " + time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dialogBoxController.Show();
        }
        if (dialogBoxController.buttonState == DialogBoxController.ButtonStates.Yes)
        {
            Application.Quit();
        }
    }
}
