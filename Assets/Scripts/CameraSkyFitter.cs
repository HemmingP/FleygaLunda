using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class CameraSkyFitter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera targetCamera;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetCamera = transform.parent != null ? transform.parent.GetComponent<Camera>() : Camera.main;

        if (targetCamera == null)
            targetCamera = Camera.main;

        FitToCamera();
    }

    private void LateUpdate()
    {
        // Re-fit in editor or runtime if resolution/aspect ratio changes
        FitToCamera();
    }

    public void FitToCamera()
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null || targetCamera == null)
            return;

        // Reset scale to calculate native dimensions
        transform.localScale = Vector3.one;

        // Get camera dimensions in world units
        float cameraHeight = targetCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * targetCamera.aspect;

        // Get sprite dimensions in world units
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Calculate scale multipliers needed to cover the entire view
        float scaleX = cameraWidth / spriteSize.x;
        float scaleY = cameraHeight / spriteSize.y;

        // Use maximum scale if you want to preserve aspect ratio without stretching, 
        // or set scale directly to match bounds exactly.
        float maxScale = Mathf.Max(scaleX, scaleY);
        transform.localScale = new Vector3(maxScale, maxScale, 1f);

        // Keep local position centered on camera lens
        transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);
    }
}