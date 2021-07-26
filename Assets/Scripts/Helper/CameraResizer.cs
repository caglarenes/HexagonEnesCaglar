using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : Singleton<CameraResizer>
{
    public float width = 8;
    public float height = 9;
    public void ResizeCamera()
    {
        width = HexagonGeometry.CalculateHorizontalSize();
        height = HexagonGeometry.CalculateVerticalSize();

        transform.position = new Vector3(((width) - (3f / 4f)) / 2f, ((height) - 1) / 2f, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;

        float verticalSize = ((height + 0.5f) / 2f) * aspectRatio;

        float horizontalSize = ((width + 0.5f) / 2f) / aspectRatio;

        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
    }

    void Start()
    {
        ResizeCamera();
    }
}
