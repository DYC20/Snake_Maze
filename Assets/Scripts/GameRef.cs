using UnityEngine;

public class GameRef : MonoBehaviour
{
    public static GameRef instance;
    
    //Available References
    [SerializeField] GameObject background;
    
    //Getters
    public GameObject Background => background;

    void Awake()
    {
        instance = this;
    }
}
