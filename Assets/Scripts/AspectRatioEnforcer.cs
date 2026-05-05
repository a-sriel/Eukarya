using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectRatioEnforcer : MonoBehaviour
{
    public float targetAspect = 16f / 9f;

    void Start()
    {
        Apply();
    }

    void Update()
    {
        Apply();
    }

    void Apply()
    {
        Camera cam = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            // Window too tall — add letterbox (top/bottom)
            Rect r = cam.rect;
            r.width = 1f;
            r.height = scaleHeight;
            r.x = 0f;
            r.y = (1f - scaleHeight) / 2f;
            cam.rect = r;
        }
        else
        {
            // Window too wide — add pillarbox (left/right)
            float scaleWidth = 1f / scaleHeight;
            Rect r = cam.rect;
            r.width = scaleWidth;
            r.height = 1f;
            r.x = (1f - scaleWidth) / 2f;
            r.y = 0f;
            cam.rect = r;
        }
    }
}
