using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiViewController : MonoBehaviour
{

    public Text gameSituationStateText;
    public Text playerNameText;
    public Text enemyNameText;
    public Text timer;

    public Image blackOutImage;

    private const float blackOutSeconds = 3f;

    private float pauseTime = 0f;
    private float currentTime = 0f;

    private void Update()
    {
        SetTimer();
    }


    public void GameOver(string gameState)
    {
        gameSituationStateText.text = gameState;
    }

    public void SetDefaultValuesText(Enemy enemy, Player player)
    {
        timer.text = "";
        gameSituationStateText.text = "";
        playerNameText.text = player.GetName();
        enemyNameText.text = enemy.GetName();
    }

    public void SetTimer()
    {
        if (BoardController.isGameOver || SettingsDialogBox.isShowingGUI)
        {
            pauseTime = Time.timeSinceLevelLoad - currentTime;
            return;
        }

        currentTime = Mathf.Round(Time.timeSinceLevelLoad - pauseTime);
        string minutes = ((int)currentTime / 60).ToString();

        float secondsFloat = currentTime % 60;
        string seconds = secondsFloat.ToString();
        if (secondsFloat < 10)
        {
            seconds = "0" + seconds;
        }
        
        timer.text = minutes + ":" + seconds;
    }

    public void Pause()
    {
        SettingsDialogBox settingsDialogBox = new SettingsDialogBox();
        settingsDialogBox.Click(true);
    }
}
