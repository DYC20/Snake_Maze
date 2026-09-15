using UnityEngine;
using UnityEngine.Playables;

public class GameRef : MonoBehaviour
{
    public static GameRef instance;
    
    //Available References
    [SerializeField] private Canvas scoreCanvas;
    [SerializeField] private PlayableAsset endGameCanvasTL;
    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private BoardGrid boardGrid;
    [SerializeField] private WallSpawner wallSpawner;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameHandler gameHandler;
    
    //Getters
    public Canvas ScoreCanvas => scoreCanvas;
    public PlayableAsset EndGameCanvasTL => endGameCanvasTL;

    public FoodSpawner FoodSpawner => foodSpawner;
    public BoardGrid BoardGrid => boardGrid;
    public WallSpawner WallSpawner => wallSpawner;
    public GameObject CoinPrefab => coinPrefab;
    public GameHandler GameHandler => gameHandler;

    private void Awake()
    {
        instance = this;
    }
}
