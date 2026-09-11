using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject indicatorprefab;
    [SerializeField] private float indicatorDuration;
    [SerializeField] private List<GameObject> wallList = new List<GameObject>();
    [SerializeField] private float wallSpawnTime = 3f;
    [SerializeField] private Vector2Int endPoindOffset;
    [SerializeField, Range(0f, 0.2f)] private float indicatorEdgePadding = 0.05f;
    
    private Camera mainCamera;
    private Vector2Int posMax;
    private Vector2Int posMin;
    private GameObject wall;
    private GameObject indicator;
    private float wallTimer;
    private Vector2Int wallPos;
    private Quaternion wallRotation;
    private bool isSpawning;
    private Vector3 indicatorSpawnPosition;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        CalculateBounds();
        transform.position = new Vector3(Mathf.Ceil(transform.position.x),
            Mathf.Ceil(transform.position.y), transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        WallCycle();
    }

    private void WallCycle()
    {
        wallTimer += Time.deltaTime;
        if (wallTimer >= wallSpawnTime)
        {
            SpawnWall();
        }
    }

    private void SpawnWall()
    {
        if (!isSpawning)
            StartCoroutine(SpawnWallSeq());
    }

    private IEnumerator SpawnWallSeq()
    {
        isSpawning = true;
        
        GameObject wallToSpawn = wallList[Random.Range(0, wallList.Count)];
        wallRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        wallPos = new Vector2Int(Random.Range(posMin.x, posMax.x), Random.Range(posMin.y, posMax.y));
        Vector3 wallSpawnPos = new Vector3(wallPos.x, wallPos.y);

        GetIndicatorPosition(wallSpawnPos);
            
       indicator = Instantiate(
            indicatorprefab,
            indicatorSpawnPosition,
            Quaternion.identity);
        
        yield return new WaitForSeconds(indicatorDuration);

        Destroy(indicator);
        //indicator.GetComponent<IndicatorManager>().DestroyIndicator;
        
        wall = Instantiate(wallToSpawn, new Vector3(wallPos.x, wallPos.y), wallRotation);
        isSpawning = false;
        wallTimer = 0;
    }

    //Writen with AI starts
    private void CalculateBounds()
    {
        Vector2Int start = new Vector2Int(
            Mathf.CeilToInt(transform.position.x),
            Mathf.CeilToInt(transform.position.y)
        );

        Vector2Int end = start + endPoindOffset;

        posMin = Vector2Int.Min(start, end);
        posMax = Vector2Int.Max(start, end);
    }
    private Vector3 GetIndicatorPosition(Vector3 wallSpawnPosition)
    {
        Vector3 viewportPosition =
            mainCamera.WorldToViewportPoint(wallSpawnPosition);

        viewportPosition.x = Mathf.Clamp(
            viewportPosition.x,
            indicatorEdgePadding,
            1f - indicatorEdgePadding
        );

        viewportPosition.y = Mathf.Clamp(
            viewportPosition.y,
            indicatorEdgePadding,
            1f - indicatorEdgePadding
        );

        float distanceToGamePlane =
            -mainCamera.transform.position.z;

        viewportPosition.z = distanceToGamePlane;

        return indicatorSpawnPosition = mainCamera.ViewportToWorldPoint(viewportPosition);
    }
    //Writen with AI ends
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 from = new Vector3(
            Mathf.CeilToInt(transform.position.x),
            Mathf.CeilToInt(transform.position.y),
            transform.position.z
        );

        Vector3 to = from + new Vector3(
            endPoindOffset.x,
            endPoindOffset.y,
            0f
        );

        Gizmos.DrawLine(from, to);
    }
    
}
