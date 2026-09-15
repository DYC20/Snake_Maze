using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [Header("food")]
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float foodLifetime = 3f;
    private GameObject food;
    public GameObject Food => food;
    private bool isRespawning = false;
    private float foodTimer;
    private Vector2Int foodPos;
    public Vector2Int FoodPos => foodPos;
    
    [Header("manager")]
    private GameRef gameRef;
    private bool gameStarted = false;
    bool notFirstSpawn = false;
    public bool GameStarted {set => gameStarted = value; }
    public bool NotFirstSpawn  { set => notFirstSpawn = value; }

    [Header("Grid")]    
    [SerializeField] private Vector2Int startGrid;
    [SerializeField] private int gridGrowAmount;
    [SerializeField] private int gridPadding;
    private Vector2Int gridCenter;
    private Vector2Int minBounds;
    private Vector2Int maxBounds;
    private Vector2Int currentGridMin;
    private Vector2Int currentGridMax;
    private void Start()
    {
        gameRef = GameRef.instance;
        maxBounds = new Vector2Int(gameRef.BoardGrid.Max.x
            , gameRef.BoardGrid.Max.y - gridPadding);
        minBounds = new Vector2Int(gameRef.BoardGrid.Min.x
            , gameRef.BoardGrid.Min.y + gridPadding);
        InitializeGrid();
    }
    private void FirstSpawn()
    {
        InitializeGrid();
        SpawnFood();
        notFirstSpawn = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (!notFirstSpawn)
            {
                if(food != null)
                    Destroy(food);
                FirstSpawn();
            }
            FoodCycle();
        }
    }
    private void SpawnFood()
     {
         foodPos = new Vector2Int(Random.Range(currentGridMin.x, currentGridMax.x),
             Random.Range(currentGridMin.y, currentGridMax.y));
         food = Instantiate(foodPrefab, new Vector3(foodPos.x, foodPos.y), Quaternion.identity);
         isRespawning = false;
         GrowGrid();
     }

    private void FoodCycle()
    {
        foodTimer += Time.deltaTime;
        if (foodTimer >= foodLifetime)
        {
            StartCoroutine(Respawn());
        }
    }

    public IEnumerator Respawn()
    {
        if (isRespawning)
            yield break;
        isRespawning = true;
        ParticleSystem deathPS = food.GetComponentInChildren<ParticleSystem>();
        SpriteRenderer foodSpriteRenderer = food.GetComponentInChildren<SpriteRenderer>();
        foodSpriteRenderer.enabled = false;
        deathPS.Play();
        AudioSource audioSource = food.GetComponentInChildren<AudioSource>();
        AudioClip eatSound = food.GetComponentInChildren<Food>().EatSound;
        audioSource.PlayOneShot(eatSound);
        yield return new WaitUntil(() => !deathPS.IsAlive());

        Destroy(food);
        SpawnFood();
        foodTimer = 0;
    }

    private void GrowGrid()
    {
        Vector2Int newMaxBounds = currentGridMax + Vector2Int.one * gridGrowAmount;
        Vector2Int newMinBounds = currentGridMin - Vector2Int.one * gridGrowAmount;
        if (newMaxBounds.x > maxBounds.x || newMaxBounds.y > maxBounds.y ||
            newMaxBounds.x < minBounds.x || newMaxBounds.y < minBounds.y)
        {
            newMaxBounds = maxBounds;
            newMinBounds = minBounds;
        }
        currentGridMax = new Vector2Int(Mathf.FloorToInt(newMaxBounds.x - 0.5f), Mathf.FloorToInt(newMaxBounds.y - 0.5f));
        currentGridMin = new Vector2Int(Mathf.CeilToInt(newMinBounds.x + 0.5f), Mathf.CeilToInt(newMinBounds.y + 0.5f));
    }
    
    private void InitializeGrid()
    {
        gridCenter = new Vector2Int(
            (minBounds.x + maxBounds.x) / 2,
            (minBounds.y + maxBounds.y) / 2
        );

        currentGridMin = gridCenter - startGrid;
        currentGridMax = gridCenter + startGrid;

        //ClampGridToBoard();
    }
  
    //Helper | Written with AI
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = new Vector3(
            (currentGridMin.x + currentGridMax.x) / 2f,
            (currentGridMin.y + currentGridMax.y) / 2f,
            0f
        );

        Vector3 size = new Vector3(
            currentGridMax.x - currentGridMin.x,
            currentGridMax.y - currentGridMin.y,
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
}
