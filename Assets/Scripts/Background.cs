using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2Int screenBounds;
    
    public Vector2Int ScreenBounds => screenBounds;

    private void Awake()
    {
        GetScreenBounds();
    }

    private void GetScreenBounds()
    {
        mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = mainCamera.aspect * cameraHeight;
        
        //Set BG scale &position
        transform.localScale = new Vector3(cameraWidth, cameraHeight, 1f);
        transform.transform.position = new Vector3(cameraWidth / 2f - cameraWidth / 2f, cameraHeight / 2f - cameraHeight /2f, 0f);
    }

}
