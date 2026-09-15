using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class CoinTween : MonoBehaviour
{
    private GameObject coinPrefab;
    private GameObject coin;
    [SerializeField] private int NumberOfCoins;
    [SerializeField] private float incrementBy;
    
    [Header("Scale")]
    [SerializeField] private float scaleDuration;
    [SerializeField] private float targetScale;
    [SerializeField] private Ease scaleEase;
    
    [Header("Movement")]
    private Vector3 targetPosition;
    [SerializeField] private float movementDuration;
    [SerializeField] private Ease moveEase;
    

    private GameRef gameRef;
    private Canvas scoreCanvas;
    private TextMeshProUGUI textMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gameRef = GameRef.instance;
        coinPrefab = gameRef.CoinPrefab;
        scoreCanvas = gameRef.ScoreCanvas.GetComponent<Canvas>();
        textMesh = scoreCanvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        //coinsList = new List<GameObject>();
        targetPosition = new Vector3(textMesh.rectTransform
            .anchoredPosition.x, textMesh.rectTransform.anchoredPosition.y
            , targetScale);
        
    }

    public void CollectCoin()
    {
        StartCoroutine(CollectCoinTween());
    }

    private IEnumerator CollectCoinTween()
    {
        
        for (int i = 0; i < NumberOfCoins; i++)
        {
            coin = Instantiate(coinPrefab, scoreCanvas.transform);
            RectTransform coinRectTransform = coin.GetComponent<RectTransform>();
            //coinRectTransform.localScale = Vector3.zero;
            coinRectTransform.position = transform.position;
            
            Sequence seq = DOTween.Sequence();
            seq.Append(coinRectTransform.DOScale(targetScale, scaleDuration)
                .SetEase(scaleEase).SetLoops(2, LoopType.Yoyo));
            seq.Append(coinRectTransform.DOMove(targetPosition, movementDuration).SetEase(moveEase));
            seq.Join(coinRectTransform.DOScale(Vector3.zero, movementDuration).SetEase(moveEase));
            
            seq.OnComplete(() => Destroy(coin));
        }
        yield return new WaitForSeconds(incrementBy);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
