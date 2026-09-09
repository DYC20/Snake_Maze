using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;
    //Available Assets
    public Sprite snakeHeadSprite;
    private void Awake()
    {
        instance = this;
    }

    
}
