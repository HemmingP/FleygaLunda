using UnityEngine;

public class MainCameraFollower : MonoBehaviour
{
    [Range(0f, 1f)]
    public float followXPercentage = 0.5f;
    [Range(0f, 1f)]
    public float followYPercentage = 0.5f;

    [SerializeField] private Camera targetCamera;
    private Vector3 initialObjectPosition;
    private Vector3 initialCameraPosition;
    private bool isInitialized = false;

    void Awake()
    {
        initialObjectPosition = transform.position;
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (targetCamera != null)
        {
            initialCameraPosition = targetCamera.transform.position;
            isInitialized = true;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isInitialized)
        {
            Initialize();
            if (!isInitialized) return;
        }

        Vector3 cameraOffset = targetCamera.transform.position - initialCameraPosition;

        transform.position = new Vector3(
            initialObjectPosition.x + cameraOffset.x * followXPercentage,
            initialObjectPosition.y + cameraOffset.y * followYPercentage,
            initialObjectPosition.z
        );
    }
}
