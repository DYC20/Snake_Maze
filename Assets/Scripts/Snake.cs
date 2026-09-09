using System;
using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour
{
    [SerializeField] GameObject background; 
    private Vector2Int snakePosition;

    private void Awake()
    {
        PositionSnakeOnAwake();
        Debug.Log("Started");
    }

    private void Update()
    {
        PosisitionSnake();
    }

    private void PosisitionSnake()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
            snakePosition.y += 1;
        if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
            snakePosition.y += -1;
        if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
            snakePosition.x += -1;
        if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
            snakePosition.x += 1;
    }

    private void PositionSnakeOnAwake()
    {
        snakePosition = new Vector2Int(Mathf.RoundToInt(background.transform.position.x), Mathf.RoundToInt(background.transform.position.y));
        transform.position = new Vector3(snakePosition.x, snakePosition.y);
    }
}
