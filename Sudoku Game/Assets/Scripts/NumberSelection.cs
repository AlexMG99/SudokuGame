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
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextColor;
        buttonImage.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;
    }

    public void OnClicked()
    {
        if (!InputController.Instance.IsNotesMode())
        {
            InputController.Instance.ChangeSelectedNumber(numberValue);
            HighlightNumber();
        }
        else
        {
            InputController.Instance.AddNoteNumber(numberValue);
        }
    }

    public void LowlightNumber()
    {
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextIdleColor;
    }

    public void HighlightNumber()
    {
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextSelectColor;
    }
    #endregion
}
