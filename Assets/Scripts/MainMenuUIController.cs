using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUIController : MonoBehaviour
{
    public Text playerScoreText;

    private DialogBoxController dialogBoxController;

   

    private void Start()
    {
        dialogBoxController = gameObject.GetComponent<DialogBoxController>();
        SaveDataController saveDataController = new SaveDataController();
        saveDataController.LoadGame();
        playerScoreText.text = "Рейтинг: " + saveDataController.playerScore.ToString();
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
