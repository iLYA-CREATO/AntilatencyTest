using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum ViewMode { Room, Outside }
    public ViewMode currentViewMode = ViewMode.Room;

    [Header("Room View")]
    public Transform[] roomPositions; // Позиции камеры в каждой комнате
    public float roomMoveSpeed = 5f;
    public float roomRotationSpeed = 2f;
    private int currentRoomIndex = 0;

    [Header("Outside View")]
    public Transform outsideViewPosition; // Позиция камеры снаружи квартиры
    public float outsideMoveSpeed = 10f;
    public float outsideRotationSpeed = 3f;
    public float outsideZoomSpeed = 5f;
    public float minZoomDistance = 10f;
    public float maxZoomDistance = 30f;
    private float currentZoomDistance;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Start()
    {
        currentZoomDistance = Vector3.Distance(transform.position, outsideViewPosition.position);
        if (currentZoomDistance < minZoomDistance) currentZoomDistance = minZoomDistance;
        if (currentZoomDistance > maxZoomDistance) currentZoomDistance = maxZoomDistance;

        SwitchViewMode(currentViewMode);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchViewMode(currentViewMode == ViewMode.Room ? ViewMode.Outside : ViewMode.Room);
        }

        if (currentViewMode == ViewMode.Room)
        {
            HandleRoomView();
        }
        else if (currentViewMode == ViewMode.Outside)
        {
            HandleOutsideView();
        }

        if (currentViewMode == ViewMode.Room)
        {
            HandleRoomSelection();
        }
    }

    private void SwitchViewMode(ViewMode newViewMode)
    {
        currentViewMode = newViewMode;

        if (currentViewMode == ViewMode.Room)
        {
            if (roomPositions.Length > 0)
            {
                transform.position = roomPositions[currentRoomIndex].position;
                transform.rotation = roomPositions[currentRoomIndex].rotation;
            }
        }
        else if (currentViewMode == ViewMode.Outside)
        {
            transform.position = outsideViewPosition.position;
            transform.rotation = outsideViewPosition.rotation;
        }
    }

    private void HandleRoomView()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += moveDirection * roomMoveSpeed * Time.deltaTime;

        if (Input.GetMouseButton(1)) 
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * roomRotationSpeed;
            pitch -= mouseY * roomRotationSpeed;

            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }

    private void HandleOutsideView()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += moveDirection * outsideMoveSpeed * Time.deltaTime;

        if (Input.GetMouseButton(1)) 
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * outsideRotationSpeed;
            pitch -= mouseY * outsideRotationSpeed;

            // Ограничиваем угол наклона камеры
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }

        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoomDistance -= zoomInput * outsideZoomSpeed;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

        transform.position = outsideViewPosition.position - transform.forward * currentZoomDistance;
    }

    private void HandleRoomSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && roomPositions.Length > 0)
        {
            currentRoomIndex = 0;
            SwitchViewMode(ViewMode.Room);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && roomPositions.Length > 1)
        {
            currentRoomIndex = 1;
            SwitchViewMode(ViewMode.Room);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && roomPositions.Length > 2)
        {
            currentRoomIndex = 2;
            SwitchViewMode(ViewMode.Room);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && roomPositions.Length > 3)
        {
            currentRoomIndex = 3;
            SwitchViewMode(ViewMode.Room);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && roomPositions.Length > 4)
        {
            currentRoomIndex = 4;
            SwitchViewMode(ViewMode.Room);
        }
    }
}