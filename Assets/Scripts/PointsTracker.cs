using UnityEngine;

public class PointsTracker : MonoBehaviour
{
    [SerializeField] private int pointsValue;
    private int points;
    private int hieghestPoints;
    void Start()
    {
        hieghestPoints = points;
        Debug.Log("Points Tracker Started");
    }

    public void AddPoints()
    {
        points += pointsValue;
        if (points > hieghestPoints)
            HieghestPoints();
            
        Debug.Log("Points: " + points);
        Debug.Log("hieghestPoints: " + hieghestPoints);
    }

    private void HieghestPoints()
    {
        int pointsToAdd = points - hieghestPoints;
        hieghestPoints += pointsToAdd;
    }

    public void RemovePoints()
    {
        points -= pointsValue;
        Debug.Log("Points: " + points);
    }

    public void ResetPoints()
    {
        points = 0;
    }
}
