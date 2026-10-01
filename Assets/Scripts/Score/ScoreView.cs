using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public void UpdateView(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}
