using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : ClickableImage
{

    protected override void OnClicked()
    {
        StartCoroutine(Clicked());

        InputController.Instance.UndoMovement();
    }

}
