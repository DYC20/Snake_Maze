using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
 [SerializeField] private GameObject snakePrefab;
 [SerializeField] private PointsTracker pointsTracker;
 [SerializeField] private WallSpawner wallSpawner;
 [SerializeField] private FoodSpawner foodSpawner;
 [SerializeField] private Canvas startGameCanvas;
 [SerializeField] private Canvas endGameCanvas;
 
 private Snake snake;

 private void Start()
 {
     
 }

 public void StartGame()
    {
        //Assign and create snake OB
        GameObject snakeHeadObj = Instantiate(snakePrefab);
        snake = snakeHeadObj.GetComponent<Snake>();
        snake.EndGameCanvas = endGameCanvas;
        snake.PointsTracker = pointsTracker;
        snake.WallSpawner = wallSpawner;
        wallSpawner.DestroyAllWalls();
        wallSpawner.GameStarted = true;
        foodSpawner.GameStarted = true;
        foodSpawner.NotFirstSpawn = false;
        startGameCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
    }

    public void GameEnd()
    {
        wallSpawner.GameStarted = false;
        foodSpawner.GameStarted = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
