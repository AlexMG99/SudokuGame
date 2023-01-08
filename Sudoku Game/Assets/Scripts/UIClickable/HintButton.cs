using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintButton : ClickableImage
{
    [SerializeField]
    private TextMeshProUGUI hintCountText;
    [SerializeField]
    private Image circleOutlineImage;
    [SerializeField]
    private Image circleInnerImage;

    protected override void Init()
    {
        hintCountText.text = InputController.Instance.HintCount.ToString();
        Debug.Log(InputController.Instance.HintCount);
    }

    protected override void OnClicked()
    {
        StartCoroutine(Clicked());
        InputController.Instance.UseHint();
        if(InputController.Instance.HintCount <= 0)
            hintCountText.text = "Ad";
        else
            hintCountText.text = InputController.Instance.HintCount.ToString();
    }

    public override void SetSkin()
    {
        base.SetSkin();
        circleOutlineImage.color = hintCountText.color = SkinController.Instance.CurrentGridSkin.BackgroundColor;
        circleInnerImage.color = SkinController.Instance.CurrentGridSkin.ButtonUIColor;
    }

}
