using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]

public class JenkaPiece : MonoBehaviour
{
    [Header("Drag Settings")]
    public float maxDragDistance = 2f;

    [Header("Push Settings")]
    public float pushForce = 5f;
    public float doubleClickForceMultiplier = 2f;
    public float doubleClickThreshold = 0.3f;

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 startDragPosition;
    private float lastClickTime = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            ApplyPush(pushForce * doubleClickForceMultiplier);
            return;
        }

        ApplyPush(pushForce);

        isDragging = true;
        rb.isKinematic = true;

        Vector3 mousePos = GetMouseWorldPosition();
        offset = transform.position - mousePos;
        startDragPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)return;

        Vector3 mousePos = GetMouseWorldPosition();
        Vector3 targetPos = mousePos + offset;


        Vector3 dragVector = targetPos - startDragPosition;
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        transform.position = startDragPosition + dragVector;
    }

    private void OnMouseUp()
    {
       isDragging = false;
       rb.isKinematic = false;
    }



    private void ApplyPush(float force)
    {
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }


    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        if(plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }
}
