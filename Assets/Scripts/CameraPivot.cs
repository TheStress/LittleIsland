using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    private float disFromPivot;
    private float heightFromPivot;

    [SerializeField]
    private Transform cameraTransform;

    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 horizontalDistance = new Vector3(cameraTransform.position.x, 0, cameraTransform.position.z);
        disFromPivot = horizontalDistance.magnitude;
        heightFromPivot = cameraTransform.position.y;

        SetAngle(angle);
    }

    // Update is called once per frame
    void Update()
    {
        //Testing
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetAngle(angle + 5);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetAngle(angle - 5);
        }
    }

    public void SetAngle(float _angle)
    {
        angle = _angle % 360f;

        //Setting Camera Position
        Vector3 newCamPosition = new Vector3(Mathf.Cos(angle*Mathf.Deg2Rad) * disFromPivot, heightFromPivot, Mathf.Sin(angle*Mathf.Deg2Rad) * disFromPivot);
        cameraTransform.position = newCamPosition;

        //Setting camera rotation
        Vector3 dirToPivot = transform.position - cameraTransform.position;
        cameraTransform.rotation = Quaternion.LookRotation(dirToPivot, Vector3.up);
    }
}
