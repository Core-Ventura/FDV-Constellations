using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StarNamesManager : MonoBehaviour
{
    public bool starNamesVisibility = true;
    public TextMeshProUGUI[] starNamesTMP;
    public void UpdateStarsNameVisibility()
    {
        for(int i=0; i<starNamesTMP.Length; i++)
        {
            if(starNamesTMP[i].GetComponentInParent<StarDisplay>().discovered == true && starNamesVisibility == true)
            {
                starNamesTMP[i].text = starNamesTMP[i].GetComponentInParent<StarDisplay>().star.name;
            } else {
                starNamesTMP[i].text = "";
            }
        }
    }

    public void StarNameButton()
    {
        starNamesVisibility = !starNamesVisibility;
        UpdateStarsNameVisibility();
    }
}
