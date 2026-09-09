using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float foodLifetime = 3f;
    
    private GameObject food;
    private GameObject background;
    private float foodTimer;

    private void Start()
    {
        background = GameRef.instance.Background;
        SpawnFood();
    }

    private void Update()
    {
        FoodCycle();
    }
    private void SpawnFood()
     {
         foodGridPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
         food = Instantiate(foodPrefab);
         food.transform.position = new Vector3(foodGridPos.x, foodGridPos.y, 0f);
     }

    private void FoodCycle()
    {
        foodTimer += Time.deltaTime;
        if (foodTimer >= foodLifetime)
        {
            Destroy(food);
            SpawnFood();
            foodTimer = 0;
        }
            
        
    }

  
}
