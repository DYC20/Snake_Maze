using System;
using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject background; 
    [SerializeField] private float timerMax = 1f;
    private Vector2Int snakePosition;
    private Vector2Int snakeDirection;
    private float timer = 0f;
    private Camera mainCamera;
    private Vector2Int minBounds;
    private Vector2Int maxBounds;
    private FoodSpawner foodSpawner;

    private void Awake()
    {
        if (background == null)
            background = GameRef.instance.Background;
        
        PositionSnakeOnAwake();
        snakeDirection = Vector2Int.up;
        Debug.Log("Awake");
    }
    private void PositionSnakeOnAwake()
    {
        snakePosition = new Vector2Int(Mathf.RoundToInt(background.transform.position.x),
            Mathf.RoundToInt(background.transform.position.y));
    }

    private void Start()
    {
        mainCamera = Camera.main;
        CameraBounds();
        foodSpawner = GameRef.instance.FoodSpawner.GetComponent<FoodSpawner>();
    }



    private void Update()
    {
        HandleInput();
        timer += Time.deltaTime;
        if ( timer >= timerMax )
        {
            //move snake in direction
            snakePosition += snakeDirection;
            timer -= timerMax;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (snakeDirection == Vector2Int.down)
                return;
            snakeDirection = Vector2Int.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (snakeDirection == Vector2Int.up)
                return;
            snakeDirection = Vector2Int.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (snakeDirection == Vector2Int.right)
                return;
            snakeDirection = Vector2Int.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (snakeDirection == Vector2Int.left)
                return;
            snakeDirection = Vector2Int.right;
        }

        WrapPosition();
        
        transform.eulerAngles = new Vector3(0,0,GetAngleFromVector(snakeDirection)-90f);

        transform.position = new Vector3(snakePosition.x, snakePosition.y);

        CheckFoodCollision();
        //Debug.Log("snake position:" + snakePosition);
    }

    private void CheckFoodCollision()
    {
        if (foodSpawner.FoodGridPos == snakePosition)
        {
            EatFood();
            foodSpawner.Respawn();
        }
        
    }

    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void CameraBounds()
    {
        //Written with AI Start
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, -mainCamera.transform.position.z)
        );

        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, -mainCamera.transform.position.z)
        );

        minBounds = new Vector2Int(
            Mathf.CeilToInt(bottomLeft.x),
            Mathf.CeilToInt(bottomLeft.y)
        );

        maxBounds = new Vector2Int(
            Mathf.FloorToInt(topRight.x),
            Mathf.FloorToInt(topRight.y)
        );
    }
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
        //Written with AI Ends
    }

    private void EatFood()
    {
        //Add points
        Debug.Log("EatFood");
    }



}
