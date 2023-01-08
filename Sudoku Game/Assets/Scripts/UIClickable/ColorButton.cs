using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Audio.AudioSFX;

public class ColorButton : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private Image selectionCircle;

    public int IdxSkin => idxSkin;
    private int idxSkin = -1;
    public bool IsSelected => isSelected;
    private bool isSelected = false;

    #region PublicFunction
    public void Init(bool _isSelected, int _idxSkin)
    {
        isSelected = _isSelected;
        idxSkin = _idxSkin;

        selectionCircle.enabled = isSelected;
    }

    public void Deselect()
    {
        isSelected = false;

        selectionCircle.enabled = isSelected;
    }
    #endregion

    #region IEvenyHandler
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClicked();

        AudioSFX.Instance.PlaySFX("UIButton");
    }
    #endregion

    #region PrivateFunction
    private void OnClicked()
    {
        isSelected = true;
        selectionCircle.enabled = isSelected;

        SkinController.Instance.ChangeSkin(idxSkin);
    }
    #endregion
}
