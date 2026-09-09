using UnityEngine;

public class GameRef : MonoBehaviour
{
    public static GameRef instance;
    
    //Available References
    [SerializeField] GameObject background;
    [SerializeField] GameObject snakeBodyPrefab;
    [SerializeField] private GameObject foodSpawner;
    
    //Getters
    public GameObject Background => background;
    public GameObject SnakeBodyPrefab => snakeBodyPrefab;
    public GameObject FoodSpawner => foodSpawner;

    private void Awake()
    {
        instance = this;
    }
}
