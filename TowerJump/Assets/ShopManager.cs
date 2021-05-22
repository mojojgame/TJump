using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager script;

    public Material bgMaterial;
    public Material ballMaterial;
    public Material platformMaterial;
    public Material platformScaleMaterial;
    public Material platformVibrationMaterial;
    public Material platformOpacityMaterial;

    public Sprite[] bgSkins;
    public Button[] bgButtons;
    public bool[] bgBool;

    public Sprite[] ballSkins;
    public Button[] ballButtons;
    public bool[] ballBool;

    public Sprite[] platformSkins;
    public Button[] platformButtons;
    public bool[] platformBool;


    public int selectedBgSkin;
    public int selectedBallSkin;
    public int selectedPlatformSkin;

    private void Awake()
    {
        script = this;
    }
    void Start()
    {
        StateHud.script.MusicLoad();
        for (int i = 0; i < bgButtons.Length; i++)
        {
            int index = i;
            bgButtons[i].onClick.AddListener(() => SetBg(index));
        }
        for (int i = 0; i < ballButtons.Length; i++)
        {
            int index = i;
            ballButtons[i].onClick.AddListener(() => SetBall(index));
        }
        for (int i = 0; i < platformButtons.Length; i++)
        {
            int index = i;
            platformButtons[i].onClick.AddListener(() => SetPlatform(index));
        }
        SetFromSave();
    }

    void SetFromSave()
    {
        bgMaterial.SetTexture("_MainTex", bgSkins[selectedBgSkin].texture);
        ballMaterial.SetTexture("_MainTex", ballSkins[selectedBallSkin].texture);

        platformMaterial.SetTexture("_MainTex", platformSkins[selectedPlatformSkin].texture);
        platformScaleMaterial.SetTexture("_MainTex", platformSkins[selectedPlatformSkin].texture);
        platformOpacityMaterial.SetTexture("_MainTex", platformSkins[selectedPlatformSkin].texture);
        platformVibrationMaterial.SetTexture("_MainTex", platformSkins[selectedPlatformSkin].texture);
    }

    public void SetBg(int i)
    {
        if(bgBool[i] == false)
        {
            bgMaterial.SetTexture("_MainTex", bgSkins[i].texture);
            selectedBgSkin = i;
        }
    }
    public void SetBall(int i)
    {
        if (ballBool[i] == false)
        {
            ballMaterial.SetTexture("_MainTex", ballSkins[i].texture);
            selectedBallSkin = i;
        }
    }
    public void SetPlatform(int i)
    {
        if (platformBool[i] == false)
        {
            platformMaterial.SetTexture("_MainTex", platformSkins[i].texture);
            platformScaleMaterial.SetTexture("_MainTex", platformSkins[i].texture);
            platformOpacityMaterial.SetTexture("_MainTex", platformSkins[i].texture);
            platformVibrationMaterial.SetTexture("_MainTex", platformSkins[i].texture);
            selectedPlatformSkin = i;
        }
    }

}
