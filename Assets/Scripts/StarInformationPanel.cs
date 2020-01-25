using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarInformationPanel : MonoBehaviour
{
    public Star star;
    [HideInInspector]
    public Color starColor;

    [Header("Colors")]
    public Color[] starColors;
    [Header("Information Panel")]
    public TextMeshProUGUI starNameTitle; //Color & Text
    public Image starRepresentation; //Color & Size
    public TextMeshProUGUI constellationTitle; //Color
    public TextMeshProUGUI constellationName; //Text
    public TextMeshProUGUI apparentMagnitudeTitle; //Color
    public TextMeshProUGUI apparentMagnitudeText; //Text
    public TextMeshProUGUI absoluteMagnitudeTitle; //Color
    public TextMeshProUGUI absoluteMagnitudeText; //Text
    public TextMeshProUGUI temperatureTitle; //Color
    public TextMeshProUGUI temperatureText; //Text
    public Image[] scaleCircles; //From -2 to 6 (9) - Color
    public TextMeshProUGUI massTitle; //Color
    public TextMeshProUGUI massText; //Text
    public TextMeshProUGUI luminosityTitle; //Color
    public TextMeshProUGUI luminosityText; //Text
    public TextMeshProUGUI radiusTitle; //Color
    public TextMeshProUGUI radiusText; //Text
    public TextMeshProUGUI observationsTitle; //Color
    public TextMeshProUGUI observationsText; //Text
    public Animator animator;

    public void OnEnable()
    {
        animator.SetBool("disappear", false);
    }

    public void UpdateInformation()
    {
        UpdateColor();
        starNameTitle.color = starColor;
        starNameTitle.text = star.name;
        starRepresentation.color = starColor;
        
        Vector3 newScale = Vector3.one * Mathf.Pow(2.512f, (-1 * star.apparentMagnitude) + 1f);

        if(star.apparentMagnitude < 0){
            newScale = Vector3.one * Mathf.Pow(2.512f, (-1 * star.apparentMagnitude) + 1f) / 7f;
        } else {
            newScale = Vector3.one * Mathf.Pow(2.512f, (-1 * star.apparentMagnitude) + 1f / 3f);
        }

        starRepresentation.transform.localScale = newScale;

        constellationTitle.color = starColor;
        constellationName.text = star.constellation;

        apparentMagnitudeTitle.color = starColor;
        apparentMagnitudeText.text = star.apparentMagnitude.ToString();

        absoluteMagnitudeTitle.color = starColor;
        absoluteMagnitudeText.text = star.absoluteMagnitude.ToString();

        temperatureTitle.color = starColor;
        temperatureText.text = star.temperature.ToString();

        for(int i = 0; i < scaleCircles.Length; i++)
        {
            scaleCircles[i].color = Color.white;
        }

        scaleCircles[(Mathf.RoundToInt(star.apparentMagnitude) + 2)].color = starColor;

        massTitle.color = starColor;
        massText.text = star.mass.ToString();      

        luminosityTitle.color = starColor;
        luminosityText.text = star.luminosity.ToString();    

        radiusTitle.color = starColor;
        radiusText.text = star.radius.ToString();   

        observationsTitle.color = starColor;
        observationsText.text = star.obervations;         
    }

    public void UpdateColor()
    {
        // RED Star
        if(star.temperature >= 1300 && star.temperature < 2000){
            starColor = starColors[0];
            return;
        }

        // ORANGE Star
        if(star.temperature >= 2000 && star.temperature < 3700){
            starColor = starColors[1];
            return;
        }

        // YELLOW Star
        if(star.temperature >= 3700 && star.temperature < 6000){
            starColor = starColors[2];
            return;
        }

        // WHITE Star
        if(star.temperature >= 6000 && star.temperature < 7500){
            starColor = starColors[3];
            return;
        }

        // WHITE BLUE Star
        if(star.temperature >= 7500 && star.temperature < 10000){
            starColor = starColors[4];
            return;
        }      

        // BLUE Star
        if(star.temperature >= 10000 && star.temperature < 33000){
            starColor = starColors[5];
            return;
        } 

        // PURPLE Star
        if(star.temperature >= 33000){
            starColor = starColors[6];
            return;
        } 
    }
    public IEnumerator Disappear()
    {
        animator.SetBool("disappear", true);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
