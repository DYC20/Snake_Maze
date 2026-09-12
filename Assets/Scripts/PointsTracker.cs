using UnityEngine;

public class PointsTracker : MonoBehaviour
{
    [SerializeField] private int pointsValue;
    private int points;
    private int hieghestPoints;
    void Start()
    {
        hieghestPoints = points;
    }

    public void AddPoints()
    {
        points += pointsValue;
        if (points > hieghestPoints)
            hieghestPoints = points;
        Debug.Log("Points: " + points);
        Debug.Log("hieghestPoints: " + hieghestPoints);
    }

    public void RemovePoints()
    {
        points -= pointsValue;
        Debug.Log("Points: " + points);
    }
}
