using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInteractable : Interactable
{
    // set values
    [SerializeField] GameObject cannonBall;
    [SerializeField] Transform launchPoint;
    [SerializeField] Transform barrel;
    [SerializeField] float force;

    public override void ClickedOn(RaycastHit hit) {
        // Creating object
        GameObject currentCannonBall = Instantiate(cannonBall, launchPoint.position, Quaternion.identity);
        
        // Calculating values
        Vector3 launchDir = (launchPoint.position - barrel.position).normalized;
        Vector3 forceApplied = launchDir * force;

        // Applying values
        currentCannonBall.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.VelocityChange);
    }
}
