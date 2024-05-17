using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    [SerializeField] private Vector3 offset;
    GameObject obstacle;
    Vector3 rayDirection;

    private void Start()
    {
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
            CheckObtacle();
        }

    }
    void CheckObtacle()
    {
        rayDirection = target.transform.position - Camera.main.transform.position;
        rayDirection.Normalize();
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out RaycastHit hit))
        {
            Debug.DrawRay(Camera.main.transform.position, rayDirection * 100f, Color.green);
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                obstacle = hit.collider.gameObject;
                obstacle.GetComponent<MaterialChange>().GetBlur();
            }
            else if (obstacle != null)
            {
                obstacle.GetComponent<MaterialChange>().GetNormal();
            }
            else { return; }
        }


    }

}
