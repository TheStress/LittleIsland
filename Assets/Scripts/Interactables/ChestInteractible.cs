using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractible : Interactable {

    [SerializeField] Transform chestLid;
    private bool open = false;
    private float lidRotTarget = 0f;

    private void Update() {
        float newX = Mathf.Lerp(chestLid.rotation.eulerAngles.x, lidRotTarget, 0.1f);
        float y = chestLid.rotation.eulerAngles.y;
        float z = chestLid.rotation.eulerAngles.z;

        chestLid.eulerAngles = new Vector3(newX, y, z);
    }

    public override void ClickedOn(RaycastHit hit) {
        if(open) {
            // Setting to close
            open = false;
            lidRotTarget = 0;
        }
        else {
            open = true;
            lidRotTarget = 90f;
        }
    }
}
