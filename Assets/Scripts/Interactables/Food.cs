using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactible
{
    public override void ClickedOn()
    {
        Debug.Log(gameObject.name + " clicked on");
        FindObjectOfType<ListManager>().FoundFood(gameObject.name);
        Destroy(gameObject);
    }
}
