using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTwistOffInteractable : Interactable
{
    // Set Values
    [SerializeField] float addRotSpeed;
    [SerializeField] float dampAmount;
    [SerializeField] float dampRate;
    [SerializeField] float popOffHeight;
    [SerializeField] float angleForPopOff;
    [SerializeField] Transform source;


    [SerializeField] Transform poppOffDir;
    [SerializeField] float popOffForce;
    [SerializeField] float popOffOffset;
    [SerializeField] GameObject looseLeaves;

    // Runtime Values
    float rotSpeed;
    float currentRotation;
    float dampTimer;
    Vector3 initalPos;
    bool poppedOff = false;

    private void Start() {
        initalPos = source.position;
    }

    private void Update() {
        if (!poppedOff) {
            RotateUpdate();
            HeightUpdate();
            DampUpdate();
        }
    }

    public void ApplyEffect(Vector3 direction, Vector3 pointOfContact) {

        // Take tangent direction and apply a rotation force
        Vector3 dirToPoint = pointOfContact - source.position;
        dirToPoint = new Vector3(dirToPoint.x, 0, dirToPoint.z).normalized; // Emilinating y component + normalizing
        dirToPoint = Quaternion.AngleAxis(-90, Vector3.up) * dirToPoint;

        // Calculating rotation direction
        int rotSign = (int)Mathf.Sign(Vector3.Dot(direction.normalized, dirToPoint)); // Calculating sign of rotation
        if (rotSign == 0) { rotSign = 1; } // Dealing with the 0 edge case

        // Adding values
        rotSpeed += addRotSpeed * rotSign;
    }
    private void RotateUpdate() {
        currentRotation += rotSpeed*Time.deltaTime; // Adding rotation
        currentRotation = Mathf.Max(0, currentRotation); // Setting floor limit of 0
        source.rotation = Quaternion.Euler(0, 360-(currentRotation%360), 0); // Applying values, additional calculations for reversing direction
    }

    private void HeightUpdate() {
        float twistVal = Mathf.InverseLerp(0, angleForPopOff, currentRotation);
        Vector3 heightOffset = Vector3.up * popOffHeight * twistVal;
        source.position = initalPos + heightOffset;

        // If the object is fully twisted off popp of
        if(twistVal >= 1) {
            PopOff();
        }
    }

    private void DampUpdate() {
        dampTimer -= Time.deltaTime;
        if (dampTimer <= 0f) {
            // Damping values
            rotSpeed = Mathf.Lerp(rotSpeed, 0, dampAmount);
            dampTimer = dampRate;
        }
    }

    private void PopOff() {
        // Initalizing Values
        poppedOff = true;

        // Adding force to rigid body
        GameObject newLeaves = Instantiate(looseLeaves, transform.position, transform.rotation);

        // Calculations
        Vector3 appliedPopOffForce = (poppOffDir.position - transform.position).normalized * popOffForce;
        Vector3 appliedPopOffOffset = -(new Vector3(appliedPopOffForce.x, 0, appliedPopOffForce.z)).normalized*popOffOffset;

        // Applying force
        Rigidbody rb = newLeaves.GetComponent<Rigidbody>();
        rb.AddForceAtPosition(appliedPopOffForce, transform.position+appliedPopOffOffset, ForceMode.VelocityChange);

        Destroy(gameObject);
    }

    public override void ClickedOn(RaycastHit hit) {
        if (!poppedOff) {
            if(rotSpeed < 0 && currentRotation <= 0) { rotSpeed = 0; } // If hitting limit reset the speed
            ApplyEffect(hit.point - Camera.main.transform.position, hit.point); // setting direction from the camera perspective to point of contact
        }
    }
}
