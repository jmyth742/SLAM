using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuController : MonoBehaviour
{
    void Start()
    {
        HideHelp();
    }

    public void Help()
    {
        this.gameObject.SetActive(true);
    }

    public void HideHelp()
    {
        this.gameObject.SetActive(false);
    }
}
