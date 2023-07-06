using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnLevel : BaseButton
{
    private TMP_Text m_Text;
    protected override void onClick()
    {
        m_Text = GetComponentInChildren<TMP_Text>();

        //select level
        Debug.Log(m_Text.text.ToString());
    }
}
