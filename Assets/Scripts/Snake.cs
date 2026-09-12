using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    [SerializeField] private float movemantSpeed = 1f;
    [SerializeField] private float speedIncrease = 0.1f;
    [SerializeField] private GameObject snakeBodyPrefab;
    private Vector2Int snakePosition;
    private Vector2Int previousHeadPosition;
    private Vector2Int nextDirection = Vector2Int.up;
    private bool directionQueued;
    private Vector2Int snakeDirection;
    private List<Vector2Int> snakeMovePositionList; 
    private List<Transform> snakeBodyList;
    private float timer = 0f;
    private Vector2Int minBounds;
    private Vector2Int maxBounds;
    private FoodSpawner foodSpawner;
    private BoardGrid boardGrid;
    private WallSpawner wallSpawner;
    private GameRef gameRef;

    private void Awake()
    {
        gameRef = GameRef.instance;
        boardGrid = gameRef.BoardGrid.GetComponent<BoardGrid>();
        
        PositionSnakeOnAwake();
        
        //Debug.Log("Awake");
    }
    private void PositionSnakeOnAwake()
    {
        snakePosition = new Vector2Int(Mathf.RoundToInt(boardGrid.Center.x), Mathf.RoundToInt(boardGrid.Center.y));
        snakeDirection = Vector2Int.up;

        transform.position = new Vector3(
            snakePosition.x,
            snakePosition.y);
        
        //assign bounds for wrap screen
        minBounds = boardGrid.Min;
        maxBounds = boardGrid.Max;
        
        snakeMovePositionList = new List<Vector2Int>();
    }

    private void Start()
    {
        timer = 1f / movemantSpeed;
        snakeBodyList = new List<Transform>();
        foodSpawner = gameRef.FoodSpawner.GetComponent<FoodSpawner>();
        wallSpawner = gameRef.WallSpawner.GetComponent<WallSpawner>();
    }

    private void Update()
    {
        HandleInput();
        timer -= Time.deltaTime;
        if ( timer <= 0 )
        {
            //move snake in direction
            timer += 1f / movemantSpeed;
            MoveSnake();
        }
    }

    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void HandleInput()
    {
        if (directionQueued)
            return;
        
        Vector2Int requestedDirection;
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            requestedDirection = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            requestedDirection = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            requestedDirection = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            requestedDirection = Vector2Int.right;
        }
        else
        {
            return;
        }
        
        if (requestedDirection == -snakeDirection)
            return;
        nextDirection = requestedDirection;
        
        directionQueued = true;

        //Debug.Log("snake position:" + snakePosition);
    }
    //Written with AI start
    private void WrapPosition()
    {
        if (snakePosition.x < minBounds.x)
        {
            snakePosition.x = maxBounds.x;
        }
        else if (snakePosition.x > maxBounds.x)
        {
            snakePosition.x = minBounds.x;
        }

        if (snakePosition.y < minBounds.y)
        {
            snakePosition.y = maxBounds.y;
        }
        else if (snakePosition.y > maxBounds.y)
        {
            snakePosition.y = minBounds.y;
        }
        //Written with AI End
    }
    private void MoveSnake()
    {
        snakeDirection = nextDirection;
        directionQueued = false;
        
        previousHeadPosition = snakePosition;
        snakePosition += snakeDirection;
        WrapPosition();
        
        snakeMovePositionList.Insert(0, previousHeadPosition);
        
        transform.position = new Vector3(snakePosition.x, snakePosition.y);
        transform.eulerAngles = new Vector3(0,0,GetAngleFromVector(snakeDirection)-90f);
        /*
        Debug.Log(
            $"Body parts: {snakeBodyList.Count}, " +
            $"positions: {snakeMovePositionList.Count}"
        );
        */
        for (int i = 0; i < snakeBodyList.Count; i++)
        {
            Vector2Int bodyPosition = snakeMovePositionList[i];
    
            snakeBodyList[i].position = new Vector3(bodyPosition.x, bodyPosition.y);
            //Debug.Log("snakeBodyList:" + snakeBodyList.Count);
        }
        CheckFoodCollision();

        while (snakeMovePositionList.Count > snakeBodyList.Count)
        {
            snakeMovePositionList.RemoveAt(
                snakeMovePositionList.Count - 1
            );
        }
    }

    private void CheckFoodCollision()
    {
        if (foodSpawner.FoodPos == snakePosition)
        {
            EatFood();
            foodSpawner.Respawn();
        }
    }
    private void EatFood()
    {
        //Add points
        
        //Increase speed
        movemantSpeed += speedIncrease;
        wallSpawner.WallSpawnTime -= speedIncrease;
        //Written with AI start
        int newBodyIndex = snakeBodyList.Count;
        
        if (newBodyIndex >= snakeMovePositionList.Count)
        {
            Debug.LogError(
                $"Missing body position. Body count: {snakeBodyList.Count}, " +
                $"position count: {snakeMovePositionList.Count}"
            );

            return;
        }

        Vector2Int spawnPosition =
            snakeMovePositionList[newBodyIndex];

        GameObject newBodyPart = Instantiate(
            snakeBodyPrefab,
            new Vector3(spawnPosition.x, spawnPosition.y, 0f),
            Quaternion.identity
        );
        //SnakeBodyPart snakeBodyPart = newBodyPart.GetComponent<SnakeBodyPart>();

        snakeBodyList.Add(newBodyPart.transform);
        SnakeBodyPart body = newBodyPart.GetComponent<SnakeBodyPart>();

        body.Initialize(this);
        
        Debug.Log("Body count: " + snakeBodyList.Count);
        Debug.Log("EatFood");
    }
    //Written with AI end
    public void RemoveBodyPart(SnakeBodyPart bodyPart)
    {
        int hitIndex = snakeBodyList.IndexOf(bodyPart.transform);
        Debug.Log("hitIndex: " + hitIndex);
        if (hitIndex < 0)
            return;
        Debug.Log("Hit Index:" + hitIndex);
        Debug.Log("snake body count:" + snakeBodyList.Count);
        
        for (int i = snakeBodyList.Count - 1; i >= hitIndex; i--)
        {
            Debug.Log("i:" + snakeBodyList[i]);
            Destroy(snakeBodyList[i].gameObject);
            snakeBodyList.RemoveAt(i);
        }

        while (snakeMovePositionList.Count > snakeBodyList.Count)
        {
            snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            //snake loose health | dies
            Debug.Log("Head Hit Wall");
        }
    }
    
}
