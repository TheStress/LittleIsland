using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    [SerializeField] AnimationCurve popEffectCurve;
    [SerializeField] float popEffectDuration;

    float popEffectTimer;
    bool clickedOn = false;

    private void Update() {
        if (clickedOn) {
            PopEffectUpdate();
        }
    }

    private void PopEffectUpdate() {
        popEffectTimer += Time.deltaTime;
        transform.localScale = Vector3.one * (1-popEffectCurve.Evaluate(Mathf.InverseLerp(0, popEffectDuration, popEffectTimer))); // Applying scale up effect

        if (popEffectTimer >= popEffectDuration) {
            // Disabling game object when effect is done
            gameObject.SetActive(false);
        }
    }

    public override void ClickedOn(RaycastHit hit)
    {
        if(!clickedOn) {
            clickedOn = true;
            FindObjectOfType<ListManager>().FoundFood(gameObject.name);
            popEffectTimer = 0;
        }
    }
}
