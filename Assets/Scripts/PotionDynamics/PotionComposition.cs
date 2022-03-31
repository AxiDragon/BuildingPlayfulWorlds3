using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PotionComposition : MonoBehaviour
{
    GameObject player;

    static float scaleMod = 1f;
    static float cameraFOV = 60f;
    static float fogAttenuation = 25f;
    static float bloomIntensity = .6f;
    static float bloomScattering = .8f;
    static float saturation = 40f;
    static float exposure = .4f;
    static float contrast = 10f;

    static Vector4 shadowHue = new Vector4(.95f, .9f, 1f, 1f);
    static Vector4 midtoneHue = new Vector4(1f, .96f, .98f, 1f);
    static Vector4 highlightHue = new Vector4(1f, .86f, .91f, 1f);
    
    static Color adjustmentHue = new Color(1f, .9372549f, .9647059f);
    static Color bloomHue = new Color(0f, .8778653f, 1f);
    static Color fogHue = new Color(.753957f, 1f, .7411765f);
    static Color vignetteHue = new Color(1f, .9149776f, .7207547f);

    //Volume postProcessing;
    Fog fog;
    Bloom bloom;
    ShadowsMidtonesHighlights shadowsMidtonesHighlights;
    ColorAdjustments colorAdjustments;
    Vignette vignette;

    void Start()
    {
        //postProcessing = GetComponent<Volume>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.localScale = Vector3.one * scaleMod;
        player.transform.Translate(Vector3.up * scaleMod);

        player.GetComponentInChildren<Camera>().fieldOfView = cameraFOV;


        fog.meanFreePath.value = fogAttenuation;
        fog.tint = new ColorParameter(fogHue);

        bloom.intensity = new ClampedFloatParameter(bloomIntensity, 0f, 1f);
        bloom.scatter = new ClampedFloatParameter(bloomScattering, 0f, 1f);
        bloom.tint = new ColorParameter(bloomHue);

        shadowsMidtonesHighlights.shadows = new Vector4Parameter(shadowHue);
        shadowsMidtonesHighlights.midtones = new Vector4Parameter(midtoneHue);
        shadowsMidtonesHighlights.highlights = new Vector4Parameter(highlightHue);

        colorAdjustments.postExposure = new FloatParameter(exposure);
        colorAdjustments.contrast = new ClampedFloatParameter(contrast, -75f, 100f);
        colorAdjustments.saturation = new ClampedFloatParameter(saturation, -100f, 200f);
        colorAdjustments.colorFilter = new ColorParameter(adjustmentHue);

        vignette.color = new ColorParameter(vignetteHue);
    }

    static public void AddIngredient(string plantName)
    {
        switch (plantName)
        {
            case "Faconite":
                bloomIntensity += .5f;
                bloomScattering += .5f;
                saturation += 5f;
                exposure += .5f;
                contrast += .5f;
                break;
            case "Fantle":
                contrast -= 2.5f;
                shadowHue += new Vector4(-.05f, .05f, -.05f, -.05f);
                highlightHue += new Vector4(-.05f, .05f, -.05f, .05f);
                adjustmentHue += new Color(.05f, -.05f, -.05f);
                break;
            case "Fenbane":
                adjustmentHue += new Color(-.05f, .05f, -.05f);
                shadowHue += new Vector4(-.05f, .05f, -.05f, 0f);
                midtoneHue += new Vector4(-.05f, .05f, -.05f, 0f);
                highlightHue += new Vector4(-.05f, .05f, -.05f, 0f);
                break;
            case "Fervain":
                bloomHue += new Color(.05f, -.03f, -.01f);
                fogHue += new Color(.05f, -.03f, -.01f);
                vignetteHue += new Color(.05f, -.03f, -.01f);
                break;
            case "Fightshade":
                scaleMod *= 1.2f;
                cameraFOV *= 1.05f;
                fogAttenuation *= 1.2f;
                break;
            case "Floppy":
                scaleMod /= 1.2f;
                cameraFOV /= 1.05f;
                fogAttenuation /= 1.2f;
                break;
        }
    }
}
