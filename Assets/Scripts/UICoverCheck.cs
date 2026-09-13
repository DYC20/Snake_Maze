using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UICoverCheck : MonoBehaviour
{
    private Canvas scoreCanvas;
    private GraphicRaycaster graphicRaycaster;
    private ScoreBoard scoreBoard;
    Vector3 screenPosition;
    GameRef gameRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gameRef = GameRef.instance;
    }
    void Start()
    {
        scoreCanvas = gameRef.ScoreCanvas;
        scoreBoard = scoreCanvas.GetComponentInChildren<ScoreBoard>();
        graphicRaycaster = scoreCanvas.GetComponentInChildren<GraphicRaycaster>();
        CheckIfCovered();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetScreenPosition()
    {
        
    }

    private void CheckIfCovered()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };
        List<RaycastResult> results = new();
        graphicRaycaster.Raycast(pointerData, results);
        foreach (var result in results)
        {
            if (result.gameObject == scoreBoard.gameObject)
            {
                scoreBoard.MoveScoreBoard();
            }
        }
    }
}

