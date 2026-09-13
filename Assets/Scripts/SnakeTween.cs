using DG.Tweening;
using UnityEngine;

public class SnakeTween : MonoBehaviour
{
    [Header("EatAnim")]
    [SerializeField] private float eatEndScale;
    [SerializeField] private float eatDuration;
    [SerializeField] private Ease eatEase;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatTween()
    {
        transform.DOScale(eatEndScale, eatDuration).SetEase(eatEase).SetLoops(2, LoopType.Yoyo);

        Debug.LogWarning("Eat Animation Complete");
    }
}
