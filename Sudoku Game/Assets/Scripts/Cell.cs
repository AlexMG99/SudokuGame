using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Cell Skin Elements")]
    [SerializeField] private Image gridBorderImage;

    [SerializeField]
    private Transform tileContainer;

    private List<Tile> tiles = new List<Tile>();

    #region MonoBehaviourFunctions
    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region PrivateFunctions
    private void Init()
    {
        tiles.AddRange(tileContainer.GetComponentsInChildren<Tile>());
    }

    private void SetSkin()
    {
        gridBorderImage.color = SkinController.Instance.CurrentGridSkin.GridBorderColor;
    }

    private Tile GetTileByNumber(int number)
    {
        Tile numberTile = null;

        foreach (Tile tile in tiles)
        {
            if (tile.CheckNumber(number))
            {
                numberTile = tile;
                break;
            }
        }

        if (!numberTile)
            Debug.LogError($"There is no tile with number {number}!");
        else if (!numberTile.IsSolved())
            return null;

        return numberTile;
    }
    #endregion

    #region PublicFunctions

    public void UpdateSkins()
    {
        SetSkin();
        foreach (Tile tile in tiles)
        {
            tile.SetSkin();
        }
    }

    public void SetTilesNumbers(string solutionNumbers, string lockedNumbers)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetNumber(solutionNumbers[i], lockedNumbers[i]);
        }
    }

    public void HighlightNumberInCell(int number)
    {
        Tile highlightTile = GetTileByNumber(number);
        if (highlightTile)
            highlightTile.HighlightTile();
    }

    public void DownlightNumberInCell(int number)
    {
        Tile highlightTile = GetTileByNumber(number);
        if (highlightTile)
            highlightTile.DownlightTile();
    }

    public void DownlightCell()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.IsHighlighted())
                tile.DownlightTile();
        }
    }
    #endregion
}
