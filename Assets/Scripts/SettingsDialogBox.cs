using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class ButtonGUI : MyGUI
{
    private string buttonName;
    
    public ButtonGUI(float buttonWidth, float buttonHeight, float buttonX, float buttonY, string buttonName) : base(buttonWidth, buttonHeight,buttonX,buttonY)
    {
        this.buttonName = buttonName;
    }

    public bool IsClicked()
    {
        Rect buttonRect = GetRect();
        return GUI.Button(buttonRect, buttonName);
    }
}

public class SliderGUI: MyGUI
{
    private float sliderValue;
    private float sliderLeftValue;
    private float sliderRightValue; 

    public SliderGUI(float sliderWidth, float sliderHeight, float sliderX, float sliderY, float sliderValue, float sliderLeftValue, float sliderRightValue) : base(sliderWidth,sliderHeight,sliderX,sliderY)
    {    
        this.sliderValue = sliderValue;
        this.sliderLeftValue = sliderLeftValue;
        this.sliderRightValue = sliderRightValue;
    }

    public float GetSliderValue()
    {
        Rect sliderRect = GetRect();
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, sliderLeftValue, sliderRightValue);
        return sliderValue;
    }
}

public class MyGUI
{
    private float width;
    private float height;
    private float x;
    private float y;
    private Rect rect;

    public MyGUI(float width, float height, float x, float y)
    {
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        rect = CreateRect();
    }

    private Rect CreateRect()
    {
        return new Rect(x, y, width, height);
    }

    public Rect GetRect()
    {
        return rect;
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public float GetWidth()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }
}
