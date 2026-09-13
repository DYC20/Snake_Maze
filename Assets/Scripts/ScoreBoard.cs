using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScoreBoard : MonoBehaviour
{
    Vector3 newPosition;
    RectTransform rectTransform;
    private float lockedY;
    private GameRef gameRef;
    private Vector2Int minBounds;
    private Vector2Int maxBounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gameRef = GameRef.instance;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        lockedY = rectTransform.anchoredPosition.y;
        maxBounds = gameRef.BoardGrid.Max;
        minBounds = gameRef.BoardGrid.Min;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveScoreBoard()
    {
        FindNewPosition();
        Vector3 nextPosition = 
            new Vector3(newPosition.x, lockedY);
        Tween moveBoardTween = 
            rectTransform.DOMove(transform.position + nextPosition, .1f);
        Debug.LogWarning("Moving score board");
    }

    private void FindNewPosition()
    {
        newPosition = new Vector3(Random.Range(minBounds.x, maxBounds.x), 
            Random.Range(minBounds.y, maxBounds.y));
    }
}
