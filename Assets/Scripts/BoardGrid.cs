using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    public Vector2Int Min{get; private set;}
    public Vector2Int Max{get; private set;}
    private Vector3 center = Vector3.zero;
    public Vector3 Center => center;
    
    
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
           Debug.Log("MainCamera is null"); return; 
        }
        CalculateGrid();
        FindCenter();
    }

    private void CalculateGrid()
    {
        //Written with AI Start
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, -mainCamera.transform.position.z)
        );

        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, -mainCamera.transform.position.z)
        );

        Min = new Vector2Int(
            Mathf.CeilToInt(bottomLeft.x),
            Mathf.CeilToInt(bottomLeft.y)
        );

        Max = new Vector2Int(
            Mathf.FloorToInt(topRight.x),
            Mathf.FloorToInt(topRight.y)
        );
    }

    private void FindCenter()
    {
        center = new Vector3(
            (Min.x + Max.x) / 2f,
            (Min.y + Max.y) / 2f,
            0f);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 size = new Vector3(
            Max.x - Min.x,
            Max.y - Min.y,
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
}
