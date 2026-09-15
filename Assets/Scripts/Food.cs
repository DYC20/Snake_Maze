using System;
using DG.Tweening;
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;
    private SpriteRenderer spriteRenderer;
    
    [Header("Instantiation Effect")]
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float waitForPS;
    private ParticleSystem ps;
    
    [Header("Instantiation Tween")]
    [SerializeField] private float tweenDuration;
    [SerializeField] private Ease ease = Ease.OutBounce;
    
    [SerializeField] private Vector3 shakeStrength;
    [SerializeField] private int shakeVibrato;
    [SerializeField] private float scaleValue;
    private float startScale;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        startScale = transform.localScale.x;
    }

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        transform.localScale = Vector3.zero;
        ps = Instantiate(effect, transform.position, effect.transform.rotation);
        if (effect == null)
        {
            Debug.LogError("No effect attached to " + gameObject.name);
        }
        InstantiationTween();
    }

    private void InstantiationTween()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            ps.Play();
        });
        seq.AppendInterval(waitForPS);
        seq.Append(transform.DOShakeRotation(
            tweenDuration, shakeStrength, shakeVibrato).SetEase(ease));
        seq.Join(transform.DOScale(
            scaleValue, tweenDuration / 2).SetEase(ease));
        seq.Append(transform.DOScale(
            startScale, tweenDuration / 2).SetEase(ease));
        if (effect == null)
        {
            Debug.LogError("Effect is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log($"OnTriggerEnter Food: {other.gameObject.name}");
            spriteRenderer.color = hoverColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            spriteRenderer.color = normalColor;
        }
    }
}
