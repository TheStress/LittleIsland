using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockInteractable : Interactable
{
    [SerializeField] float liftHeight;
    [SerializeField] Animator animator;
    [HideInInspector] public UnityEvent OnClicked = new UnityEvent();

    bool canLift = true;

    private void CompleteLiftAnimation() {
        canLift = true;
    }
    public override void ClickedOn(RaycastHit hit) {
        // Starting lift
        if(canLift) {
            canLift = false;
            OnClicked.Invoke();
            animator.SetTrigger("StartLift");
        }
    }
}
