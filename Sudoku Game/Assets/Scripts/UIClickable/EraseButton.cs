using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseButton : ClickableImage
{
    protected override void OnClicked()
    {
        StartCoroutine(Clicked());

        InputController.Instance.RemoveNumberOnTile();
    }
}
