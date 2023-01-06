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
    private Color uiTextSelectColor;
    public Color UITextSelectColor => uiTextSelectColor;

    [SerializeField]
    private Color uiTextIdleColor;
    public Color UITextIdleColor => uiTextIdleColor;

}
