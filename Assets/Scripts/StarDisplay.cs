using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;
using UnityEngine.UI;
public class StarDisplay : MonoBehaviour
{
    public Star star;
    [HideInInspector]
    public TextMeshProUGUI uiName;
    Transform modelTransform;
    VisualEffect modelVFX;
    public Color[] starColors;
    public VisualEffectAsset[] starVFXs;
    public bool discovered = false;

    public void Awake()
    {
        uiName = GetComponentInChildren<TextMeshProUGUI>();
        uiName.text = "";
    }

    public void Start()
    {
        modelVFX = GetComponentInChildren<VisualEffect>();
        modelTransform = GetComponentInChildren<SphereCollider>().transform;
        ColorSetup();
        modelTransform.localScale = Vector3.one * Mathf.Pow(2.512f, (-1 * star.apparentMagnitude) + 1f);
        modelTransform.localScale /= 100f;
        
        if (modelTransform.localScale.x < 0.01f)
        {
            modelTransform.localScale = Vector3.one * 0.01f;
        }
    }

    public void ColorSetup()
    {
        // RED Star
        if(star.temperature >= 1300 && star.temperature < 2000){
            uiName.color = starColors[0];
            modelVFX.visualEffectAsset = starVFXs[0];
            return;
        }

        // ORANGE Star
        if(star.temperature >= 2000 && star.temperature < 3700){
            uiName.color = starColors[1];
            modelVFX.visualEffectAsset = starVFXs[1];
            return;
        }

        // YELLOW Star
        if(star.temperature >= 3700 && star.temperature < 6000){
            uiName.color = starColors[2];
            modelVFX.visualEffectAsset = starVFXs[2];
            return;
        }

        // WHITE Star
        if(star.temperature >= 6000 && star.temperature < 7500){
            uiName.color = starColors[3];
            modelVFX.visualEffectAsset = starVFXs[3];
            return;
        }

        // WHITE BLUE Star
        if(star.temperature >= 7500 && star.temperature < 10000){
            uiName.color = starColors[4];
            modelVFX.visualEffectAsset = starVFXs[4];
            return;
        }      

        // BLUE Star
        if(star.temperature >= 10000 && star.temperature < 33000){
            uiName.color = starColors[5];
            modelVFX.visualEffectAsset = starVFXs[5];
            return;
        } 

        // PURPLE Star
        if(star.temperature >= 33000){
            uiName.color = starColors[6];
            modelVFX.visualEffectAsset = starVFXs[6];
            return;
        } 
    }
}
