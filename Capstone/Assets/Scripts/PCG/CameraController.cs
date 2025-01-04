using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Pan Settings")]
    [SerializeField] private float panSpeed = 0.5f;
    [Tooltip("Mouse button to use for dragging the camera (0 = Left, 1 = Right, 2 = Middle)")]
    [SerializeField] private int panMouseButton = 2;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;

    private Camera cam;

    private Vector3 dragOrigin;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (IsPointerOverUI())
            return;
        
        HandleCameraPan();
        HandleCameraZoom();
    }
    
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        return EventSystem.current.IsPointerOverGameObject();
    }

    private void HandleCameraPan()
    {
        if (Input.GetMouseButtonDown(panMouseButton))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(panMouseButton))
        {
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 difference = cam.ScreenToViewportPoint(dragOrigin - currentMousePos);

            Vector3 right = transform.right;
            right.y = 0f;
            right.Normalize();

            Vector3 forward = transform.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 move = difference.x * right + difference.y * forward;

            transform.position += move * panSpeed;

            dragOrigin = currentMousePos;
        }
    }

    private void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            if (cam.orthographic)
            {
                float newSize = cam.orthographicSize - scroll * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
            }
            else
            {
                Vector3 direction = transform.forward * scroll * zoomSpeed;

                float newFov = cam.fieldOfView - scroll * (zoomSpeed * 2f); 
                cam.fieldOfView = Mathf.Clamp(newFov, 15f, 90f);
            }
        }
    }
}
