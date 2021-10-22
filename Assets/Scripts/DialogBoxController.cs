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

    public enum ButtonStates {Yes, No, Cancel, None};
    private ButtonStates buttonState;

    private void Start()
    {
        isShowingDialogBox = true;
        buttonState = ButtonStates.None;
        GetCanvasRect();
        SetupBackButton();
    }

    private void OnGUI()
    {
        if (isShowingDialogBox)
        {
            buttonState = textureAndTextDialogBox.ShowAllGUI();
        }
        switch (buttonState)
        {
            case ButtonStates.Yes:
                isShowingDialogBox = false;
                print("Yes pressed");
                break;
            case ButtonStates.No:
                isShowingDialogBox = false;
                print("no pressed");
                break;
            case ButtonStates.None:
                break;
            default:
                isShowingDialogBox = false;
                print("Error, somethin went wrong");
                break;
        }
    }

    private void GetCanvasRect()
    {
        RectTransform rectTransformCanvas = canvas.GetComponent<RectTransform>();
        canvasWidth = rectTransformCanvas.rect.width;
        canvasHeight = rectTransformCanvas.rect.height;
    }

    private void SetupBackButton()
    {
        float backButtonOffsetX = 0;
        float backButtonOffsetY = 200;
        float backButtonWidth = 300;
        float backButtonHeight = 150;
        float backButtonX = (canvasWidth / 2) - (backButtonWidth / 2) + backButtonOffsetX;
        float backButtonY = (canvasHeight / 2) - (backButtonHeight / 2) + backButtonOffsetY;
        string backButtonName = "Are you sure?";
        textureAndTextDialogBox = new TextureAndTextDialogBox(backButtonWidth, backButtonHeight, backButtonX, backButtonY,texture, backButtonName);
    }

    public void Click()
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
