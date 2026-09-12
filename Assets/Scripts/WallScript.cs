using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 maxBounds;

    private void OnEnable()
    {
        GetRandomBounds();
    }

    private void GetRandomBounds()
    {
        transform.localScale = new Vector3(Random.Range(maxBounds.x, 1f), Random.Range(maxBounds.y, 1f), maxBounds.z);
    }

    private void Update()
    {
        transform.position += Vector3.up * movementSpeed * Time.deltaTime;
    }
    
    
}
