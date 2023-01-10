using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NumberSelection : MonoBehaviour
{
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private TextMeshProUGUI numberText;
    [SerializeField]
    private Button button;

    private int numberValue = -1;


    #region MonoBehaviourFunctions
    private void Start()
    {
        Init();
    }

    #endregion

    #region PublicFunction
    public void Init()
    {
        SetSkin();
        numberValue = int.Parse(numberText.text);
    }

    public void SetSkin()
    {
        numberText.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;
        buttonImage.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;

        var newColorBlock = button.colors;
        newColorBlock.disabledColor = SkinController.Instance.CurrentGridSkin.BackgroundColor;
        button.colors = newColorBlock;
    }

    public void OnClicked()
    {
        if (!InputController.Instance.IsNotesMode())
            InputController.Instance.ChangeSelectedNumber(numberValue);
        else
            InputController.Instance.AddNoteNumber(numberValue);
    }

    public void ChangeNumberState(bool state)
    {
        button.interactable = state;

        if (state)
            numberText.color = SkinController.Instance.CurrentTileSkin.NumberSolutionColor;
        else
            numberText.color = SkinController.Instance.CurrentGridSkin.ButtonHoverColor;
    }
    #endregion
}
