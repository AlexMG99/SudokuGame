using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviourSingleton<SkinController>
{
    [Header("Grid Skin")]
    [SerializeField] List<GridSkinSO> gridSkins;
    public GridSkinSO CurrentGridSkin => currentGridSkin;
    private GridSkinSO currentGridSkin;

    [Header("Tile Skin")]
    [SerializeField] List<TileSkinSO> tileSkins;
    public TileSkinSO CurrentTileSkin => currentTileSkin;
    private TileSkinSO currentTileSkin;

    private int currentSkinIdx = 0;

    [Header("Broadcasting To:")]
    [SerializeField]
    private VoidEventSO changeSkinEvent;

    public override void Awake()
    {
        base.Awake();
        currentSkinIdx = PlayerPrefs.GetInt("SkinIndex", 0);
        ChangeSkin(currentSkinIdx);
    }

    public void ChangeSkin(int newSkinIdx)
    {
        currentSkinIdx = newSkinIdx;

        currentTileSkin = tileSkins[currentSkinIdx];
        currentGridSkin = gridSkins[currentSkinIdx];

        PlayerPrefs.SetInt("SkinIndex", currentSkinIdx);

        changeSkinEvent.Raise();
    }
}
