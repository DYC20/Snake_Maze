using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float foodLifetime = 3f;

    public float FoodLifetime => foodLifetime;

    private Camera mainCamera;
    private GameObject food;
    //private GameObject background;
    private float foodTimer;

    public float FoodTimer
    {
        get => foodTimer;
        set => foodTimer = value;
    }
    private Vector2Int posGridmin;
    private Vector2Int posGridmax;
    private Vector2Int foodGrid;
    private Vector2Int foodGridPos;
    public Vector2Int FoodGridPos => foodGridPos;


    private void Start()
    {
        mainCamera = Camera.main;
        //background = GameRef.instance.Background;
        CreateGrid();
        SpawnFood();
    }

    private void Update()
    {
        FoodCycle();
    }
    private void SpawnFood()
     {
         foodGridPos = new Vector2Int(Random.Range(posGridmin.x, posGridmax.x), Random.Range(posGridmin.y, posGridmax.y));
         food = Instantiate(foodPrefab, new Vector3(foodGridPos.x, foodGridPos.y), Quaternion.identity);
     }

    private void FoodCycle()
    {
        foodTimer += Time.deltaTime;
        if (foodTimer >= foodLifetime)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        Destroy(food);
        SpawnFood();
        foodTimer = 0;
    }

    private void CreateGrid()
    {
        //Find Vector2 bounds
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, -mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, -mainCamera.transform.position.z));
        
        posGridmax = new Vector2Int(Mathf.FloorToInt(topRight.x - 0.5f), Mathf.FloorToInt(topRight.y - 0.5f));
        posGridmin = new Vector2Int(Mathf.CeilToInt(bottomLeft.x + 0.5f), Mathf.CeilToInt(bottomLeft.y + 0.5f));

    }
  
    //Helper | Written with AI
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 center = new Vector3(
            (posGridmin.x + posGridmax.x) / 2f,
            (posGridmin.y + posGridmax.y) / 2f,
            0f
        );

        Vector3 size = new Vector3(
            posGridmax.x - posGridmin.x,
            posGridmax.y - posGridmin.y,
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
}
