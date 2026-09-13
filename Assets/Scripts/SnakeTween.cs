using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnakeTween : MonoBehaviour
{
    [Header("EatAnim")]
    [SerializeField] private float eatEndScale;
    [SerializeField] private float eatDuration;
    [SerializeField] private Ease eatEase;
    private float delayBetweenTweens;
    private SpriteRenderer sp;
    public float DelayBetweenTweens => delayBetweenTweens;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
        delayBetweenTweens = eatDuration / 2f;
        Debug.Log("renderer in snake tween:" + sp.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Tween EatTween()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform
            .DOScale(eatEndScale, eatDuration)
            .SetEase(eatEase)
            .SetLoops(2, LoopType.Yoyo));
        seq.Join(sp.DOColor(Color.white, eatDuration)
            .SetEase(eatEase)
            .SetLoops(2, LoopType.Yoyo));
        
        seq.OnComplete(() => seq.Kill()); 
        
        Debug.LogWarning("Eat Animation Complete");   
        
        return seq;
        
    }
}
