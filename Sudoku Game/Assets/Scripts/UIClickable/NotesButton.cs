using Audio.AudioSFX;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotesButton : ClickableImage
{

    [SerializeField]
    private TextMeshProUGUI notesText;
    [SerializeField]
    private Image circleOutlineImage;
    [SerializeField]
    private Image circleInnerImage;

    private bool isSelected = false;

    protected override void OnClicked()
    {
        AudioSFX.Instance.PlaySFX("Switch");

        ChangeButtonState();
        InputController.Instance.SetNotesMode(isSelected);
    }

    private void ChangeButtonState()
    {
        if (isSelected)
        {
            StartCoroutine(ClickExit());
            circleInnerImage.color = SkinController.Instance.CurrentGridSkin.ButtonIdleColor;
            notesText.text = "OFF";
        }
        else
        {
            StartCoroutine(ClickEnter());
            circleInnerImage.color = SkinController.Instance.CurrentGridSkin.ImageColor;
            notesText.text = "ON";
        }

        isSelected = !isSelected;
    }

    public override void SetSkin()
    {
        base.SetSkin();
        circleOutlineImage.color = notesText.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;
        circleInnerImage.color = SkinController.Instance.CurrentGridSkin.ButtonIdleColor;
    }
}
