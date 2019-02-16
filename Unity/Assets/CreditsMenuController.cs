using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuController : MonoBehaviour
{
    void Start()
    {
        HideCredits();
    }

    public void Credits()
    {
        this.gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        this.gameObject.SetActive(false);
    }
}
