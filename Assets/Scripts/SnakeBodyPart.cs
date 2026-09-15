using System;
using System.Collections;
using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    private Snake snake;
    public int BodyIndex { get; set; }
    
    public void Initialize(Snake owner)
    {
        snake = owner;
    }

    public IEnumerator DestroyBodyPart()
    {
        SpriteRenderer sr = gameObject
            .GetComponentInChildren<SpriteRenderer>();
        sr.enabled = false;
        ParticleSystem ps = gameObject
            .GetComponentInChildren<ParticleSystem>();
        ps.Play();
        yield return new WaitUntil(() => !ps.IsAlive());
        
        snake.RemoveBodyPart(gameObject.GetComponent<SnakeBodyPart>());
        
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            //Debug.Log("Body Hit:" + collision.gameObject.name);
            //Debug.Log("this GO: " + gameObject);
            StartCoroutine(DestroyBodyPart());
        }
    }
}
