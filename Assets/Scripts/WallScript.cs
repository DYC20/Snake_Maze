using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    private void Update()
    {
        transform.position += Vector3.up * movementSpeed * Time.deltaTime;
    }
    
}
