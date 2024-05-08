using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    [SerializeField] AnimationCurve popEffectCurve;
    [SerializeField] float popEffectDuration;
    [SerializeField] AudioSource dingSound;
    [SerializeField] Animator animator = null;

    float popEffectTimer;
    bool clickedOn = false;

    private void Update() {
        if (clickedOn) {
            PopEffectUpdate();
        }
        if(TryGetComponent(out Animator anim)) {
            animator = anim;
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


            // Playing sound
            if (dingSound.isPlaying) {
                dingSound.Stop();
            }
            dingSound.Play();
            if(animator != null) {
                animator.speed = 0;
            }
        }
    }
}
