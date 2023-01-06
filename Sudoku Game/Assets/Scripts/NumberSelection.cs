using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class NumberSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI numberText;

    private int numberValue = -1;
    private bool isSelected = false;

    #region IPointerEvent
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked();
        Debug.Log("NumberTile was clicked");
    }

    #endregion

    #region PrivateFunction
    private void OnClicked()
    {
        if (isSelected)
        {
            InputController.Instance.ChangeSelectedNumber(-1);
        }
        else
        {
            InputController.Instance.ChangeSelectedNumber(numberValue);
            HighlightNumber();
        }
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
    }


    public void LowlightNumber()
    {
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextIdleColor;
        isSelected = false;
    }

    public void HighlightNumber()
    {
        numberText.color = SkinController.Instance.CurrentGridSkin.UITextSelectColor;
        isSelected = true;
    }
    #endregion
}
