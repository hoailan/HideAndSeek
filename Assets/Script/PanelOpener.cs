using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelOpener : MonoBehaviour
{
    public GameObject panel; 
    private Animator panelAnimator;

    private void Start()
    {
        panelAnimator = panel.GetComponent<Animator>();
    }

    public void TogglePanel()
    {
        bool isOpen = panel.activeSelf;
        panel.SetActive(!isOpen);

        if (panelAnimator != null)
        {
            panelAnimator.SetBool("isOpen", !isOpen);
        }
    }
}
