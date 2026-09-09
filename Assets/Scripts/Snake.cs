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

    }

    private void Update()
    {
        PosisitionSnake();
        timer += Time.deltaTime;
        if ( timer >= timerMax )
        {
            //move snake in direction
            snakePosition += snakeDirection;
            timer -= timerMax;
        }
    }

    private void PosisitionSnake()
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

        transform.eulerAngles = new Vector3(0,0,GetAngleFromVector(snakeDirection)-90f);

        transform.position = new Vector3(snakePosition.x, snakePosition.y);
        //Debug.Log("snake position:" + snakePosition);
    }

    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
