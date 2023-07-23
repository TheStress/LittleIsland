using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractible : Interactible
{
    private bool startOpen = false;
    private Transform chestLid;
    private float lidRotTarget = 200f;

    private BoxCollider boxCollider;
    private void Start()
    {
        chestLid = transform.GetChild(0);
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if(startOpen && chestLid.rotation.eulerAngles.x < lidRotTarget-20f)
        {
            float newX = Mathf.Lerp(chestLid.rotation.eulerAngles.x, lidRotTarget, 0.1f);
            float y = chestLid.rotation.eulerAngles.y;
            float z = chestLid.rotation.eulerAngles.z;
            chestLid.rotation = Quaternion.Euler(newX, y, z);
        }
    }
    public override void ClickedOn()
    {
        Debug.Log(gameObject.name + " clicked on");
        Destroy(boxCollider);
        startOpen = true;
    }
}
