using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    public StarDisplay[] orionStarsDisplays;
    public Image[] starRepresentations;

    public void UpdateChallenge()
    {
        for(int i = 0; i<orionStarsDisplays.Length; i++)
        {
            if(orionStarsDisplays[i].discovered)
            {
                starRepresentations[i].color = orionStarsDisplays[i].uiName.color;
            }
        }
    }
}
