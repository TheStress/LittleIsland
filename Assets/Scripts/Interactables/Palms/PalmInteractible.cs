using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmInteractible : Interactable
{
    // Set Values
    [Range(0,90)]
    [SerializeField] float maxAmplitudeAngle;
    [SerializeField] float addAmplitudeAngle;
    [SerializeField] float addRotSpeed;
    [SerializeField] float addOcilationSpeed;
    [SerializeField] float dampRate;
    [Range(0,1)]
    [SerializeField] float dampAmount;
    [Range(0, 1)]
    [SerializeField] float dampRotFactor;
    [Range(0, 1)]
    [SerializeField] float dampOcilationFactor;
    [Range(0, 1)]
    [SerializeField] float dampAmplitudeFactor;

    [SerializeField] Transform source;

    // Fruit
    [SerializeField] GameObject fruitObject;
    [SerializeField] float fruitDropSpeed;
    bool fruitDropped = false;


    // Current Values
    float rotSpeed;
    float ocilationSpeed;
    float amplitudeAngle;
    float timer;
    float dampTimer;
    Vector3 ocilationDir = Vector3.zero;

    private void Update() {
        // Dropping fruit
        if(!fruitDropped) {
            if(ocilationSpeed > fruitDropSpeed) {
                fruitDropped = true;
                fruitObject.SetActive(true);
            }
        }

        // Rotating about y axis based on rot speed
        RotateUpdate();

        // Update occilation based on values
        OcilationUpdate();

        // Updating any values for dampening
        DampUpdate();
    }

    public void ApplyEffect(Vector3 direction, Vector3 point) {
        // Initalizing values
        timer = 0;

        // Take tangent direction and apply a rotation force
        ocilationDir = point - source.position;
        ocilationDir = new Vector3(ocilationDir.x, 0, ocilationDir.z).normalized; // Emilinating y component + normalizing
        ocilationDir = Quaternion.AngleAxis(-90, Vector3.up) * ocilationDir;
        Debug.DrawLine(source.transform.position, source.transform.position+ocilationDir, Color.red, 5f);

        // Calculating rotation direction
        int rotSign = -(int)Mathf.Sign(Vector3.Dot(direction.normalized, ocilationDir)); // Calculating sign of rotation
        if (rotSign == 0) { rotSign = 1; } // Dealing with the 0 edge case

        // Adding values
        rotSpeed += addRotSpeed*rotSign;
        ocilationSpeed += addOcilationSpeed;
        amplitudeAngle = Mathf.Min(amplitudeAngle+addAmplitudeAngle, maxAmplitudeAngle); // Adding amplitude limited by max
    }

    private void OcilationUpdate() {
        // Calculating ocilation values
        timer += Time.deltaTime;
        float currentAmplitudeAngle = amplitudeAngle * -Mathf.Sin(timer* ocilationSpeed*Mathf.Deg2Rad); // Calculating amplitude, set sine to negative so when time=0 it starts going down

        // Calculating rotation values
        Quaternion currentRotation = Quaternion.Euler(0, 0, 0);
        Quaternion ocilationOffset = Quaternion.AngleAxis(currentAmplitudeAngle, ocilationDir);

        source.rotation = currentRotation * ocilationOffset;
    }

    private void RotateUpdate() {
        // Calculating update offset
        Quaternion rotUpdate = Quaternion.AngleAxis(rotSpeed*Time.deltaTime, Vector3.up);

        // Applying values
        //source.rotation = rotUpdate * source.rotation;
        ocilationDir = rotUpdate * ocilationDir;
    }

    private void DampUpdate() {
        dampTimer -= Time.deltaTime;
        if(dampTimer <= 0f) {
            // Damping values
            rotSpeed = Mathf.Lerp(rotSpeed, 0, dampAmount*dampRotFactor);
            ocilationSpeed = Mathf.Lerp(ocilationSpeed, 0, dampAmount*dampOcilationFactor);
            amplitudeAngle = Mathf.Lerp(amplitudeAngle, 0, dampAmount*dampAmplitudeFactor);
            dampTimer = dampRate;
        }
    }

    public override void ClickedOn(RaycastHit hit) {
        ApplyEffect(hit.point - Camera.main.transform.position, hit.point); // setting direction from the camera perspective to point of contact
    }
}
