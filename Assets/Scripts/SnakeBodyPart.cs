using System;
using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    private Snake snake;
    public int BodyIndex { get; set; }
    
    public void Initialize(Snake owner)
    {
        snake = owner;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            //Debug.Log("Body Hit:" + collision.gameObject.name);
            //Debug.Log("this GO: " + gameObject);
            snake.RemoveBodyPart(gameObject.GetComponent<SnakeBodyPart>());
        }
    }
}
