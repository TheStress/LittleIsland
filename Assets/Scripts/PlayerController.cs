using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

        }
    }

    public void ClickOnScreen(Vector3 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        Debug.Log(ray.origin);
        Debug.Log(ray.direction);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 5f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Interactible interactible;
            if(hit.collider.gameObject.TryGetComponent<Interactible>(out interactible))
            {
                interactible.ClickedOn();
            }
        }
    }
}
