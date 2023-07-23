using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    private GameObject helpScreen;

    public void ClickButton()
    {
        if(helpScreen.active)
        {
            helpScreen.SetActive(false);
        }
        else
        {
            helpScreen.SetActive(true);
        }
    }
}
