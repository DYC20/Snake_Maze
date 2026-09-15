using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [Header("General")]
    private bool gameStarted = false;
    public bool GameStarted {get => gameStarted; set => gameStarted = value; }
    private Camera mainCamera;
    
    [Header("Wall")]
    [SerializeField] private List<GameObject> wallPrefabList = new List<GameObject>();
    [SerializeField] private float WallsLLifetime = 5f;
    private GameObject wall;
    private Vector2Int wallPos;
    private Quaternion wallRotation;    
    private List<GameObject> liveWallsList = new List<GameObject>();
    public List<GameObject> LiveWallsList => liveWallsList; 
    
    [Header("Wall Spawner")]
    [SerializeField] private bool notSpawning = false;
    [SerializeField] private float wallSpawnTime = 3f;
    [SerializeField] private Vector2Int endPoindOffset;
    private Vector2Int posMax;
    private Vector2Int posMin;
    private float wallTimer;
    private bool isSpawningSeq;
    public float WallSpawnTime{get => wallSpawnTime; set => wallSpawnTime = value;}
    
    [Header("Indicator")]
    [SerializeField] private GameObject indicatorprefab;
    [SerializeField] private float indicatorDuration;
    [SerializeField, Range(0f, 0.2f)] private float indicatorEdgePadding = 0.05f;
    private GameObject indicator;
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
        if (gameStarted && !notSpawning)
            WallCycle();
    }

    private void WallCycle()
    {
        wallTimer += Time.deltaTime;
        if (wallTimer >= wallSpawnTime)
        {
            SpawnWall();
        }
        if (wallTimer >= WallsLLifetime + wallSpawnTime)
            DestroySingleWall();
    }

    private void DestroySingleWall()
    {
        GameObject wallToDestroy;
        wallToDestroy = LiveWallsList.Last();
        LiveWallsList.Remove(wallToDestroy);
        Destroy(wallToDestroy);
    }

    private void SpawnWall()
    {
        if (!isSpawningSeq)
            StartCoroutine(SpawnWallSeq());
    }

    private IEnumerator SpawnWallSeq()
    {
        isSpawningSeq = true;
        
        GameObject wallToSpawn = wallPrefabList[Random.Range(0, wallPrefabList.Count)];
        wallRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        wallPos = new Vector2Int(Random.Range(posMin.x, posMax.x), Random.Range(posMin.y, posMax.y));
        Vector3 wallSpawnPos = new Vector3(wallPos.x, wallPos.y);

        GetIndicatorPosition(wallSpawnPos);
            
       indicator = Instantiate(
            indicatorprefab,
            indicatorSpawnPosition,
            Quaternion.Euler(0, 0, 180));
        
        yield return new WaitForSeconds(indicatorDuration);

        Destroy(indicator);
        //indicator.GetComponent<IndicatorManager>().DestroyIndicator;
        
        wall = Instantiate(wallToSpawn, new Vector3(wallPos.x, wallPos.y), wallRotation);
        
        liveWallsList.Add(wall);
        Debug.Log($"New Walls Count: {liveWallsList.Count} ");
        
        isSpawningSeq = false;
        wallTimer = 0;
    }

    public void DestroyAllWalls()
    {
        Debug.Log($"Destroying {liveWallsList.Count} walls");
        for (int i = liveWallsList.Count - 1; i >= 0; i--)
        {
            GameObject wallToDestroy = liveWallsList[i];
            liveWallsList.Remove(wallToDestroy);
            Destroy(wallToDestroy);
        }
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
