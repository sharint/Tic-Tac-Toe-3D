using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsDialogBox : MonoBehaviour
{
    bool stateGUI = false;

    public GameObject canvas;

    public List<GameObject> buttons;

    private float canvasWidth;
    private float canvasHeight;

    private ButtonGUI backButton;
    private SliderGUI volumeSlider;

    private void Start()
    {
        GetCanvasRect();
        SetupAllGUI();
    }


    void OnGUI() 
    {
        if (stateGUI) 
        {
            print(volumeSlider.GetSliderValue());
            if (backButton.IsClicked()) 
            {
                HideSettingsUI();
            }
        }
    }

    private void GetCanvasRect()
    {
        RectTransform rectTransformCanvas = canvas.GetComponent<RectTransform>();
        canvasWidth = rectTransformCanvas.rect.width;
        canvasHeight = rectTransformCanvas.rect.height;
    }

    private void SetupAllGUI()
    {
        SetupVolumeSlider();
        SetupBackButton();
    }

    private void SetupVolumeSlider()
    {
        float backButtonOffsetX = 0;
        float backButtonOffsetY = 100;
        float backButtonWidth = 300;
        float backButtonHeight = 10;
        float backButtonX = (canvasWidth / 2) - (backButtonWidth / 2) + backButtonOffsetX;
        float backButtonY = (canvasHeight / 2) - (backButtonHeight / 2) + backButtonOffsetY;
        float sliderValue = 1f;
        float sliderLeftValue = 0f;
        float sliderRightValue = 1f;
        volumeSlider = new SliderGUI(backButtonWidth, backButtonHeight, backButtonX, backButtonY, sliderValue, sliderLeftValue,sliderRightValue);
    }


    private void SetupBackButton()
    {
        float backButtonOffsetX = 0;
        float backButtonOffsetY = 200;
        float backButtonWidth = 300;
        float backButtonHeight = 50;
        float backButtonX = (canvasWidth / 2) - (backButtonWidth / 2) + backButtonOffsetX;
        float backButtonY = (canvasHeight / 2) - (backButtonHeight / 2) + backButtonOffsetY;
        string backButtonName = "Back";
        backButton = new ButtonGUI(backButtonWidth, backButtonHeight, backButtonX, backButtonY, backButtonName);
    }

 

    private void ShowSettingsUI()
    {
        stateGUI = true;
        foreach (GameObject uiButton in buttons)
        {
            uiButton.SetActive(false);
        }

    }

    private void HideSettingsUI()
    {
        stateGUI = false;
        foreach (GameObject uiButton in buttons)
        {
            uiButton.SetActive(true);
        }
    }

    public void Click()
    { 
        ShowSettingsUI();   
    }
}

public class ButtonGUI
{
    private float buttonWidth;
    private float buttonHeight;
    private float buttonX;
    private float buttonY;
    private string buttonName;
    private Rect buttonRect;

    public ButtonGUI(float buttonWidth, float buttonHeight, float buttonX, float buttonY, string buttonName)
    {
        this.buttonWidth = buttonWidth;
        this.buttonHeight = buttonHeight;
        this.buttonX = buttonX;
        this.buttonY = buttonY;
        this.buttonName = buttonName;
        buttonRect = CreateRect();
    }

    private Rect CreateRect()
    {
        return new Rect(buttonX, buttonY, buttonWidth, buttonHeight);
    }

    public bool IsClicked()
    {
        return GUI.Button(buttonRect, buttonName);
    }
}

public class SliderGUI
{
    private float sliderWidth;
    private float sliderHeight;
    private float sliderX;
    private float sliderY;
    private float sliderValue;
    private float sliderLeftValue;
    private float sliderRightValue;
    private Rect sliderRect;

    

    public SliderGUI(float sliderWidth, float sliderHeight, float sliderX, float sliderY, float sliderValue, float sliderLeftValue, float sliderRightValue)
    {
        this.sliderWidth = sliderWidth;
        this.sliderHeight = sliderHeight;
        this.sliderX = sliderX;
        this.sliderY = sliderY;
        this.sliderValue = sliderValue;
        this.sliderLeftValue = sliderLeftValue;
        this.sliderRightValue = sliderRightValue;
        sliderRect = CreateRect();
    }

    private Rect CreateRect()
    {
        return new Rect(sliderX, sliderY, sliderWidth, sliderHeight);
    }



    public float GetSliderValue()
    {
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, sliderLeftValue, sliderRightValue);
        return sliderValue;
    }
}
