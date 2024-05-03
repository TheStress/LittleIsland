using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroySelf : MonoBehaviour
{
    ParticleSystem part;
    private void Start() {
        part = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if(part.isStopped) {
            Destroy(gameObject);
        }
    }
}
