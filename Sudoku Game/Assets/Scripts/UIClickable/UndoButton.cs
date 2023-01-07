using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : ClickableImage
{

    protected override void OnClicked()
    {
        StartCoroutine(Clicked());
    }

    /*protected override void OnClickEnter()
    {
        StartCoroutine(ClickEnter());
    }

    protected override void OnClickExit()
    {
        StartCoroutine(ClickExit());
    }*/


}
