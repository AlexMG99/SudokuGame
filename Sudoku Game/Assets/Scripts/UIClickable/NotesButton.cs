using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesButton : ClickableImage
{
    [SerializeField]
    private bool isSelected = false;

    protected override void OnClicked()
    {
        ChangeButtonState();

        InputController.Instance.SetNotesMode(isSelected);
    }

    private void ChangeButtonState()
    {
        if (isSelected)
            StartCoroutine(ClickExit());
        else
            StartCoroutine(ClickEnter());

        isSelected = !isSelected;
    }
}
