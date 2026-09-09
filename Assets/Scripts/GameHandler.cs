using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assign and create snake OB
        GameObject snakeHeadObj = new GameObject();
        SpriteRenderer snakeSpriteRenderer =  snakeHeadObj.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.instance.snakeHeadSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
