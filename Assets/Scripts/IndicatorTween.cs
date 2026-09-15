using DG.Tweening;
using UnityEngine;

public class IndicatorTween : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float targetSize;
    [SerializeField] private Ease ease;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiationTween();
    }

    private void InstantiationTween()
    {
        transform.DOScale(targetSize, duration)
            .SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
