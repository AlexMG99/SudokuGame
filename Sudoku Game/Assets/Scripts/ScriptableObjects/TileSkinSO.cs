using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSkin", menuName = "ScriptableObjects/Tiles/New Tile Skin", order = 1)]
public class TileSkinSO : ScriptableObject
{
    [Header("Color Tile Settings")]
    [SerializeField]
    private Color tileIdleColor;
    public Color TileIdleColor => tileIdleColor;

    [SerializeField]
    private Color tileHoverColor;
    public Color TileHoverColor => tileHoverColor;

    [SerializeField]
    private Color tileSelectedColor;
    public Color TileSelectedColor => tileSelectedColor;

    [SerializeField]
    private Color tileSameNumberColor;
    public Color TileSameNumberColor => tileSameNumberColor;

    [SerializeField]
    private Color tileBorderColor;
    public Color TileBorderColor => tileBorderColor;

    [SerializeField]
    private Color numberLockColor;
    public Color NumberLockColor => numberLockColor;

    [SerializeField]
    private Color numberSolutionColor;
    public Color NumberSolutionColor => numberSolutionColor;

    [SerializeField]
    private Color notesColor;
    public Color NotesColor => notesColor;


}
