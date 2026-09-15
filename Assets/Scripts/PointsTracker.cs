using UnityEngine;
using TMPro;

public class PointsTracker : MonoBehaviour
{
    [SerializeField] private float scoreValue;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private float score;
    private float hieghestScore;
    void Start()
    {
        hieghestScore = score;
        Debug.Log("Points Tracker Started");
    }

    private void UpdateScore()
    {
        scoreText.SetText("Score: {0}", score);
        highScoreText.SetText("High Score: {0}", hieghestScore);
    }

    public void AddPoints()
    {
        score += scoreValue;
        if (score > hieghestScore)
            HieghestPoints();

        UpdateScore();
        Debug.Log("Points: " + score);
        Debug.Log("hieghestPoints: " + hieghestScore);
    }

    private void HieghestPoints()
    {
        float scoreToAdd = score - hieghestScore;
        hieghestScore += scoreToAdd;
    }

    public void RemovePoints()
    {
        score -= scoreValue;
        UpdateScore();
        Debug.Log("Points: " + score);
    }

    public void ResetPoints()
    {
        score = 0;
        UpdateScore();
    }
}
