using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : MonoBehaviour
{
    public GameObject canvas;

    private float canvasWidth;
    private float canvasHeight;

    public Texture texture;

    private bool isShowingDialogBox = false;

    private TextureAndTextDialogBox textureAndTextDialogBox;
    //private TextFieldAndTextDialogBox textFieldAndTextDialogBox;

    public enum ButtonStates { None, Yes, No, Cancel, Done};
    public ButtonStates buttonState;

    private void Start()
    {
        texture = Resources.Load<Texture>("UI/Volume Texture");
        isShowingDialogBox = false;
        buttonState = ButtonStates.None;
        GetCanvasRect();
        SetupTextureAndTextDialogBox();
    }

    private void OnGUI()
    {
        if (isShowingDialogBox)
        {
            buttonState = textureAndTextDialogBox.ShowAllGUI();
        }
        if (buttonState != ButtonStates.None)
        {
            isShowingDialogBox = false;
        }
    }

    private void GetCanvasRect()
    {
        RectTransform rectTransformCanvas = canvas.GetComponent<RectTransform>();
        canvasWidth = rectTransformCanvas.rect.width;
        canvasHeight = rectTransformCanvas.rect.height;
    }

    private void SetupTextureAndTextDialogBox()
    {
        float backButtonOffsetX = 0;
        float backButtonOffsetY = 200;
        float backButtonWidth = 300;
        float backButtonHeight = 150;
        float backButtonX = (canvasWidth / 2) - (backButtonWidth / 2) + backButtonOffsetX;
        float backButtonY = (canvasHeight / 2) - (backButtonHeight / 2) + backButtonOffsetY;
        string backButtonName = "Are you sure?";
        textureAndTextDialogBox = new TextureAndTextDialogBox(backButtonWidth, backButtonHeight, backButtonX, backButtonY,texture, backButtonName);
        //textFieldAndTextDialogBox = new TextFieldAndTextDialogBox(backButtonWidth, backButtonHeight, backButtonX, backButtonY, backButtonName);
    }

    public void Show()
    {
        isShowingDialogBox = true;
    }
}

public class TextureAndTextDialogBox : MyGUI
{
    private Texture texture;
    private string text;

    private ImageGUI image;
    private BoxGUI box;
    private ButtonGUI yesButton;
    private ButtonGUI noButton;

    public TextureAndTextDialogBox(float width, float height, float x, float y, Texture texture, string text):base(width,height,x,y)
    {
        this.texture = texture;
        this.text = text;
        SetNearUI();
    }

    private void SetNearUI()
    {
        float boxWidth = GetWidth();
        float boxHeight = GetHeight();
        float boxX = GetX();
        float boxY = GetY();
        box = new BoxGUI(boxWidth, boxHeight, boxX, boxY, text);

        float imageWidth = 50;
        float imageHeight = 50;
        float imageX = GetX()+ GetWidth()/2 - imageWidth/2;
        float imageY = GetY() + GetHeight()/2 - imageHeight/2;
        image = new ImageGUI(imageWidth, imageHeight, imageX, imageY, texture);

        float yesButtonWidth = GetWidth() / 2;
        float yesButtonHeight = GetHeight() / 4;
        float yesButtonX = GetX();
        float yesButtonY = GetY() + GetHeight()-yesButtonHeight;
        string yesButtonText = "Yes";
        yesButton = new ButtonGUI(yesButtonWidth, yesButtonHeight,yesButtonX,yesButtonY, yesButtonText);

        float noButtonWidth = GetWidth() / 2;
        float noButtonHeight = GetHeight() / 4;
        float noButtonX = GetX()+GetWidth()/2;
        float noButtonY = GetY() + GetHeight()-noButtonHeight;
        string noButtonText = "No";
        noButton = new ButtonGUI(noButtonWidth, noButtonHeight, noButtonX, noButtonY, noButtonText);
    }

    public DialogBoxController.ButtonStates ShowAllGUI()
    {
        box.Show();
        image.Show();
        if (yesButton.IsClicked())
        {
            return DialogBoxController.ButtonStates.Yes;
        }
        if (noButton.IsClicked())
        {
            return DialogBoxController.ButtonStates.No;
        }
        return DialogBoxController.ButtonStates.None;
    }
}

public class TextFieldAndTextDialogBox : MyGUI
{
    private string text;

    private TextFiledGUI textField;
    private BoxGUI box;
    private ButtonGUI doneButton;
    private ButtonGUI cancelButton;


    public TextFieldAndTextDialogBox(float width, float height, float x, float y, string text) : base(width, height, x, y)
    {
        this.text = text;
        SetNearUI();
    }

    public string GetText()
    {
        return text;
    }

    private void SetNearUI()
    {
        float boxWidth = GetWidth();
        float boxHeight = GetHeight();
        float boxX = GetX();
        float boxY = GetY();
        box = new BoxGUI(boxWidth, boxHeight, boxX, boxY, text);

        float textFieldWidth = GetWidth();
        float textFieldHeight = 75;
        float textFieldX = GetX() + GetWidth() / 2 - textFieldWidth / 2;
        float textFieldY = GetY() + GetHeight() / 2 - textFieldHeight / 2;
        textField = new TextFiledGUI(textFieldWidth, textFieldHeight, textFieldX, textFieldY, text);

        float yesButtonWidth = GetWidth() / 2;
        float yesButtonHeight = GetHeight() / 4;
        float yesButtonX = GetX();
        float yesButtonY = GetY() + GetHeight() - yesButtonHeight;
        string yesButtonText = "Done";
        doneButton = new ButtonGUI(yesButtonWidth, yesButtonHeight, yesButtonX, yesButtonY, yesButtonText);

        float noButtonWidth = GetWidth() / 2;
        float noButtonHeight = GetHeight() / 4;
        float noButtonX = GetX() + GetWidth() / 2;
        float noButtonY = GetY() + GetHeight() - noButtonHeight;
        string noButtonText = "Cancel";
        cancelButton = new ButtonGUI(noButtonWidth, noButtonHeight, noButtonX, noButtonY, noButtonText);
    }

    public DialogBoxController.ButtonStates ShowAllGUI()
    {
        box.Show();
        textField.Show();
        text = textField.GetText();
        textField.SetText(text);
        if (doneButton.IsClicked())
        {
            return DialogBoxController.ButtonStates.Done;
        }
        if (cancelButton.IsClicked())
        {
            return DialogBoxController.ButtonStates.Cancel;
        }
        return DialogBoxController.ButtonStates.None;
    }
}
