using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class NumberSelection : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI numberText;

    private int numberValue = -1;
    //private bool isSelected = false;

    #region PrivateFunction
    
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
    }

    public void OnClicked()
    {
        InputController.Instance.ChangeSelectedNumber(numberValue);
        HighlightNumber();
    }

    public void LowlightNumber()
    {
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextIdleColor;
        //isSelected = false;
    }

    public void HighlightNumber()
    {
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextSelectColor;
        //isSelected = true;
    }
    #endregion
}
