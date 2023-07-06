using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BaseButton : CusMonoBehaviour
{
    [Header("Base Button")]
    [SerializeField]
    protected Button button;

    protected override void Start()
    {
        base.Start();
        this.AddOnclickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }

    private void LoadButton()
    {
        if(this.button == null) return;

        this.button = GetComponent<Button>();
    }

    protected virtual void AddOnclickEvent()
    {
        this.button.onClick.AddListener(this.onClick);
    }

    protected abstract void onClick();
}
