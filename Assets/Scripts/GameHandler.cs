using UnityEngine;

public class GameHandler : MonoBehaviour
{
 [SerializeField] private GameObject snakePrefab;
    void Start()
    {
        //Assign and create snake OB
        GameObject snakeHeadObj = Instantiate(snakePrefab);
        SpriteRenderer snakeSpriteRenderer =  snakeHeadObj.GetComponent<SpriteRenderer>();

        if (snakeSpriteRenderer == null)
        {
            snakeSpriteRenderer = snakeHeadObj.AddComponent<SpriteRenderer>();
            snakeSpriteRenderer.sprite = GameAssets.instance.snakeHeadSprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
