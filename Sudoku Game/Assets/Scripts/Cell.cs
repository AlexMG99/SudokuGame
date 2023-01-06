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
    private int cellIdx = -1;

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

    public void SetTilesNumbers(string solutionNumbers, string lockedNumbers, int _cellIdx)
    {
        cellIdx = _cellIdx;
        for (int i = 0; i < tiles.Count; i++)
        {
            Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(i/3) + Mathf.FloorToInt(cellIdx/3) * 3
                ,i + 3 * (cellIdx - Mathf.FloorToInt(i / 3) - Mathf.FloorToInt(cellIdx / 3) * 3));
            tiles[i].SetNumber(solutionNumbers[i], lockedNumbers[i], tilePosition);
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
