

public class EraseButton : ClickableImage
{
    protected override void OnClicked()
    {
        StartCoroutine(Clicked());
        InputController.Instance.RemoveNumberOnTile();
    }
}
