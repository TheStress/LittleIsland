using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject clickParticle;
    [SerializeField] float particleOffset;
    [SerializeField] AudioSource clickSound;

    private float xFollowPosition;
    private float maxCamRotSpeed = 15f;
    private float maxDisForMaxCamRot = 200f;
    private CameraPivot cameraPivot;


    // Start is called before the first frame update
    void Start()
    {
        cameraPivot = FindObjectOfType<CameraPivot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClickOnScreen(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(1))
        {
            xFollowPosition = Input.mousePosition.x;
        }
        if(Input.GetMouseButton(1))
        {
            float disToMouse = xFollowPosition - Input.mousePosition.x;
            float camRotSpeed = maxCamRotSpeed * 
                Mathf.InverseLerp(0, maxDisForMaxCamRot, Mathf.Abs(disToMouse)) * 
                Mathf.Sign(disToMouse);
            xFollowPosition = Mathf.Lerp(xFollowPosition, Input.mousePosition.x, 0.2f);
            
            cameraPivot.SetAngle(cameraPivot.angle + camRotSpeed);
        }
    }

    public void ClickOnScreen(Vector3 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 5f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Spawning and orienting particle
            GameObject particle = Instantiate(clickParticle, hit.point, Quaternion.identity);
            particle.transform.position += particleOffset * hit.normal;
            particle.transform.up = hit.normal;

            // Playing sound
            if(clickSound.isPlaying) {
                clickSound.Stop();
            }
            clickSound.Play();

            // Checking for any interactables
            Interactable interactible;
            if(hit.collider.gameObject.TryGetComponent<Interactable>(out interactible)) {
                // Triggering interactables
                interactible.ClickedOn(hit); 
            }
        }
    }
}
