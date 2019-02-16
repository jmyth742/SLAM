using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideMenuController : MonoBehaviour {

    public GameObject mainPanel;

    private TMP_Text[] slides;
    private int slideN;

    private void Start()
    {
        slides = mainPanel.GetComponentsInChildren<TMP_Text>();
        slideN = 0;

        slideN = showMenuItemCurrent(0);
    }

    public void showMenuItem(int slideNumber)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
        }

        slides[slideNumber].gameObject.SetActive(true);
    }

    public int showMenuItemCurrent(int slideNumber)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
        }

        slides[slideNumber].gameObject.SetActive(true);
        return slideNumber;
    }

    public void Next()
    {
        if (slideN<slides.Length-1)
        {
            slideN = showMenuItemCurrent(slideN+1);
        }

    }

    public void Back()
    {
        if (slideN > 0)
        {
            slideN = showMenuItemCurrent(slideN - 1);
        }
        
    }

}
