using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationsLinesManager : MonoBehaviour
{
    public bool linesVisibility = true;
    public bool constellationNamesVisibility = true;
    public StarDisplay[] orionStarDisplays;
    public GameObject orionLines;
    public GameObject orionName;
    public bool orionDiscovered = false;
    public void UpdateLinesVisibility()
    {
        int aux = 0;

        for(int i=0; i<orionStarDisplays.Length; i++)
        {
            if(orionStarDisplays[i].discovered == true)
            {
                aux++;
            }
        }

        if(aux == orionStarDisplays.Length && linesVisibility == true)
        {
            orionLines.SetActive(true);
            orionDiscovered = true;
            UpdateConstellationsNamesVisibility();
        } else {
            orionLines.SetActive(false);
        }
    }
    public void UpdateConstellationsNamesVisibility()
    {
        if(constellationNamesVisibility == true && orionDiscovered == true)
        {
            orionName.SetActive(true);
        } else {
            orionName.SetActive(false);           
        }
    }
    public void ConstellationNameButton()
    {
        constellationNamesVisibility = !constellationNamesVisibility;
        UpdateConstellationsNamesVisibility();
    }
    public void ConstellationLinesButton()
    {
        linesVisibility = !linesVisibility;
        UpdateLinesVisibility();
    }
}
