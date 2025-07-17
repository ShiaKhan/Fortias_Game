using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_Controller : MonoBehaviour
{
    public Image hpBar;
    public Image mpBar;
    public GameObject kingView;
    private void Awake()
    {
        hpBar.fillAmount = 0;
        mpBar.fillAmount = 0;
    }
}
