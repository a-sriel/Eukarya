using UnityEngine;
using System.IO;

public class PortraitCapture : MonoBehaviour
{
    public int width = 512;
    public int height = 512;
    public string fileName = "portrait";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Capture();
    }

    void Capture()
    {
        Camera cam = GetComponent<Camera>();
        RenderTexture rt = new RenderTexture(width, height, 32, RenderTextureFormat.ARGB32);
        rt.antiAliasing = 4;
        cam.targetTexture = rt;
        cam.Render();

        RenderTexture.active = rt;
        Texture2D image = new Texture2D(width, height, TextureFormat.ARGB32, false);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "Downloads", fileName + ".png");
        File.WriteAllBytes(path, image.EncodeToPNG());
        Debug.Log("Saved to " + path);
    }
}
