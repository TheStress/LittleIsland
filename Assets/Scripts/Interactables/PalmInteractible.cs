using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmInteractible : Interactible
{
    Vector3 originalPos;

    public float shakeFallOffRate;
    public float maxShakeIntensity;
    private float currentShakeIntensity;

    public float swayFallOffRate;
    public float maxSwayIntensity;
    private float currentSwayIntensity;
    private Vector3 swayDirection;


    private void Start()
    {
        originalPos = transform.position;
    }
    private void Update()
    {
        if (currentShakeIntensity > 0f) {
            currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, 0, shakeFallOffRate);
            //transform.position = Juice.Shake(originalPos, currentShakeIntensity);
            //transform.parent.eulerAngles = Juice.Shake(Vector3.zero, currentShakeIntensity*10f);

            currentSwayIntensity = Mathf.Lerp(currentSwayIntensity, 0, shakeFallOffRate);
            transform.parent.rotation = Quaternion.AngleAxis(currentSwayIntensity, swayDirection);

        }
        
    }
    public override void ClickedOn()
    {
        currentShakeIntensity = maxShakeIntensity;
        currentSwayIntensity = maxSwayIntensity;
        Vector2 newSwayDirection = Random.insideUnitCircle;
        swayDirection = new Vector3(newSwayDirection.x, 0, newSwayDirection.y);
    }
}
