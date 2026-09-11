using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;
    //Available Assets
    public Sprite snakeHeadSprite;
    public Sprite snakeBodySprite;
    public Sprite foodSprite;
    private void Awake()
    {
        instance = this;
    }

    
}
