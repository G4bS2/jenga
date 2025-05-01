using UnityEngine;
using UnityEngine.UIElements;

public class CameraOrbit : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -10);

    [Header("Rotation Settings")]
    public float rotationSpeed;
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    private float currentDistance;

    private void Start()
    {
        if (target == null)
        {
            GameObject pivot = new GameObject("Camera Pivot");
                pivot.transform.position = Vector3.zero;
        }
        currentDistance = offset.magnitude;
        transform.position = target.position + offset;
        transform.LookAt(target);

    }
    
    void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion camTurAngle = Quaternion.Euler(mouseY, mouseX, 0);
            offset = camTurAngle * offset;
        }

        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minZoom, maxZoom);
    }
}
