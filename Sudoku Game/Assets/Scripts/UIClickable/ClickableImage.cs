using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public abstract class ClickableImage : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField]
    protected Image clickableIcon;
    [SerializeField]
    protected TextMeshProUGUI clickableLabel;

    protected Timer clickAnimTimer = new Timer();


    #region MonoBehaviourFunctions

    public virtual void Start()
    {
        Init();
        SetSkin();
    }
    #endregion

    #region IEventHandler
    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance.GameState == GameManager.GameStatus.PLAY)
            OnClicked();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState == GameManager.GameStatus.PLAY)
            OnClickEnter();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState == GameManager.GameStatus.PLAY)
            OnClickExit();
    }
    #endregion

    #region ProtectedFunctions
    protected virtual void OnClicked() {}

    protected virtual void OnClickEnter() {}

    protected virtual void OnClickExit() {}

    protected virtual void Init() { }

    public virtual void SetSkin()
    {
        clickableLabel.color = clickableIcon.color = SkinController.Instance.CurrentGridSkin.ButtonIdleColor;
    }

    protected IEnumerator Clicked()
    {
        float time = 0.15f;
        float speed = 1 / time;
        float status = 0f;
        clickAnimTimer.StartTimer(time);
        while (!clickAnimTimer.CheckTime())
        {
            status = speed * Time.deltaTime;
            Color color = Color.Lerp(SkinController.Instance.CurrentGridSkin.ButtonHoverColor, SkinController.Instance.CurrentGridSkin.ButtonIdleColor, status);
            clickableIcon.color = color;
            clickableLabel.color = color;
            yield return null;
        }

        clickAnimTimer.StartTimer(time);
        while (!clickAnimTimer.CheckTime())
        {
            status = speed * Time.deltaTime;
            Color color = Color.Lerp(SkinController.Instance.CurrentGridSkin.ButtonIdleColor, SkinController.Instance.CurrentGridSkin.ButtonHoverColor, status);
            clickableIcon.color = color;
            clickableLabel.color = color;
            yield return null;
        }
    }

    protected IEnumerator ClickExit()
    {
        float time = 0.05f;
        float speed = 1 / time;
        float status = 0f;
        clickAnimTimer.StartTimer(time);
        while (!clickAnimTimer.CheckTime())
        {
            status = speed * Time.deltaTime;
            Color color = Color.Lerp(SkinController.Instance.CurrentGridSkin.ButtonHoverColor, SkinController.Instance.CurrentGridSkin.ButtonIdleColor, 1 - status);
            clickableIcon.color = color;
            clickableLabel.color = color;
            yield return null;
        }
    }

    protected IEnumerator ClickEnter()
    {
        float time = 0.05f;
        float speed = 1 / time;
        float status = 0f;

        clickAnimTimer.StartTimer(time);
        while (!clickAnimTimer.CheckTime())
        {
            status = speed * Time.deltaTime;
            Color color = Color.Lerp(SkinController.Instance.CurrentGridSkin.ButtonIdleColor, SkinController.Instance.CurrentGridSkin.ButtonHoverColor, 1 - status);
            clickableIcon.color = color;
            clickableLabel.color = color;
            yield return null;
        }
    }


    #endregion
}
