using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDialogBox : MonoBehaviour
{
    public static bool isShowingGUI = false;

    public GameObject canvas;

    public List<GameObject> uiElements;

    private float canvasWidth;
    private float canvasHeight;

    private ButtonGUI backButton;
    private VolumeSliderGUI volumeSlider;

    private Texture volumeTexture;

    private SaveDataController saveDataController;

    private void Awake()
    {
        volumeTexture = Resources.Load<Texture>("UI/Volume Texture");
    }

    private void Start()
    {
        GetCanvasRect();
        saveDataController = new SaveDataController();
        saveDataController.LoadGame();
        SetupAllGUI(saveDataController.volumeValue);
    }


    void OnGUI() 
    {
        if (isShowingGUI) 
        {
            saveDataController.volumeValue = volumeSlider.GetSliderValue();
            if (backButton.IsClicked()) 
            {
                saveDataController.SaveGame();
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

    private void SetupAllGUI(float sliderValue)
    {
        SetupVolumeSlider(sliderValue);
        SetupBackButton();
    }

    private void SetupVolumeSlider(float sliderValue)
    {
        float backButtonOffsetX = 0;
        float backButtonOffsetY = 100;
        float backButtonWidth = 300;
        float backButtonHeight = 10;
        float backButtonX = (canvasWidth / 2) - (backButtonWidth / 2) + backButtonOffsetX;
        float backButtonY = (canvasHeight / 2) - (backButtonHeight / 2) + backButtonOffsetY;
        float sliderLeftValue = 0f;
        float sliderRightValue = 100f;
        volumeSlider = new VolumeSliderGUI(volumeTexture,backButtonWidth, backButtonHeight, backButtonX, backButtonY, sliderValue, sliderLeftValue,sliderRightValue);
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
        isShowingGUI = true;
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }

    }

    private void HideSettingsUI()
    {
        isShowingGUI = false;
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(true);
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

    public virtual float GetSliderValue()
    {
        Rect sliderRect = GetRect();
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, sliderLeftValue, sliderRightValue);
        
        return sliderValue;
    }

    public float GetSliderLeftValue()
    {
        return sliderLeftValue;
    }

    public float GetSliderRightValue()
    {
        return sliderRightValue;
    }
}

public class VolumeSliderGUI : SliderGUI
{
    private Texture texture;
    private string stateText;

    private LabelGUI label;
    private ImageGUI image;


    public VolumeSliderGUI(Texture texture, float sliderWidth, float sliderHeight, float sliderX, float sliderY, float sliderValue, float sliderLeftValue, float sliderRightValue) : base(sliderWidth, sliderHeight, sliderX, sliderY, sliderValue, sliderLeftValue, sliderRightValue)
    {
        this.texture = texture;
        stateText = sliderValue.ToString();
        SetNearUI();
    }

    private void SetNearUI()
    {
        float labelWidth = GetWidth();
        float labelHeight = GetHeight() + 40;
        float labelX = GetX() + GetWidth() + 10;
        float labelY = GetY() - 5;
        label = new LabelGUI(labelWidth,labelHeight,labelX,labelY, stateText);

        float imageWidth = 50;
        float imageHeight = 50;
        float imageX = GetX() - imageWidth - 10;
        float imageY = GetY() - (imageHeight / 2);
        image = new ImageGUI(imageWidth,imageHeight,imageX,imageY, texture);
    }

    public override float GetSliderValue()
    {
        Rect sliderRect = GetRect();
        float oldSliderValue = base.GetSliderValue();
        float sliderLeftValue = GetSliderLeftValue();
        float sliderRightValue = GetSliderRightValue();
        float sliderValue = GUI.HorizontalSlider(sliderRect, oldSliderValue, sliderLeftValue, sliderRightValue);
        sliderValue = Mathf.Round(sliderValue);
        string textSliderValue = sliderValue.ToString();
        label.text = textSliderValue;

        ShowAllGUI();

        return sliderValue;
    }

    private void ShowAllGUI()
    {
        label.Show();
        image.Show();
    }
}

public class LabelGUI : MyGUI
{
    public string text;

    public LabelGUI(float labelWidth, float labelHeight, float labelX, float labelY, string text) : base(labelWidth, labelHeight, labelX, labelY)
    {
        this.text = text;
    }

    public override void Show()
    {
        Rect labelRect = GetRect();
        GUI.Label(labelRect, text);
    }
}

public class ImageGUI : MyGUI
{
    private Texture image;

    public ImageGUI(float imageWidth,float imageHeight, float imageX, float imageY, Texture image) : base(imageWidth, imageHeight, imageX, imageY)
    {
        this.image = image;
    }

    public override void Show()
    {
        Rect imageRect = GetRect();
        GUI.DrawTexture(imageRect, image);
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

    public virtual void Show() { }
}
