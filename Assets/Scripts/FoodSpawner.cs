using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float foodLifetime = 3f;

    public float FoodLifetime => foodLifetime;

    //private Camera mainCamera;
    private GameObject food;
    //private GameObject background;
    private float foodTimer;
    private GameRef gameRef;

    public float FoodTimer
    {
        get => foodTimer;
        set => foodTimer = value;
    }
    private Vector2Int posGridmin;
    private Vector2Int posGridmax;
    private Vector2Int minBounds;
    private Vector2Int maxBounds;
    private Vector2Int foodGridPos;
    public Vector2Int FoodGridPos => foodGridPos;


    private void Start()
    {
        gameRef = GameRef.instance;
        maxBounds = gameRef.BoardGrid.GetComponent<BoardGrid>().Max;
        minBounds = gameRef.BoardGrid.GetComponent<BoardGrid>().Min;
        

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
        posGridmax = new Vector2Int(Mathf.FloorToInt(maxBounds.x - 0.5f), Mathf.FloorToInt(maxBounds.y - 0.5f));
        posGridmin = new Vector2Int(Mathf.CeilToInt(minBounds.x + 0.5f), Mathf.CeilToInt(minBounds.y + 0.5f));
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
