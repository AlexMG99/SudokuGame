using Audio.AudioSFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinController : MonoBehaviourSingleton<SkinController>
{
    [Header("Skin UI")]
    [SerializeField] List<ColorButton> colorsButton;

    [SerializeField] List<GridSkinSO> gridSkins;
    public GridSkinSO CurrentGridSkin => currentGridSkin;
    private GridSkinSO currentGridSkin;

    [SerializeField] List<TileSkinSO> tileSkins;
    public TileSkinSO CurrentTileSkin => currentTileSkin;
    private TileSkinSO currentTileSkin;

    [SerializeField]
    private GameObject skinSelectionScreen;

    [SerializeField]
    private Image backgroundImageScreen;
    [SerializeField]
    private Image triangleImageScreen;
    [SerializeField]
    private Image paletteButton;

    private int currentSkinIdx = 0;
    private bool isScreenOpen = false;

    [Header("Broadcasting To:")]
    [SerializeField]
    private VoidEventSO changeSkinEvent;

    #region MonoBehaviourFunctions
    public override void Awake()
    {
        base.Awake();
        currentSkinIdx = PlayerPrefs.GetInt("SkinIndex", 0);
        InitColorButtons();
        ChangeSkin();
    }
    #endregion

    #region PrivateFuntions
    private void InitColorButtons()
    {
        for(int i = 0; i < colorsButton.Count; i++)
        {
            colorsButton[i].Init((i == currentSkinIdx), i);
        }
    }

    private void DeselectButtonColor(int currentSelectedNumber)
    {
        for (int i = 0; i < colorsButton.Count; i++)
        {
            if (colorsButton[i].IsSelected && colorsButton[i].IdxSkin != currentSelectedNumber)
                colorsButton[i].Deselect();
        }
    }

    private void SetSkin()
    {
        backgroundImageScreen.color = triangleImageScreen.color = currentGridSkin.ThemeImageColor;
        paletteButton.color = currentGridSkin.ButtonUIColor;
    }
    #endregion

    #region PublicFunction
    public void SetScreenState()
    {
        isScreenOpen = !isScreenOpen;
        skinSelectionScreen.SetActive(isScreenOpen);

        AudioSFX.Instance.PlaySFX("UIButton");
    }

    public void ChangeSkin(int newSkinIdx)
    {
        currentSkinIdx = newSkinIdx;

        currentTileSkin = tileSkins[currentSkinIdx];
        currentGridSkin = gridSkins[currentSkinIdx];

        DeselectButtonColor(newSkinIdx);

        PlayerPrefs.SetInt("SkinIndex", currentSkinIdx);

        SetSkin();

        changeSkinEvent.Raise();
    }

    public void ChangeSkin()
    {
        currentTileSkin = tileSkins[currentSkinIdx];
        currentGridSkin = gridSkins[currentSkinIdx];

        SetSkin();
    }
    #endregion
}
