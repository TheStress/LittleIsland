using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] float distanceForDestruction;
    [SerializeField] float lifeTime;

    private void Update() {
        // Life duration
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0) { Destroy(gameObject); }
        
        // Destroying when getting too far
        if(transform.position.magnitude >= distanceForDestruction) { Destroy(gameObject); }
    }

    private void OnCollisionEnter(Collision collision) {
        // Detecting, and triggering collision
        if(collision.gameObject.TryGetComponent<Ship>(out Ship ship)) {
            ship.CannonBallCollision();
            Destroy(gameObject);
        }
    }
}
