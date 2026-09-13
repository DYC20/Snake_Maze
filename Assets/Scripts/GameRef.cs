using UnityEngine;

public class GameRef : MonoBehaviour
{
    public static GameRef instance;
    
    //Available References
    [SerializeField] private Canvas scoreCanvas;
    [SerializeField] private Snake snake;
    [SerializeField] private Background background;
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private BoardGrid boardGrid;
    [SerializeField] private WallSpawner wallSpawner;
    [SerializeField] private GameHandler gameHandler;
    
    //Getters
    public Canvas ScoreCanvas => scoreCanvas;
    public Snake Snake => snake;
    public Background Background => background;
    public GameObject SnakeBodyPrefab => snakeBodyPrefab;
    public FoodSpawner FoodSpawner => foodSpawner;
    public BoardGrid BoardGrid => boardGrid;
    public WallSpawner WallSpawner => wallSpawner;
    public GameHandler GameHandler => gameHandler;

    private void Awake()
    {
        instance = this;
    }
}
