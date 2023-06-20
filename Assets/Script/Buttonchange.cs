using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonchange : MonoBehaviour
{
    public Image imageColor;
    public Color color1, color2;
    public bool isChanged = false;
    public void ColorChange()
    {
        if (isChanged == false)
        {
            imageColor.color = color1;
            isChanged = true;
        }
        else if (isChanged == true)
        {
            imageColor.color = color2;
            isChanged = false;
        }

    }
}
