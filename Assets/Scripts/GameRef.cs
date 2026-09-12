using UnityEngine;

public class GameRef : MonoBehaviour
{
    public static GameRef instance;
    
    //Available References
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private GameObject foodSpawner;
    [SerializeField] private GameObject boardGrid;
    [SerializeField] private GameObject wallSpawner;
    
    //Getters
    public GameObject Background => background;
    public GameObject SnakeBodyPrefab => snakeBodyPrefab;
    public GameObject FoodSpawner => foodSpawner;
    public GameObject BoardGrid => boardGrid;

    private void Awake()
    {
        instance = this;
    }
}
