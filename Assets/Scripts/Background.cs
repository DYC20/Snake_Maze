using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private GameRef gameRef;
    private BoardGrid boardGrid;
    private float gridWidth;
    private float gridHeight;

    private void Awake()
    {
        gameRef = GameRef.instance;
        boardGrid = gameRef.BoardGrid;
    }
    
    private void Start()
    {
        GetScreenBounds();
    }
    
    private void GetScreenBounds()
    {
        gridWidth = boardGrid.Max.x - boardGrid.Min.x + 1f;
        gridHeight = boardGrid.Max.y - boardGrid.Min.y;
        
        //Set BG scale & position
        transform.localScale = new Vector3(gridWidth, gridHeight, 1f);
        transform.transform.position = boardGrid.Center;
    }
}
