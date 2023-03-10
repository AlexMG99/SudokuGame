using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridSkin", menuName = "ScriptableObjects/Grid/New Grid Skin", order = 1)]
public class GridSkinSO : ScriptableObject
{
    [Header("Color Grid Settings")]
    [SerializeField]
    private Color backgroundColor;
    public Color BackgroundColor => backgroundColor;

    [SerializeField]
    private Color gridBorderColor;
    public Color GridBorderColor => gridBorderColor;

    [SerializeField]
    private Color uiTextColor;
    public Color UITextColor => uiTextColor;


    [SerializeField]
    private Color uiTextIdleColor;
    public Color UITextIdleColor => uiTextIdleColor;

    [SerializeField]
    private Color buttonIdleColor;
    public Color ButtonIdleColor => buttonIdleColor;

    [SerializeField]
    private Color buttonHoverColor;
    public Color ButtonHoverColor => buttonHoverColor;

    [SerializeField]
    private Color buttonUIColor;
    public Color ButtonUIColor => buttonUIColor;

    [SerializeField]
    private Color themeImageColor;
    public Color ThemeImageColor => themeImageColor;

}
